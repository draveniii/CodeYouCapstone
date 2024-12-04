using Microsoft.EntityFrameworkCore;
using System.Transactions;
using WebBankingApp.Models;

namespace WebBankingApp.Data
{
    public class BankingContext : DbContext
    { 
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Models.Transaction> Transactions { get; set; }
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
    }
}
