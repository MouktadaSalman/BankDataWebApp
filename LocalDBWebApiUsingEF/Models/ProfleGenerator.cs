using System.Text;
using System;

namespace DataTierWebServer.Models
{
    public class ProfleGenerator
    {
        private static readonly Random _random = new Random(1234);
        private static HashSet<string> emailStrings = new HashSet<string>();
        private static HashSet<string> usernameStrings = new HashSet<string>();
        private static HashSet<string> passwordStrings = new HashSet<string>();

        // Lists of actual names
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

        private static string GetFirstname()
        {
            return _firstNames[_random.Next(_firstNames.Count)];
        }

        private static string GetLastname()
        {
            return _lastNames[_random.Next(_lastNames.Count)];
        }

        private static string GetUniqueEmail(string firstName, string lastName)
        {
            string randomEmail;
            do
            {
                randomEmail = GetRandomString(firstName, lastName);
            } while (!emailStrings.Add(randomEmail));

            randomEmail += "@gmail.com";

            return randomEmail;
        }

        private static string GetUniqueUsername(string firstName, string lastName)
        {
            string randomEmail;
            do
            {
                randomEmail = GetRandomString(firstName, lastName);
            } while (!usernameStrings.Add(randomEmail));

            return randomEmail;
        }

        private static string GetRandomString(string firstName, string lastName)
        {
            return firstName + "." + lastName + _random.Next(1, 1000);
        }

        private static uint GetAge()
        {
            return (uint)_random.Next(18, 110);
        }

        private static string GetPhoneNumber()
        {
            return "+961" + _random.Next(100, 1000) + _random.Next(1000, 10000);
        }

        private static string GetAddress() 
        {
            int houseNumber = _random.Next(1, 1000);
            string street = _firstNames[_random.Next(_firstNames.Count)] + " " + _lastNames[_random.Next(_lastNames.Count)] + " St.";

            return houseNumber + " " + street;
        }

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

        public static UserProfile GetNextAccount()
        {
            UserProfile userProfile = new UserProfile();

            userProfile.Age = GetAge();
            userProfile.FName = GetFirstname();
            userProfile.LName = GetLastname();
            userProfile.PhoneNumber = GetPhoneNumber();
            userProfile.Username = GetUniqueUsername(userProfile.FName, userProfile.LName);
            userProfile.Email = GetUniqueEmail(userProfile.FName, userProfile.LName);
            userProfile.Address = GetAddress(); 
            userProfile.Password = GetUniquePassword(12);

            return userProfile;
        }

        public int NumOfUserProfiles()
        {
            return _random.Next(10, 100);
        }
    }
}