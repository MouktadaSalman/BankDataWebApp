/* 
 * Module: AccountGenerator
 * Description: Contains the AccountGenerator class for generating random account data
 * Author: Moukhtada
 * ID: 20640266
 * Version: 1.0.0.1
 */

namespace DataTierWebServer.Models
{
    public class AccountGenerator
    {
        // Random number generator 
        private static Random _random = new Random(1234);

        // User profile associated with the account generator
        private readonly UserProfile _userProfile;


        /* 
         * Constructor
         * Description: Initializes a new instance of the AccountGenerator class
         * Params:
         *   userProfile: The user profile associated with this account generator
         */
        public AccountGenerator (UserProfile userProfile)
        {
            _userProfile = userProfile;
        }


        /* 
         * Method: GenerateAcctNo
         * Description: Generates a random account number
         * Params: None
         */
        private static uint GenerateAcctNo()
        {
            return (uint)_random.Next(1, 10000);
        }


        

        /* 
         * Method: GeneratRandomBalance
         * Description: Generates a random account balance
         * Params: None
         */
        private static int GeneratRandomBalance()
        {
            return _random.Next(0, 100000);
        }

        /* 
         * Method: GenerateAccount
         * Description: Creates a new Account instance with randomly generated data
         * Params: None
         */
        public Account GenerateAccount()
        {
            return new Account(GenerateAcctNo(), "Savings Account", GeneratRandomBalance(), _userProfile.Id);
        }
    }
}