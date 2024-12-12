﻿using Microsoft.EntityFrameworkCore;
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
            //modelBuilder.Entity<Account>().ToTable(nameof(Accounts))
            //    .HasMany(m => m.Members)
            //    .WithMany(a => a.Accounts);

            //modelBuilder.Entity<Account>().ToTable(nameof(Account))
            //    .HasMany(s => s.Shares)
            //    .WithMany(a => a.Accounts);

            //modelBuilder.Entity<Account>().ToTable(nameof(Account))
            //    .HasMany(t => t.Transactions)
            //    .WithMany(a => a.Accounts);
            base.OnModelCreating(modelBuilder);
        }
    }
}
