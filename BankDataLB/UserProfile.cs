using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDataLB
{
    public class UserProfile
    {
        // Unique identifier for the user
        public int Id { get; set; }

        // First name of the user
        public string FName { get; set; }

        // Last name of the user
        public string LName { get; set; }

        // Email address of the user 
        public string Email { get; set; }

        // Username for user login 
        public string Username { get; set; }

        // Age of the user
        public uint Age { get; set; }

        // Physical address of the user
        public string Address { get; set; }

        // Contact phone number for the user
        public string PhoneNumber { get; set; }

        // URL to the user's profile picture
        public string ProfilePictureUrl { get; set; }

        // Password for user login (should be stored securely)
        public string Password { get; set; }

        // Associated bank account for the user
        public BankAccount Account { get; set; }
    }
}
