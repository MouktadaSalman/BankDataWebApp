/* 
 * Module: Account
 * Description: Contains model classes for Account and UserHistory which is an attribute of an Account
 * Author: Ahmed, Moukhtada
 * ID: 21467369, 20640266
 * Version: 1.0.0.1
 */

using System.ComponentModel.DataAnnotations;

namespace DataTierWebServer.Models
{
    public class Account
    {
        [Key]
        // Unique identifier for the account, as the primary key
        public uint AcctNo { get; set; }

        // Name of the account, can be null
        public string? AccountName { get; set; }

        // Current balance of the account
        public int Balance { get; set; }

        // ID of the user who owns this account
        public int UserId { get; set; }

        // Collection of transaction history entries for this account
        public ICollection<UserHistory> History { get; set; }

        // Default constructor
        // Description: Initializes an empty account with default values
        // Params: None
        public Account()
        {
            AcctNo = 0;
            AccountName = string.Empty;
            Balance = 0;
            History = new List<UserHistory>();
        }

        // Parameterized constructor
        // Description: Creates an account with specified initial values
        // Params:
        //   acctNo: The account number
        //   accountName: The name of the account
        //   balance: The initial balance
        //   userId: The ID of the user who owns this account
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
        // Unique identifier for this transaction, as the primary key
        public int Transaction { get; set; }

        // ID of the account associated with this transaction
        public uint AccountId { get; set; }

        // Amount involved in the transaction
        public double Amount { get; set; }

        // Type of transaction (deposit, withdrawal, sent , recieved), can be null
        public string? Type { get; set; }

        // Date and time when the transaction occurred
        public DateTime DateTime { get; set; }

        // Account number of the sender (if applicable)
        public uint Sender { get; set; }

        // String representation of the transaction history, can be null
        public string? HistoryString { get; set; }
    }
}
