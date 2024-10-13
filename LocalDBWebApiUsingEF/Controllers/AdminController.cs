/* 
 * Module: AdminController
 * Description: Handles HTTP requests related to admin operations
 * Author: Jauhar
 * ID: 21494299
 * Version: 1.0.0.1
 */

using DataTierWebServer.Data;
using DataTierWebServer.Models;
using DataTierWebServer.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DataTierWebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        // Database context for data operations
        private readonly DBManager _context;
        // Logger for this controller
        private readonly ILogger<AdminController> _logger;
        // Lock object for thread-safe logging
        private static readonly object _logLock = new object();

        /* 
         * Method: AdminController
         * Description: Constructor for the AdminController class
         * Params:
         *   dBManager: The DBManager instance for database operations
         *   logger: The ILogger instance for logging
         * Use: Constructor for dependency injection
         */
        public AdminController(DBManager dBManager, ILogger<AdminController> logger)
        {
            // Initialize the database context and logger
            _context = dBManager;
            _logger = logger;
        }

        /* 
         * Method: Log
         * Description: Logs messages with thread-safe implementation
         * Params:
         *   message: The message to log
         *   logLevel: The severity level of the log
         */
        private void Log(string message, LogLevel logLevel)
        {
            // Ensure thread-safe logging
            lock (_logLock)
            {
                // Create a log entry with timestamp
                string logEntry = $"{DateTime.Now}: {message}";

                // Log the message
                _logger.Log(logLevel, logEntry);
            }
        }

        /* 
         * Method: GetAccounts
         * Description: Retrieves all user histories
         * Params: None
         * Use: GET: api/admin/userhistory
         */
        [HttpGet("userhistory")]
        public async Task<ActionResult<IEnumerable<UserHistory>>> GetAccounts()
        {
            // Check if Accounts DbSet is null
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            // Retrieve all user histories from the database
            List<UserHistory> histories = await _context.UserHistories.ToListAsync();
            return Ok(histories);
        }

        /* 
         * Method: GetAccountByNo
         * Description: Retrieves an account by its account number
         * Params:
         *   acctNo: The account number to search for
         * Use: GET: api/admin/byno/2135
         */
        [HttpGet("byno/{acctNo}")]
        public async Task<ActionResult> GetAccountByNo(uint acctNo)
        {
            try
            {
                // Log the attempt to retrieve account details
                Log($"Attempt to get account details: '{acctNo}'", LogLevel.Information);
                // Check if Accounts DbSet is null
                if (_context.Accounts == null)
                {
                    throw new DataGenerationFailException("Accounts");
                }
                // Find the account by its number
                var account = await _context.Accounts.FirstOrDefaultAsync(up => up.AcctNo == acctNo);

                // If the account is not found, throw an exception
                if (account == null)
                {
                    throw new MissingAccountException($"'{acctNo}'");
                }

                // Log successful retrieval of account details
                Log($"Successful retrieval of account details: '{acctNo}'", LogLevel.Information);
                return Ok(account);
            }
            catch (DataGenerationFailException ex)
            {
                // Log and return NotFound for DataGenerationFailException
                Log(ex.Message, LogLevel.Critical);
                return NotFound();
            }
            catch (MissingAccountException ex)
            {
                // Log and return NoContent for MissingAccountException
                Log(ex.Message, LogLevel.Warning);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log and return BadRequest for other unknown exceptions
                Log(ex.Message, LogLevel.Critical);
                return BadRequest();
            }
        }

        /* 
         * Method: GetadminByName
         * Description: Retrieves an admin by their first name
         * Params:
         *   name: The first name of the admin to search for
         * Use: GET: api/admin/byname/Mike
         */
        [HttpGet("byname/{name}")]
        public async Task<ActionResult<Admin>> GetadminByName(string name)
        {
            try
            {
                // Log the attempt to retrieve admin details
                Log($"Attempt to get admin details: '{name}'", LogLevel.Information);

                // Check if Admins DbSet is null
                if (_context.Admins == null)
                {
                    throw new DataGenerationFailException("Admins");
                }
                // Find the admin by their first name
                var admin = await _context.Admins.FirstOrDefaultAsync(up => up.FName == name);

                // If the admin is not found, throw an exception
                if (admin == null)
                {
                    throw new MissingProfileException($"'{name}'");
                }

                // Log successful retrieval of admin details
                Log($"Successful retrieval of admin details: '{name}'", LogLevel.Information);
                return Ok(admin);
            }
            catch (DataGenerationFailException ex)
            {
                // Log and return NotFound for DataGenerationFailException
                Log(ex.Message, LogLevel.Critical);
                return NotFound();
            }
            catch (MissingProfileException ex)
            {
                // Log and return NoContent for MissingProfileException
                Log(ex.Message, LogLevel.Warning);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log and return BadRequest for other unknown exceptions
                Log(ex.Message, LogLevel.Critical);
                return BadRequest();
            }
        }

        /* 
         * Method: GetadminByEmail
         * Description: Retrieves an admin by their email address
         * Params:
         *   email: The email address of the admin to search for
         * Use: GET: api/admin/byemail/Mike@admin.bank.dc.au
         */
        [HttpGet("byemail/{email}")]
        public async Task<ActionResult> GetadminByEmail(string email)
        {
            try
            {
                // Log the attempt to retrieve admin details
                Log($"Attempt to get admin details: '{email}'", LogLevel.Information);
                // Check if Admins DbSet is null
                if (_context.Admins == null)
                {
                    throw new DataGenerationFailException("Admins");
                }
                // Find the admin by their email
                var admin = await _context.Admins.FirstOrDefaultAsync(up => up.Email == email);

                // If the admin is not found, throw an exception
                if (admin == null)
                {
                    throw new MissingProfileException($"'{email}'");
                }

                // Log successful retrieval of admin details
                Log($"Successful retrieval of admin details: '{email}'", LogLevel.Information);
                return Ok(admin);
            }
            catch (DataGenerationFailException ex)
            {
                // Log and return NotFound for DataGenerationFailException
                Log(ex.Message, LogLevel.Critical);
                return NotFound();
            }
            catch (MissingProfileException ex)
            {
                // Log and return NoContent for MissingProfileException
                Log(ex.Message, LogLevel.Warning);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log and return BadRequest for other unknown exceptions
                Log(ex.Message, LogLevel.Critical);
                return BadRequest();
            }
        }

        /* 
         * Method: GetadminById
         * Description: Retrieves an admin by their ID
         * Params:
         *   id: The ID of the admin to search for
         * Use: GET: api/admin/byid/2
         */
        [HttpGet("byid/{id}")]
        public async Task<ActionResult> GetadminById(int id)
        {
            try
            {
                // Log the attempt to retrieve admin details
                Log($"Attempt to get admin details: '{id}'", LogLevel.Information);
                // Check if Admins DbSet is null
                if (_context.Admins == null)
                {
                    throw new DataGenerationFailException("Admins");
                }
                // Find the admin by their ID
                var admin = await _context.Admins.FirstOrDefaultAsync(up => up.Id == id);

                // If the admin is not found, throw an exception
                if (admin == null)
                {
                    throw new MissingProfileException($"'{id}'");
                }

                // Log successful retrieval of admin details
                Log($"Successful retrieval of admin details: '{id}'", LogLevel.Information);
                return Ok(admin);
            }
            catch (DataGenerationFailException ex)
            {
                // Log and return NotFound for DataGenerationFailException
                Log(ex.Message, LogLevel.Critical);
                return NotFound();
            }
            catch (MissingProfileException ex)
            {
                // Log and return NoContent for MissingProfileException
                Log(ex.Message, LogLevel.Warning);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log and return BadRequest for other unknown exceptions
                Log(ex.Message, LogLevel.Critical);
                return BadRequest();
            }
        }

        /* 
         * Method: PutAccountProfile
         * Description: Updates an existing admin's details
         * Params:
         *   id: The ID of the admin to update
         *   admin: The updated admin details
         * Use: PUT: api/admin/update/1
         */
        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutAccountProfile(int id, [FromBody] Admin admin)
        {
            try
            {
                // Log the attempt to update admin details
                Log($"Attempt to update admin details: '{id}'", LogLevel.Information);
                // Check if Admins DbSet is null
                if (_context.Admins == null)
                {
                    throw new DataGenerationFailException("Admins");
                }

                // Check if the provided ID matches the admin's ID
                if (id != admin.Id)
                {
                    throw new MissingProfileException($"'{id}'");
                }

                // Mark the admin entity as modified
                _context.Entry(admin).State = EntityState.Modified;
                // Save changes to the database
                await _context.SaveChangesAsync();
                return Ok(admin);
            }
            catch (DataGenerationFailException ex)
            {
                // Log and return NotFound for DataGenerationFailException
                Log(ex.Message, LogLevel.Critical);
                return NotFound();
            }
            catch (MissingProfileException ex)
            {
                // Log and return BadRequest for MissingProfileException
                Log(ex.Message, LogLevel.Warning);
                return BadRequest();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency conflicts
                if (!AdminProfileExists(id))
                {
                    Log(ex.Message, LogLevel.Warning);
                    return NotFound();
                }
                else
                {
                    Log("Concurrency conflict detected.", LogLevel.Warning);
                    return StatusCode(StatusCodes.Status409Conflict, "Concurrency conflict");
                }
            }
            catch (Exception ex)
            {
                //Catch other unkown exceptions
                Log(ex.Message, LogLevel.Critical);
                return BadRequest();
            }
        }

        /* 
         * Method: AdminProfileExists
         * Description: Checks if an admin profile with the given ID exists
         * Params:
         *   id: The ID of the admin to check
         */
        private bool AdminProfileExists(int id)
        {
            // Check if any admin with the given ID exists in the database
            return (_context.Admins?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
