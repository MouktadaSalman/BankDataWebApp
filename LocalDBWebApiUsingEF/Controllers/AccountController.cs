/* 
 * Module: AccountController
 * Description: Handles API requests related to the account management
 * Author: Ahmed, Moukhtada, Jauhar
 * ID: 21467369, 20640266, 21494299
 * Version: 1.0.0.4
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataTierWebServer.Models;
using DataTierWebServer.Data;
using Microsoft.CodeAnalysis.Scripting;
using System.Xml.Linq;
using DataTierWebServer.Models.Exceptions;

namespace DataTierWebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        // Adding variables that will be used for this controller
        private readonly DBManager _context;
        private readonly ILogger<AccountController> _logger;
        private static readonly object _logLock = new object();

        /* 
         * Method: AccountController
         * Description: Constructor for the AccountController class
         * Params:
         *   logger: The ILogger instance for logging
         *   context: The DBManager instance for database operations
         * Use: Constructor for dependency injection
         */
        public AccountController(ILogger<AccountController> logger, DBManager context)
        {
            // Initialize logger for this controller
            _logger = logger;
            // Set the database context for data operations
            _context = context;
        }

        /* 
         * Method: Log
         * Description: Logs messages or exceptions with thread-safe implementation
         * Params:
         *   message: The message to log
         *   logLevel: The severity level of the log
         *   ex: The exception to log, if any
         */
        private void Log(string? message, LogLevel logLevel, Exception? ex)
        {
            // Ensure thread-safe logging
            lock (_logLock)
            {
                if (ex != null)
                {
                    // Log exception with timestamp
                    _logger.Log(logLevel, ex, $"{DateTime.Now}:");
                }
                else
                {
                    // Log message with timestamp
                    string logEntry = $"{DateTime.Now}: {message}";
                    _logger.Log(logLevel, logEntry);
                }
            }
        }

        /* 
         * Method: GetAccounts
         * Description: Retrieves all accounts
         * Params: None
         * Use: GET: api/account
         */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            // Check if Accounts DbSet is null
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            // Retrieve all accounts from the database
            List<Account> accounts = await _context.Accounts.ToListAsync();
            return Ok(accounts);
        }

        /* 
        * Method: GetAccountsById
        * Description: Retrieves accounts associated with a specific user ID
        * Params:
        *   Id: The user ID to search for
        * Use: GET: api/account/{id}
        */
        [HttpGet("{Id}")]
        public async Task<ActionResult<Account>> GetAccountsById(int Id)
        {
            // Check if Accounts DbSet is null
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            // Retrieve accounts associated with the given user ID
            var accounts = await _context.Accounts
                .Where(a => a.UserId == Id)
                .ToListAsync();

            // Print each account to console (for debugging)
            accounts.ForEach(account => Console.WriteLine(account));

            // Check if any accounts were found
            if (accounts == null)
            {
                return NotFound();
            }
            return Ok(accounts);
        }

        /* 
         * Method: GetAccountHistory
         * Description: Retrieves the transaction history for a specific account
         * Params:
         *   acctNo: The account number to retrieve history for
         *   startDate: Optional start date for filtering
         *   endDate: Optional end date for filtering
         * Use: GET: api/account/history/{acctNo}
         */
        [HttpGet("history/{acctNo}")]
        public async Task<ActionResult<List<string>>> GetAccountHistory(uint acctNo, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            // Check if Accounts DbSet is null
            if (_context.Accounts == null)
            {
                return NotFound();
            }

            // Retrieve the account with its history
            var account = await _context.Accounts
                .Include(u => u.History)
                .FirstOrDefaultAsync(u => u.AcctNo == acctNo);

            // Check if the account exists
            if (account == null)
            {
                return NotFound();
            }

            // Filter the history by date range, if provided
            var historyQuery = account.History.AsQueryable();

            if (startDate.HasValue)
            {
                historyQuery = historyQuery.Where(h => h.DateTime >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                historyQuery = historyQuery.Where(h => h.DateTime <= endDate.Value);
            }

            // Select the history strings from the filtered query
            var filteredHistory = historyQuery.Select(h => h.HistoryString).ToList();

            // Return a message if no transactions were found
            if (filteredHistory.Count == 0)
            {
                return Ok(new List<string> { "No transactions found for this date range." });
            }

            return Ok(filteredHistory);
        }

        /* 
         * Method: UpdateBalance
         * Description: Updates the balance of an account and creates a transaction history entry
         * Params:
         *   type: The type of transaction (deposit, withdraw, send, receive)
         *   acctNo: The account number to update
         *   amount: The amount to update the balance by
         * Use: GET: api/account/{accctNo}/{amount}
         */
        [HttpPut("{type}/{acctNo}/{amount}")]
        public async Task<ActionResult<Account>> UpdateBalance(string type, uint acctNo, int amount)
        {
            // Check if Accounts DbSet is null
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            // Find the account by its number
            var account = await _context.Accounts.FindAsync(acctNo);

            // Check if the account exists
            if (account == null)
            {
                return NotFound();
            }

            // Update the account balance
            account.Balance += amount;

            // Create a new history entry for this transaction
            var historyEntry = new UserHistory
            {
                AccountId = account.AcctNo,
                Amount = amount,
                Type = type.ToLower() == "receive" ? "Receive" :
                       type.ToLower() == "send" ? "Send" :
                       (amount >= 0) ? "Deposit" : "Withdraw",
                DateTime = DateTime.Now,
                Sender = acctNo,
                HistoryString = type.ToLower() == "receive" ?
                    $"Account ID: {account.AcctNo} --- " +
                    $"Received: ${amount:F2} --- " +
                    $"Date and Time: {DateTime.Now:MMMM dd, yyyy HH:mm tt}" :
                    type.ToLower() == "send" ?
                    $"Account ID: {account.AcctNo} --- " +
                    $"Sent: ${Math.Abs(amount):F2} --- " +
                    $"Date and Time: {DateTime.Now:MMMM dd, yyyy HH:mm tt}" :
                    (amount >= 0) ?
                    $"Account ID: {account.AcctNo} --- " +
                    $"Deposited: ${amount:F2} --- " +
                    $"Date and Time: {DateTime.Now:MMMM dd, yyyy HH:mm tt}" :
                    $"Account ID: {account.AcctNo} --- " +
                    $"Withdrawn: ${Math.Abs(amount):F2} --- " +
                    $"Date and Time: {DateTime.Now:MMMM dd, yyyy HH:mm tt}"
            };
            // Add the history entry to the account's history
            account.History.Add(historyEntry);

            // Add the history entry to the UserHistories DbSet
            _context.UserHistories.Add(historyEntry);
            // Mark the account as modified in the context
            _context.Entry(account).State = EntityState.Modified;

            // Save changes to the database
            await _context.SaveChangesAsync();
            return Ok();
        }

        /* 
         * Method: PutAccount
         * Description: Updates an existing account's details
         * Params:
         *   acctNo: The account number to update
         *   account: The updated account details
         * Use: PUT: api/account/1
         * BODY -> row -> Enter new updated account details for the acctNo
         * {
         *   "acctNo": 4262, 
         *   "acctName": 'Saving Account'
         *   "balance": 5250,
         *   "userId": 8
         * }
         */
        [HttpPut("{acctNo}")]
        public async Task<IActionResult> PutAccount(uint acctNo, Account account)
        {
            // Check if the provided account number matches the account's AcctNo
            if (acctNo != account.AcctNo)
            {
                return BadRequest();
            }

            // Mark the account as modified in the context
            _context.Entry(account).State = EntityState.Modified;

            try
            {
                // Attempt to save changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // If the account doesn't exist, return NotFound
                if (!AccountExists(acctNo))
                {
                    return NotFound();
                }
                else
                {
                    // If it's a different error, rethrow the exception
                    throw;
                }
            }

            return NoContent();
        }

        /* 
         * Method: PostAccount
         * Description: Creates a new account
         * Params:
         *   account: The account details to create
         * Use: POST: api/account
         */
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount([FromBody] Account account)
        {
            // Check if Accounts DbSet is null
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'DBManager.Accounts'  is null.");
            }

            // Add the new account to the context
            _context.Accounts.Add(account);
            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return a CreatedAtAction result
            return CreatedAtAction("GetAccount", new { acctNo = account.AcctNo }, account);
        }

        /* 
        * Method: DeleteAccount
        * Description: Deletes an existing account
        * Params:
        *   acctNo: The account number to delete
        * Use: DELETE: api/account/5
        */
        [HttpDelete("{acctNo}")]
        public async Task<IActionResult> DeleteAccount(uint acctNo)
        {
            // Check if Accounts DbSet is null
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            // Find the account by its number
            var account = await _context.Accounts.FindAsync(acctNo);
            // If the account doesn't exist, return NotFound
            if (account == null)
            {
                return NotFound();
            }

            // Remove the account from the context
            _context.Accounts.Remove(account);
            // Save changes to the database
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /*================================From Business admin controller=================================*/
        /* 
        * Method: AdminPutAccount
        * Description: Updates an existing account's details (admin operation)
        * Params:
        *   acctNo: The account number to update
        *   account: The updated account details
        * Use: PUT: api/account/fromadmin/{acctNo}
        */
        [HttpPut("fromadmin/{acctNo}")]
        public async Task<IActionResult> AdminPutAccount(uint acctNo, [FromBody] Account account)
        {
            try
            {
                // Log the attempt to update the account
                Log($"Attempt to add account to database: {account.AcctNo}", LogLevel.Warning, null);
                // Check if Accounts DbSet is null
                if (_context.Accounts == null)
                {
                    throw new DataGenerationFailException("Accounts");
                }

                // Check if the provided account number matches the account's AcctNo
                if (acctNo != account.AcctNo)
                {
                    Log("Account didn't pass through", LogLevel.Warning, null);
                    throw new MissingAccountException("");
                }

                // Mark the account as modified in the context
                _context.Entry(account).State = EntityState.Modified;
                // Save changes to the database
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DataGenerationFailException ex)
            {
                // Log the exception and return NotFound
                Log(null, LogLevel.Critical, ex);
                return NotFound();
            }
            catch (MissingAccountException ex)
            {
                // Log the exception and return BadRequest
                Log(null, LogLevel.Warning, ex);
                return BadRequest();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Log the exception and return a conflict status
                Log(null, LogLevel.Warning, ex);
                return StatusCode(StatusCodes.Status409Conflict, "Concurrency conflict");
            }
            catch (Exception ex)
            {
                // Log the exception and return BadRequest for unknown exceptions
                Log(null, LogLevel.Critical, ex);
                //Catch other unkown exceptions
                return BadRequest();
            }
        }

        /* 
         * Method: AddNewAccount
         * Description: Creates a new account (admin operation)
         * Params:
         *   account: The account details to create
         * Use: POST: api/account/addaccount
         */
        [HttpPost("addaccount")]
        public async Task<IActionResult> AddNewAccount([FromBody] Account account)
        {
            try
            {
                // Log the attempt to add a new account
                Log($"Attempt to add account to database: {account.AcctNo}", LogLevel.Warning, null);
                // Check if Accounts DbSet is null
                if (_context.Accounts == null)
                {
                    throw new DataGenerationFailException("Accounts");
                }

                // Check if the account object is null
                if (account == null)
                {
                    Log("Account didn't pass through", LogLevel.Warning, null);
                    throw new MissingAccountException("");
                }

                // Add the new account to the context
                _context.Accounts.Add(account);
                // Save changes to the database
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DataGenerationFailException ex)
            {
                // Log the exception and return NotFound
                Log(null, LogLevel.Warning, ex);
                return NotFound();
            }
            catch (MissingAccountException ex)
            {
                // Log the exception and return BadRequest
                Log(null, LogLevel.Warning, ex);
                return BadRequest();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Log the exception and return a conflict status
                Log(null, LogLevel.Warning, ex);
                return StatusCode(StatusCodes.Status409Conflict, "Concurrency conflict");
            }
            catch (Exception ex)
            {
                // Log the exception and return BadRequest for unknown exceptions
                Log(null, LogLevel.Critical, ex);
                //Catch other unkown exceptions
                return BadRequest();
            }
        }

        /* 
         * Method: DeleteAccountFromAdmin
         * Description: Deletes an existing account (admin operation)
         * Params:
         *   acctNo: The account number to delete
         * Use: DELETE: api/account/fromadmin/{acctNo}
         */
        [HttpDelete("fromadmin/{acctNo}")]
        public async Task<IActionResult> DeleteAccountFromAdmin(uint acctNo)
        {
            try
            {
                // Check if Accounts DbSet is null
                if (_context.Accounts == null)
                {
                    throw new DataGenerationFailException("Accounts");
                }

                // Find the account by its number
                var account = await _context.Accounts.FindAsync(acctNo);
                // If the account doesn't exist, throw an exception
                if (account == null)
                {
                    throw new MissingAccountException($"'{acctNo}'");
                }

                // Remove the account from the context
                _context.Accounts.Remove(account);
                // Save changes to the database
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DataGenerationFailException)
            {
                // Return NotFound if Accounts DbSet is null
                return NotFound();
            }
            catch (MissingAccountException)
            {
                // Return BadRequest if the account is not found
                return BadRequest();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency conflicts
                if (AccountExists(acctNo))
                {
                    return StatusCode(StatusCodes.Status409Conflict, "Concurrency conflict");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                // Return BadRequest for other unknown exceptions
                return BadRequest();
            }
        }
        /*================================From Business admin controller=================================*/

        /* 
         * Method: AccountExists
         * Description: Checks if an account with the given account number exists
         * Params:
         *   acctNo: The account number to check
         */
        private bool AccountExists(uint acctNo)
        {
            // Check if any account with the given account number exists in the database
            return (_context.Accounts?.Any(e => e.AcctNo == acctNo)).GetValueOrDefault();
        }
    }
}