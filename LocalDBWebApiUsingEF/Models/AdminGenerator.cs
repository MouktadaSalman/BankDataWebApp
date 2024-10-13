/* 
 * Module: AdminGenerator
 * Description: Contains the AdminGenerator class for generating random admin data
 * Author: Moukhtada
 * ID: 20640266
 * Version: 1.0.0.1
 */

using System.Text;

namespace DataTierWebServer.Models
{
    public class AdminGenerator
    {
        // Random number generator 
        private static readonly Random _random = new Random(1234);

        // HashSets in which we store the generated data to different attributes of the account
        private static HashSet<string> emailStrings = new HashSet<string>();
        private static HashSet<string> usernameStrings = new HashSet<string>();
        private static HashSet<string> passwordStrings = new HashSet<string>();

        // List of first names
        private static readonly List<string> _firstNames = new List<string>
        {
            "James", "Mary", "John", "Patricia", "Robert", "Jennifer", "Michael", "Linda",
            "William", "Elizabeth", "David", "Barbara", "Richard", "Susan", "Joseph", "Jessica",
            "Thomas", "Sarah", "Charles", "Karen", "Christopher", "Nancy", "Daniel", "Lisa",
            "Matthew", "Margaret", "Anthony", "Betty", "Donald", "Sandra", "Mark", "Ashley",
            "Paul", "Dorothy", "Steven", "Kimberly", "Andrew", "Emily", "Kenneth", "Donna",
            "Joshua", "Michelle", "Kevin", "Carol", "Brian", "Amanda", "George", "Melissa",
            "Edward", "Deborah", "Ronald", "Stephanie", "Timothy", "Rebecca", "Jason", "Laura",
            "Jeffrey", "Sharon", "Ryan", "Cynthia", "Jacob", "Kathleen", "Gary", "Amy",
            "Nicholas", "Shirley", "Eric", "Angela", "Stephen", "Helen", "Jonathan", "Anna",
            "Larry", "Brenda", "Justin", "Pamela", "Scott", "Nicole", "Brandon", "Emma",
            "Benjamin", "Samantha", "Samuel", "Katherine", "Frank", "Christine", "Gregory", "Debra",
            "Raymond", "Rachel", "Alexander", "Catherine", "Patrick", "Carolyn", "Jack", "Janet",
            "Dennis", "Ruth", "Jerry", "Maria"
        };

        // List of last names
        private static readonly List<string> _lastNames = new List<string>
        {
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis",
            "Rodriguez", "Martinez", "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas",
            "Taylor", "Moore", "Jackson", "Martin", "Lee", "Perez", "Thompson", "White",
            "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson", "Walker", "Young",
            "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores",
            "Green", "Adams", "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell",
            "Carter", "Roberts", "Gomez", "Phillips", "Evans", "Turner", "Diaz", "Parker",
            "Cruz", "Edwards", "Collins", "Reyes", "Stewart", "Morris", "Morales", "Murphy",
            "Cook", "Rogers", "Gutierrez", "Ortiz", "Morgan", "Cooper", "Peterson", "Bailey",
            "Reed", "Kelly", "Howard", "Ramos", "Kim", "Cox", "Ward", "Richardson",
            "Watson", "Brooks", "Chavez", "Wood", "James", "Bennett", "Gray", "Mendoza",
            "Ruiz", "Hughes", "Price", "Alvarez", "Castillo", "Sanders", "Patel", "Myers",
            "Long", "Ross", "Foster", "Jimenez"
        };

        /* 
         * Method: GetFirstname
         * Description: Retrieves a random first name from the predefined list
         * Params: None
         */
        private static string GetFirstname()
        {
            return _firstNames[_random.Next(_firstNames.Count)];
        }

        /* 
         * Method: GetLastname
         * Description: Retrieves a random last name from the predefined list
         * Params: None
         */
        private static string GetLastname()
        {
            return _lastNames[_random.Next(_lastNames.Count)];
        }

        /* 
         * Method: GetUniqueEmail
         * Description: Generates a unique email address for an admin
         * Params: 
         *   firstName:The admin's first name
         *   lastName: The admin's last name
         */
        private static string GetUniqueEmail(string firstName, string lastName)
        {
            string randomEmail;
            do
            {
                randomEmail = GetRandomString(firstName, lastName);
            } while (!emailStrings.Add(randomEmail));

            randomEmail += "@admin.bank.dc.au";

            return randomEmail;
        }

        /* 
         * Method: GetUniqueUsername
         * Description: Generates a unique username for an admin
         * Params: 
         *   firstName: The admin's first name
         *   lastName: The admin's last name
         */
        private static string GetUniqueUsername(string firstName, string lastName)
        {
            string randomEmail;
            do
            {
                randomEmail = GetRandomString(firstName, lastName);
            } while (!usernameStrings.Add(randomEmail));

            return randomEmail;
        }

        /* 
         * Method: GetRandomString
         * Description: Generates a random string based on first name and last name
         * Params: 
         *   firstName: The admin's first name
         *   lastName: The admin's last name
         */
        private static string GetRandomString(string firstName, string lastName)
        {
            return firstName + "." + lastName + _random.Next(1, 1000);
        }

        /* 
         * Method: GetAge
         * Description: Generates a random age for an admin between 18 and 109
         * Params: None
         */
        private static uint GetAge()
        {
            return (uint)_random.Next(18, 110);
        }

        /* 
         * Method: GetPhoneNumber
         * Description: Generates a random phone number for an admin
         * Params: None
         */
        private static string GetPhoneNumber()
        {
            return "+961" + _random.Next(100, 1000) + _random.Next(1000, 10000);
        }

        /* 
         * Method: GetAddress
         * Description: Generates a random address for an admin
         * Params: None
         */
        private static string GetAddress()
        {
            int houseNumber = _random.Next(1, 1000);
            string street = _firstNames[_random.Next(_firstNames.Count)] + " " + _lastNames[_random.Next(_lastNames.Count)] + " St.";

            return houseNumber + " " + street;
        }

        /* 
         * Method: GetUniquePassword
         * Description: Generates a unique random password of specified length
         * Params: 
         *   length: The desired length of the password
         */
        public static string GetUniquePassword(int length)
        {
            string randomString;

            // Keep generating new strings until we get a unique one
            do
            {
                randomString = GenerateRandomPassword(length);
            } while (!passwordStrings.Add(randomString));

            return randomString;
        }

        /* 
         * Method: GenerateRandomPassword
         * Description: Generates a random password of specified length
         * Params: 
         *   length: The desired length of the password
         */
        private static string GenerateRandomPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(chars[_random.Next(chars.Length)]);
            }
            return result.ToString();
        }

        /* 
         * Method: GetNextAdmin
         * Description: Generates a new Admin instance with randomly generated data
         * Params: None
         */
        public static Admin GetNextAdmin()
        {
            Admin admin = new Admin();

            admin.Age = GetAge();
            admin.FName = GetFirstname();
            admin.LName = GetLastname();
            admin.PhoneNumber = GetPhoneNumber();
            admin.Username = GetUniqueUsername(admin.FName, admin.LName);
            admin.Email = GetUniqueEmail(admin.FName, admin.LName);
            admin.Address = GetAddress();
            admin.Password = GetUniquePassword(12);

            return admin;
        }
    }
}