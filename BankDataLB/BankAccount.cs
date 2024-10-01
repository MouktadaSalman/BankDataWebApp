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
        public string AccountName { get; set; }
        public int Balance { get; set; }
        public int UserId { get; set; }
        public ICollection<UserHistory> History { get; set; }
    }

    public class UserHistory
    {
        public int Transaction { get; set; }
        public uint AccountId { get; set; }
        public string HistoryString { get; set; }
    }
}
