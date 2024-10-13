/* 
 * Module: DBManager
 * Description: Manages the database context and seeding for the Bank application
 * Author: Ahmed, Moukhtada, Jauhar
 * ID: 21467369, 20640266, 21494299
 * Version: 1.0.0.3
 */

using DataTierWebServer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataTierWebServer.Data
{
    public class DBManager : DbContext
    {
        /* 
         * Method: OnConfiguring
         * Description: Configures the database connection
         * Params:
         *   optionsBuilder: The DbContextOptionsBuilder to configure
         */
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = Bank.db;");
        }

        // DbSet for UserProfile entities
        public DbSet<UserProfile> UserProfiles { get; set; }

        // DbSet for Account entities
        public DbSet<Account> Accounts { get; set; }

        // DbSet for UserHistory entities
        public DbSet<UserHistory> UserHistories { get; set; }

        // DbSet for Admin entities
        public DbSet<Admin> Admins { get; set; }


        /* 
         * Method: OnModelCreating
         * Description: Seeds the database with initial data
         * Params:
         *   modelBuilder: The ModelBuilder to use for configuring the model
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<UserProfile> userProfiles = new List<UserProfile>();
            List<Account> accounts = new List<Account>();
            List<UserHistory> userHistory = new List<UserHistory>();
            List<Admin> admins = new List<Admin>();

            UserProfile user;
            Account account;
            AccountGenerator accountGenerator;

            Admin admin;

            // Generate admin data
            for (int i = 0; i < 3; i++)
            {
                admin = AdminGenerator.GetNextAdmin();
                admin.Id = i + 1;
                admins.Add(admin);
            }

            // Generate user and account data
            for (int i = 0; i < 10; i++)
            {
                user = new UserProfile();
                account = new Account();

                user = ProfleGenerator.GetNextAccount();
                user.Id = i + 1;
                userProfiles.Add(user);

                accountGenerator = new AccountGenerator(user);
                account = accountGenerator.GenerateAccount();
                accounts.Add(account);
            }

            // Using a random generator for creating some random transactions
            Random random = new Random(1234);
            int randomIndex;
            string[] transactionTypes = { "deposit", "withdraw", "send", "receive" };

            // Generate transaction history
            for (int i = 0; i < 10; i++)
            {
                int randomAmount = random.Next(-1000, 1000);
                randomIndex = random.Next(0, 10);
                string selectedType = transactionTypes[random.Next(transactionTypes.Length)];

                // Adjust the balance based on transaction type
                if (selectedType == "deposit" || selectedType == "receive")
                {
                    accounts[randomIndex].Balance += Math.Abs(randomAmount);
                }
                else
                {
                    accounts[randomIndex].Balance -= Math.Abs(randomAmount);
                }


                // Create a new history entry
                var historyEntry = new UserHistory
                {
                    Transaction = i + 1,  // Primary key for UserHistory
                    AccountId = accounts[randomIndex].AcctNo,
                    Amount = (selectedType == "withdraw" || selectedType == "send") ? -Math.Abs(randomAmount) : Math.Abs(randomAmount),
                    Type = (selectedType == "deposit" || selectedType == "withdraw") ?
                           ((randomAmount >= 0) ? "Deposit" : "Withdraw") :
                           selectedType == "send" ? "Send" : "Receive",
                    DateTime = DateTime.Now,
                    Sender = (selectedType == "send" || selectedType == "receive") ? accounts[random.Next(0, 10)].AcctNo : accounts[randomIndex].AcctNo,

                    // Generate a formatted history string based on the transaction type
                    HistoryString = (selectedType == "receive") ?
                        $"Account ID: {accounts[randomIndex].AcctNo} --- " +
                        $"Received: ${Math.Abs(randomAmount):F2} --- " +
                        $"Date and Time: {DateTime.Now:MMMM dd, yyyy HH:mm tt}" :
                        (selectedType == "send") ?
                        $"Account ID: {accounts[randomIndex].AcctNo} --- " +
                        $"Sent: ${Math.Abs(randomAmount):F2} --- " +
                        $"Date and Time: {DateTime.Now:MMMM dd, yyyy HH:mm tt}" :
                        ((randomAmount >= 0) ?
                        $"Account ID: {accounts[randomIndex].AcctNo} --- " +
                        $"Deposited: ${randomAmount:F2} --- " +
                        $"Date and Time: {DateTime.Now:MMMM dd, yyyy HH:mm tt}" :
                        $"Account ID: {accounts[randomIndex].AcctNo} --- " +
                        $"Withdrawn: ${Math.Abs(randomAmount):F2} --- " +
                        $"Date and Time: {DateTime.Now:MMMM dd, yyyy HH:mm tt}")
                };

                userHistory.Add(historyEntry);
            }

            // Seed the data to the tables
            modelBuilder.Entity<UserProfile>().HasData(userProfiles);
            modelBuilder.Entity<Account>().HasData(accounts);
            modelBuilder.Entity<UserHistory>().HasData(userHistory);
            modelBuilder.Entity<Admin>().HasData(admins);
        }

    }
}
