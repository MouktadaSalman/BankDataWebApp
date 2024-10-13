/* 
 * Module: Admin
 * Description: Contains the Admin class representing administrator information
 * Author: Jauhar
 * ID: 21494299
 * Version: 1.0.0.1
 */

namespace DataTierWebServer.Models
{
    public class Admin
    {
        // Unique identifier for the admin
        public int Id { get; set; }

        // First name of the admin
        public string? FName { get; set; }

        // Last name of the admin
        public string? LName { get; set; }

        // Email address of the admin
        public string? Email { get; set; }

        // Username for admin login
        public string? Username { get; set; }

        // Age of the admin
        public uint Age { get; set; }

        // Physical address of the admin
        public string? Address { get; set; }

        // Contact phone number for the admin
        public string? PhoneNumber { get; set; }

        // URL to the admin's profile picture
        public string? ProfilePictureUrl { get; set; }

        // Password for admin login
        public string? Password { get; set; }
    }
}
