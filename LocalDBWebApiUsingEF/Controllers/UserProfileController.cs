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

namespace DataTierWebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly DBManager _context;

        public UserProfileController(DBManager context)
        {
            _context = context;
        }

        // GET: api/userprofile
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetUserProfile()
        {
          if (_context.UserProfiles == null)
          {
              return NotFound();
          }
            return await _context.UserProfiles.ToListAsync();
        }

        // GET: api/userprofile/byname/Mike
        [HttpGet("byname/{name}")]
        public async Task<ActionResult<UserProfile>> GetUserProfileByName(string name)
        {
            try
            {
                if (_context.UserProfiles == null)
                {
                    return NotFound();
                }
                var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.FName == name);

                if (userProfile == null)
                {
                    return NotFound();
                }

                return Ok(userProfile);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        // GET: api/userprofile/byemail/Mike
        [HttpGet("byemail/{email}")]
        public async Task<ActionResult<UserProfile>> GetUserProfileByEmail(string email)
        {
            try
            {
                if (_context.UserProfiles == null)
                {
                    return NotFound();
                }
                var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.Email == email);

                if (userProfile == null)
                {
                    return NotFound();
                }

                return Ok(userProfile);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/userprofile/1
        /* BODY -> row -> Enter new updated userprofile for the id
         * {
            "id": 1, 
            "name": "Sajib Updated",
            "email": "sajib_updated@email.com",
            "address": "1 Street Suburb Updated",
            "phoneNumber": 9876543,
            "profilePictureUrl": "UpdatedImageUrl",
            "password": "UpdatedPassword"
        }
        */
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserProfile(int id, [FromBody] UserProfile userProfile)
        {
            if (id != userProfile.Id)
            {
                return BadRequest();
            }

            _context.Entry(userProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProfileExists(id))
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

        // POST: api/userprofile
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserProfile>> PostUserProfile([FromBody] UserProfile userProfile)
        {
            if (_context.UserProfiles == null)
            {
                return Problem("Entity set 'DBManager.UserProfiles'  is null.");
            }
            
            _context.UserProfiles.Add(userProfile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserProfile", new { id = userProfile.Id }, userProfile);
        }

        // DELETE: api/userprofile/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserProfile(int id)
        {
            if (_context.UserProfiles == null)
            {
                return NotFound();
            }
            var userProfile = await _context.UserProfiles.FindAsync(id);
            if (userProfile == null)
            {
                return NotFound();
            }

            _context.UserProfiles.Remove(userProfile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserProfileExists(int id)
        {
            return (_context.UserProfiles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
