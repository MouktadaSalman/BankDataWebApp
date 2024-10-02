using DataTierWebServer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataTierWebServer.Data
{
    public class DBManager : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = Bank.db;");
        }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<UserHistory> UserHistories { get; set; }

        public DbSet<Admin> Admins { get; set; }


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

            for (int i = 0; i < 3; i++)
            {
                admin = AdminGenerator.GetNextAdmin();
                admin.Id = i + 1;
                admins.Add(admin);
            }

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

            Random random = new Random(1234);
            int randomIndex;
            for (int i = 0; i < 10; i++)
            {
                int randomAmount = random.Next(-1000, 1000);
                randomIndex = random.Next(0, 10);
                accounts[randomIndex].Balance += randomAmount;

                var historyEntry = new UserHistory
                {
                    Transaction = i + 1,  // Primary key for UserHistory
                    AccountId = accounts[randomIndex].AcctNo,  // Foreign key linking to Account
                    HistoryString = $"Balance updated by {randomAmount} on {DateTime.Now}.  | " +
                    $"Old Balance: {accounts[randomIndex].Balance - randomAmount} ----- New Balance: {accounts[randomIndex].Balance}"
                };
                userHistory.Add(historyEntry);
            }

            modelBuilder.Entity<UserProfile>().HasData(userProfiles);
            modelBuilder.Entity<Account>().HasData(accounts);
            modelBuilder.Entity<UserHistory>().HasData(userHistory);
            modelBuilder.Entity<Admin>().HasData(admins);
        }
    }
}
