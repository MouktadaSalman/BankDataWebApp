using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankPresentationLayer
{
    public class TransactionInfo
    {
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }

        public string Type { get; set; }
    }
}
