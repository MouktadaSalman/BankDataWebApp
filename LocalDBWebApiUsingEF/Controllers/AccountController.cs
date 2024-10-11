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
        private readonly DBManager _context;
        private readonly ILogger<AccountController> _logger;
        private static readonly object _logLock = new object();

        public AccountController(ILogger<AccountController> logger, DBManager context)
        {
            _logger = logger;
            _context = context;
        }

        private void Log(string? message, LogLevel logLevel, Exception? ex)
        {
            lock (_logLock)
            {
                if (ex != null)
                {
                    _logger.Log(logLevel, ex, $"{DateTime.Now}:");
                }
                else
                {
                    string logEntry = $"{DateTime.Now}: {message}";
                    _logger.Log(logLevel, logEntry);
                }
            }
        }

        // GET: api/account
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            List<Account> accounts = await _context.Accounts.ToListAsync();
            return Ok(accounts);
        }

        // GET: api/account/5
        [HttpGet("{Id}")]
        public async Task<ActionResult<Account>> GetAccountsById(int Id)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var accounts = await _context.Accounts
                .Where(a => a.UserId == Id)
                .ToListAsync();

            accounts.ForEach(account => Console.WriteLine(account));

            if (accounts == null)
            {
                return NotFound();
            }
            return Ok(accounts);
        }

        // GET: api/account/history/{acctNo}
        [HttpGet("history/{acctNo}")]
        public async Task<ActionResult<List<string>>> GetAccountHistory(uint acctNo, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(u => u.History)
                .FirstOrDefaultAsync(u => u.AcctNo == acctNo);

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

            var filteredHistory = historyQuery.Select(h => h.HistoryString).ToList();

            if (filteredHistory.Count == 0)
            {
                return Ok(new List<string> { "No transactions found for this date range." });
            }

            return Ok(filteredHistory);
        }


        // GET: api/account/1/1000
        [HttpPut("{type}/{acctNo}/{amount}")]
        public async Task<ActionResult<Account>> UpdateBalance(string type, uint acctNo, int amount)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(acctNo);

            if (account == null)
            {
                return NotFound();
            }

            account.Balance += amount;
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
            account.History.Add(historyEntry);

            _context.UserHistories.Add(historyEntry);
            _context.Entry(account).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT: api/account/1
        /* BODY -> row -> Enter new updated account details for the acctNo
        * {
            "acctNo": 1, 
            "firstName": "Sajib Updated",
            "lastName": "Updated",
            "email": "updated_email@email.com",
            "age": 30,
            "balance": 5000,
            "address": "1 Street Suburb Updated"
         }
        */

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{acctNo}")]
        public async Task<IActionResult> PutAccount(uint acctNo, Account account)
        {
            if (acctNo != account.AcctNo)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(acctNo))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/account
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount([FromBody] Account account)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'DBManager.Accounts'  is null.");
            }

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { acctNo = account.AcctNo }, account);
        }

        // DELETE: api/account/5
        [HttpDelete("{acctNo}")]
        public async Task<IActionResult> DeleteAccount(uint acctNo)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(acctNo);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /*================================From Business admin controller=================================*/
        [HttpPut("fromadmin/{acctNo}")]
        public async Task<IActionResult> AdminPutAccount(uint acctNo, [FromBody] Account account)
        {
            try
            {
                Log($"Attempt to add account to database: {account.AcctNo}", LogLevel.Warning, null);
                if (_context.Accounts == null)
                {
                    throw new DataGenerationFailException("Accounts");
                }

                if (acctNo != account.AcctNo)
                {
                    Log("Account didn't pass through", LogLevel.Warning, null);
                    throw new MissingAccountException("");
                }

                _context.Entry(account).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DataGenerationFailException ex)
            {
                Log(null, LogLevel.Critical, ex);
                return NotFound();
            }
            catch (MissingAccountException ex)
            {
                Log(null, LogLevel.Warning, ex);
                return BadRequest();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Log(null, LogLevel.Warning, ex);
                return StatusCode(StatusCodes.Status409Conflict, "Concurrency conflict");
            }
            catch (Exception ex)
            {
                Log(null, LogLevel.Critical, ex);
                //Catch other unkown exceptions
                return BadRequest();
            }
        }
        // POST: api/account/new
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("addaccount")]
        public async Task<IActionResult> AddNewAccount([FromBody] Account account)
        {
            try
            {
                Log($"Attempt to add account to database: {account.AcctNo}", LogLevel.Warning, null);
                if (_context.Accounts == null)
                {
                    throw new DataGenerationFailException("Accounts");
                }

                if (account == null)
                {
                    Log("Account didn't pass through", LogLevel.Warning, null);
                    throw new MissingAccountException("");
                }

                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DataGenerationFailException ex)
            {
                Log(null, LogLevel.Warning, ex);
                return NotFound();
            }
            catch (MissingAccountException ex)
            {
                Log(null, LogLevel.Warning, ex);
                return BadRequest();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Log(null, LogLevel.Warning, ex);
                return StatusCode(StatusCodes.Status409Conflict, "Concurrency conflict");
            }
            catch (Exception ex)
            {
                Log(null, LogLevel.Critical, ex);
                //Catch other unkown exceptions
                return BadRequest();
            }
        }

        // DELETE: api/account/5
        [HttpDelete("fromadmin/{acctNo}")]
        public async Task<IActionResult> DeleteAccountFromAdmin(uint acctNo)
        {
            try
            {
                if (_context.Accounts == null)
                {
                    throw new DataGenerationFailException("Accounts");
                }

                var account = await _context.Accounts.FindAsync(acctNo);
                if (account == null)
                {
                    throw new MissingAccountException($"'{acctNo}'");
                }

                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DataGenerationFailException)
            {
                return NotFound();
            }
            catch (MissingAccountException)
            {
                return BadRequest();
            }
            catch (DbUpdateConcurrencyException)
            {
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
                //Catch other unkown exceptions
                return BadRequest();
            }
        }
        /*================================From Business admin controller=================================*/

        private bool AccountExists(uint acctNo)
        {
            return (_context.Accounts?.Any(e => e.AcctNo == acctNo)).GetValueOrDefault();
        }
    }
}