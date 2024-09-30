using System.ComponentModel.DataAnnotations;

namespace DataTierWebServer.Models
{
    public class Account
    {
        [Key]
        public uint AcctNo { get; set; }
        public string FirstName { get; set; } //get from userprofile
        public string LastName { get; set; } //get from userprofile
        public string Email { get; set; } //get from userprofile
        public uint Age { get; set; } //get from userprofile
        public int Balance { get; set; } 
        public string Address { get; set; } //get from userprofile
        public ICollection<UserHistory> History { get; set; }

        public Account()
        {
            AcctNo = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Age = 0;
            Balance = 0;
            Address = string.Empty;
            History = new List<UserHistory>();
        }
        public Account(uint acctNo, string? firstName, string? lastName,string? email, uint age, int balance, string? address)
        {
            AcctNo = acctNo;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Age = age;
            Balance = balance;
            Address = address;
            History = new List<UserHistory>();
        }
    }

    public class UserHistory
    {
        [Key]
        public int Transaction { get; set; }
        public uint AccountId { get; set; }
        public string? HistoryString { get; set; }        
    }
}
