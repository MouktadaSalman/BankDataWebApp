/* 
 * Module: UserProfile
 * Description: Contains the UserProfile class representing user information
 * Author: Ahmed
 * ID: 21467369
 * Version: 1.0.0.1
 */

namespace DataTierWebServer.Models
{
    public class UserProfile
    {
        // Unique identifier for the user
        public int Id { get; set; }

        // First name of the user
        public string? FName { get; set; }

        // Last name of the user
        public string? LName { get; set; }

        // Email address of the user (Could be used for retreiving the user profile)
        public string? Email { get; set; }

        // Username for user login ( Could be used for retreiving the user profile)
        public string? Username { get; set; }

        // Age of the user
        public uint Age { get; set; }

        // Physical address of the user
        public string? Address { get; set; }

        // Contact phone number for the user
        public string? PhoneNumber { get; set; }

        // URL to the user's profile picture
        public string? ProfilePictureUrl { get; set; }

        // Password for user login (should be stored securely)
        public string? Password { get; set; }

        // Associated bank account for the user
        public Account? Account { get; set; }
    }
}
