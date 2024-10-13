/*
 * Module: BankAccount
 * Description: Contains model classes for BankAccount and UserHistory which is an attribute of a BankAccount
 * Author: Ahmed, Moukhtada
 * ID: 21467369, 20640266
 * Version: 1.0.0.1
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDataLB
{
    public class BankAccount
    {
        // Unique identifier for the account
        public uint AcctNo { get; set; }

        // Name of the account
        public string AccountName { get; set; }

        // Current balance of the account
        public int Balance { get; set; }

        // ID of the user who owns this account
        public int UserId { get; set; }

        // Collection of transaction history entries for this account
        public ICollection<UserHistory> History { get; set; }
    }

    public class UserHistory
    {
        // Unique identifier for this transaction
        public int Transaction { get; set; }

        // ID of the account associated with this transaction
        public uint AccountId { get; set; }

        // Amount involved in the transaction
        public double Amount { get; set; }

        // Type of transaction (deposit, withdrawal, sent, received)
        public string Type { get; set; }

        // Date and time when the transaction occurred
        public DateTime DateTime { get; set; }

        // Account number of the sender
        public uint Sender { get; set; }

        // String representation of the transaction history
        public string HistoryString { get; set; }
    }
}
