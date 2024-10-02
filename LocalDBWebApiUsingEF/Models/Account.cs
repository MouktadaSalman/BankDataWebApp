using System.ComponentModel.DataAnnotations;

namespace DataTierWebServer.Models
{
    public class Account
    {
        [Key]
        public uint AcctNo { get; set; }
        public string? AccountName { get; set; }
        public int Balance { get; set; }
        public int UserId { get; set; }
        public ICollection<UserHistory> History { get; set; }

        public Account()
        {
            AcctNo = 0;
            AccountName = string.Empty;
            Balance = 0;
            History = new List<UserHistory>();
        }
        public Account(uint acctNo, string? accountName, int balance, int userId)
        {
            AcctNo = acctNo;
            Balance = balance;
            AccountName = accountName;
            History = new List<UserHistory>();
            UserId = userId;
        }
    }

    public class UserHistory
    {
        [Key]
        public int Transaction { get; set; }
        public uint AccountId { get; set; }
        public double Amount { get; set; }
        public string? Type { get; set; }
        public DateTime DateTime { get; set; }
        public uint Sender { get; set; }
        public string? HistoryString { get; set; }
    }
}
