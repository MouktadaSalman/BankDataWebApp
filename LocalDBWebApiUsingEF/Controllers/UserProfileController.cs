/* 
 * Module: UserProfileController
 * Description: Handles HTTP requests related to user profile management
 * Author: Ahmed, Moukhtada, Jauhar
 * ID: 21467369, 20640266, 21494299
 * Version: 1.0.0.1
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

namespace DataTierWebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        // Database context for data operations
        private readonly DBManager _context;

        /* 
         * Method: UserProfileController
         * Description: Constructor for the UserProfileController class
         * Params:
         *   context: The DBManager instance for database operations
         * Use: Constructor for dependency injection
         */
        public UserProfileController(DBManager context)
        {
            _context = context;
        }

        /* 
         * Method: GetUserProfile
         * Description: Retrieves all user profiles
         * Params: None
         * Use: GET: api/userprofile
         */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetUserProfile()
        {
            if (_context.UserProfiles == null)
            {
                return NotFound();
            }
            return await _context.UserProfiles.ToListAsync();
        }

        /* 
         * Method: GetUserProfileByName
         * Description: Retrieves a user profile by the user's first name
         * Params:
         *   name: The first name of the user to search for
         * Use: GET: api/userprofile/byname/Mike
         */
        [HttpGet("byname/{name}")]
        public async Task<ActionResult<UserProfile>> GetUserProfileByName(string name)
        {
            try
            {
                // Check if UserProfiles DbSet is null
                if (_context.UserProfiles == null)
                {
                    return NotFound();
                }
                // Find the user profile by first name
                var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.FName == name);

                // If the user profile is not found, return NotFound
                if (userProfile == null)
                {
                    return NotFound();
                }

                return Ok(userProfile);
            }
            catch (Exception ex)
            {
                // Return BadRequest with the exception message for any errors
                return BadRequest(ex.Message);
            }
        }

        /* 
         * Method: GetUserProfileByEmail
         * Description: Retrieves a user profile by the user's email address
         * Params:
         *   email: The email address of the user to search for
         * Use: GET: api/userprofile/byemail/Mike@example.com
         */
        [HttpGet("byemail/{email}")]
        public async Task<ActionResult<UserProfile>> GetUserProfileByEmail(string email)
        {
            try
            {
                // Check if UserProfiles DbSet is null
                if (_context.UserProfiles == null)
                {
                    return NotFound();
                }
                // Find the user profile by email
                var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.Email == email);

                // If the user profile is not found, return NotFound
                if (userProfile == null)
                {
                    return NotFound();
                }

                return Ok(userProfile);
            }
            catch (Exception ex)
            {
                // Return BadRequest with the exception message for any errors
                return BadRequest(ex.Message);
            }
        }

        /* 
         * Method: PutUserProfile
         * Description: Updates an existing user profile
         * Params:
         *   id: The ID of the user profile to update
         *   userProfile: The updated user profile details
         * Use: PUT: api/userprofile/1
         * BODY -> row -> Enter new updated userprofile for the id
         * {
         *   "id": 2442, 
         *   "fname": "Ahmed Updated",
         *   "lname": "Youseif Updated",
         *   "email": "ay_updated@email.com",
         *   "username": "ayAhmedo",
         *   "age": 20,
         *   "address": "1 Street Suburb Updated",
         *   "phoneNumber": 9876543,
         *   "profilePictureUrl": "UpdatedImageUrl",
         *   "password": "UpdatedPassword"
         * }
         * (Note: You can also modify only the attributes you want, for example you can only update the password)
         */
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserProfile(int id, [FromBody] UserProfile userProfile)
        {
            // Check if the provided ID matches the user profile's ID
            if (id != userProfile.Id)
            {
                return BadRequest();
            }

            // Mark the user profile entity as modified
            _context.Entry(userProfile).State = EntityState.Modified;

            try
            {
                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // If the user profile doesn't exist, return NotFound
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

        /* 
         * Method: PostUserProfile
         * Description: Creates a new user profile
         * Params:
         *   userProfile: The user profile details to create
         * Use: POST: api/userprofile
         */
        [HttpPost]
        public async Task<ActionResult<UserProfile>> PostUserProfile([FromBody] UserProfile userProfile)
        {
            // Check if UserProfiles DbSet is null
            if (_context.UserProfiles == null)
            {
                return Problem("Entity set 'DBManager.UserProfiles'  is null.");
            }

            // Add the new user profile to the context
            _context.UserProfiles.Add(userProfile);
            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return a CreatedAtAction result
            return CreatedAtAction("GetUserProfile", new { id = userProfile.Id }, userProfile);
        }

        /* 
         * Method: DeleteUserProfile
         * Description: Deletes an existing user profile
         * Params:
         *   id: The ID of the user profile to delete
         * Use: DELETE: api/userprofile/5
         */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserProfile(int id)
        {
            // Check if UserProfiles DbSet is null
            if (_context.UserProfiles == null)
            {
                return NotFound();
            }
            // Find the user profile by ID
            var userProfile = await _context.UserProfiles.FindAsync(id);
            // If the user profile is not found, return NotFound
            if (userProfile == null)
            {
                return NotFound();
            }

            // Remove the user profile from the context
            _context.UserProfiles.Remove(userProfile);
            // Save changes to the database
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /* 
         * Method: UserProfileExists
         * Description: Checks if a user profile with the given ID exists
         * Params:
         *   id: The ID of the user profile to check
         */
        private bool UserProfileExists(int id)
        {
            // Check if any user profile with the given ID exists in the database
            return (_context.UserProfiles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
