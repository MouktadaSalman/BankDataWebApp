namespace DataTierWebServer.Models
{
    public class AccountGenerator
    {

        private static Random _random = new Random(1234);


        private readonly UserProfile _userProfile;

        public AccountGenerator (UserProfile userProfile)
        {
            _userProfile = userProfile;
        }

        private static uint GenerateAcctNo()
        {
            return (uint)_random.Next(1, 10000);
        }

        private static int GeneratRandomBalance()
        {
            return (int)_random.Next(0, 100000);
        }

        public Account GenerateAccount()
        {
            return new Account(GenerateAcctNo(), _userProfile.FName , _userProfile.LName, _userProfile.Email, _userProfile.Age, GeneratRandomBalance(), _userProfile.Address);
        }

    }
}
