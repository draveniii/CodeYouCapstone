﻿using Microsoft.EntityFrameworkCore;
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

        public BankingContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite();
        }
    }
}
