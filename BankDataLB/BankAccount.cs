using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDataLB
{
    public class BankAccount
    {
        public uint AcctNo { get; set; }
        public string FirstName { get; set; } //get from userprofile
        public string LastName { get; set; } //get from userprofile
        public string Email { get; set; } //get from userprofile
        public uint Age { get; set; } //get from userprofile
        public int Balance { get; set; }
        public string Address { get; set; } //get from userprofile
        public ICollection<UserHistory> History { get; set; }
    }

    public class UserHistory
    {
        public int Transaction { get; set; }
        public uint AccountId { get; set; }
        public string HistoryString { get; set; }
    }
}
