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

        public AdminController(DBManager dBManager, ILogger<AdminController> logger)
        {
            _context = dBManager;
            _logger = logger;
        }

        // GET: api/admin/byname/Mike
        [HttpGet("byname/{name}")]
        public async Task<ActionResult<Admin>> GetadminByName(string name)
        {
            try
            {
                if (_context.Admins == null)
                {
                    var ex = new DataGenerationFailException("Admins");

                    _logger.LogCritical(ex, $"{DateTime.Now.ToString()}: {ex.Message}");
                    return NotFound(ex);
                }
                var admin = await _context.Admins.FirstOrDefaultAsync(up => up.FName == name);

                if (admin == null)
                {
                    var ex = new MissingProfileException($"'{name}'");

                    _logger.LogWarning(ex, $"{DateTime.Now.ToString()}: {ex.Message}");
                    return NotFound(ex);
                }

                return Ok(admin);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/admin/byemail/Mike@admin.bank.dc.au
        [HttpGet("byemail/{email}")]
        public async Task<ActionResult> GetadminByEmail(string email)
        {
            try
            {
                if (_context.Admins == null)
                {
                    var ex = new DataGenerationFailException("Admins");

                    _logger.LogCritical(ex, $"{DateTime.Now.ToString()}: {ex.Message}");
                    return NotFound(ex);
                }
                var admin = await _context.Admins.FirstOrDefaultAsync(up => up.Email == email);

                if (admin == null)
                {
                    var ex = new MissingProfileException($"'{email}'");

                    _logger.LogWarning(ex, $"{DateTime.Now.ToString()}: {ex.Message}");
                    return NotFound(ex);
                }

                return Ok(admin);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
