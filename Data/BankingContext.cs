using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using System.Transactions;
using WebBankingApp.Models;

namespace WebBankingApp.Data
{
    public class BankingContext : DbContext
    { 
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Share> Shares { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Models.Transaction> Transactions { get; set; }
        public string DbPath { get; set; }

        public BankingContext() 
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "Banking.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Initializes all accounts
            Account account1 = new Account(1);
            Account account2 = new Account(2);
            Account account3 = new Account(3);
            Account account4 = new Account(4);
            Account account5 = new Account(5);

            // Creates and adds shares to account 1
            Share account1Share1 = new Share(1, "Savings", 25, .005M, account1, 500);
            account1Share1.AccountId = 1;
            Share account1Share2 = new Share(2, "Checking", 0, 0, account1, 100);
            Share account1Share3 = new Share(3, "Money Market", 1000, .1M, account1, 2000);

            // Creates and adds shares to account 2
            Share account2Share1 = new Share(4, "Savings", 25, .005M, account2, 2500);
            Share account2Share2 = new Share(5, "Checking", 0, 0, account2, 400);

            // Creates and adds shares to account 3
            Share account3Share1 = new Share(6, "Savings", 25, .005M, account3, 1500);

            // Creates and adds shares to account 4
            Share account4Share1 = new Share(7, "Savings", 25, .005M, account4, 25);
            Share account4Share2 = new Share(8, "Checking", 0, 0, account4, 325);
            Share account4Share3 = new Share(9, "Money Market", 1000, .1M, account4, 10000);

            // Creates and adds shares to account 5
            Share account5Share1 = new Share(10, "Savings", 25, .005M, account5, 2500);
            Share account5Share2 = new Share(11, "Money Market", 1000, .1M, account5, 400);

            //// Creates Members
            Member member1 = new Member(1, "Draven McConathy", new(2001, 5, 18), 123456789);
            Member member2 = new Member(2, "Mercedes Helliangao", new(2002, 3, 12), 987654321);
            Member member3 = new Member(3, "Terry Pratchet", new(1987, 5, 1), 278465792);
            Member member4 = new Member(4, "Harry Dresdon", new(1950, 10, 13), 098764245);
            Member member5 = new Member(5, "Adam Littlefinger", new(2004, 1, 27), 098536678);
            Member member6 = new Member(6, "Mary Littlefinger", new(1979, 8, 19), 444886578);

            //// AccountMember association
            AccountMember accountMember1 = new AccountMember(1, 1, account1, 1, member1);
            AccountMember accountMember2 = new AccountMember(2, 1, account1, 2, member2);
            AccountMember accountMember3 = new AccountMember(3, 2, account2, 2, member2);
            AccountMember accountMember4 = new AccountMember(4, 3, account3, 3, member3);
            AccountMember accountMember5 = new AccountMember(5, 4, account4, 4, member4);
            AccountMember accountMember6 = new AccountMember(6, 5, account5, 4, member4);
            AccountMember accountMember7 = new AccountMember(7, 5, account5, 5, member5);
            AccountMember accountMember8 = new AccountMember(8, 5, account5, 6, member6);


            modelBuilder.Entity<Account>().HasData(account1, account2, account3, account4, account5);

            modelBuilder.Entity<Share>().HasData(account1Share1, account1Share2, account1Share3,
                account2Share1, account2Share2, account3Share1, account4Share1, account4Share2,
                account4Share3, account5Share1, account5Share2);

            modelBuilder.Entity<AccountMember>().HasData(accountMember1, accountMember2, accountMember3,
                accountMember4, accountMember5, accountMember6, accountMember7, accountMember8);

            modelBuilder.Entity<Member>().HasData(member1, member2, member3, member4, member5, member6);

        }
    }
}
