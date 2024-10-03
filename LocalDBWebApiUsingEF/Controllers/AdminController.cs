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
        private readonly DBManager _context;
        private readonly ILogger<AdminController> _logger;
        private static readonly object _logLock = new object();

        public AdminController(DBManager dBManager, ILogger<AdminController> logger)
        {
            _context = dBManager;
            _logger = logger;
        }

        private void Log(string message, LogLevel logLevel)
        {
            lock (_logLock)
            {
                string logEntry = $"{DateTime.Now}: {message}";

                _logger.Log(logLevel, logEntry);
            }
        }

        // GET: api/admin/byname/Mike
        [HttpGet("byname/{name}")]
        public async Task<ActionResult<Admin>> GetadminByName(string name)
        {
            try
            {
                Log($"Attempt to get admin details: '{name}'", LogLevel.Information);

                if (_context.Admins == null)
                {
                    throw new DataGenerationFailException("Admins");
                }
                var admin = await _context.Admins.FirstOrDefaultAsync(up => up.FName == name);

                if (admin == null)
                {
                    throw new MissingProfileException($"'{name}'");
                }

                Log($"Successful retrieval of admin details: '{name}'", LogLevel.Information);
                return Ok(admin);
            }
            catch (DataGenerationFailException ex)
            {
                Log(ex.Message, LogLevel.Critical);
                return NotFound(ex.Message);
            }
            catch (MissingProfileException ex)
            {
                Log(ex.Message, LogLevel.Warning);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log(ex.Message, LogLevel.Critical);
                return BadRequest(ex);
            }
        }

        // GET: api/admin/byemail/Mike@admin.bank.dc.au
        [HttpGet("byemail/{email}")]
        public async Task<ActionResult> GetadminByEmail(string email)
        {
            try
            {
                Log($"Attempt to get admin details: '{email}'", LogLevel.Information);
                if (_context.Admins == null)
                {
                    throw new DataGenerationFailException("Admins");
                }
                var admin = await _context.Admins.FirstOrDefaultAsync(up => up.Email == email);

                if (admin == null)
                {
                    throw new MissingProfileException($"'{email}'");
                }

                Log($"Successful retrieval of admin details: '{email}'", LogLevel.Information);
                return Ok(admin);
            }
            catch (DataGenerationFailException ex)
            {
                Log(ex.Message, LogLevel.Critical);
                return NotFound(ex.Message);
            }
            catch (MissingProfileException ex)
            {
                Log(ex.Message, LogLevel.Warning);
                return NoContent();
            }
            catch (Exception ex)
            {
                //Catch other unkown exceptions
                Log(ex.Message, LogLevel.Critical);
                return BadRequest(ex);
            }
        }
    }
}
