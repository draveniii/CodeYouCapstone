﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebBankingApp.Data;

#nullable disable

namespace MVCWebBanking.Migrations
{
    [DbContext(typeof(BankingContext))]
    [Migration("20241213023446_Updated_Full_Database_Seeding")]
    partial class Updated_Full_Database_Seeding
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("WebBankingApp.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            Id = 1
                        },
                        new
                        {
                            Id = 2
                        },
                        new
                        {
                            Id = 3
                        },
                        new
                        {
                            Id = 4
                        },
                        new
                        {
                            Id = 5
                        });
                });

            modelBuilder.Entity("WebBankingApp.Models.AccountMember", b =>
                {
                    b.Property<int>("AccountMemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MemberId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AccountMemberId");

                    b.HasIndex("AccountId");

                    b.HasIndex("MemberId");

                    b.ToTable("AccountMember");

                    b.HasData(
                        new
                        {
                            AccountMemberId = 1,
                            AccountId = 1,
                            MemberId = 1
                        },
                        new
                        {
                            AccountMemberId = 2,
                            AccountId = 1,
                            MemberId = 2
                        },
                        new
                        {
                            AccountMemberId = 3,
                            AccountId = 2,
                            MemberId = 2
                        },
                        new
                        {
                            AccountMemberId = 4,
                            AccountId = 3,
                            MemberId = 3
                        },
                        new
                        {
                            AccountMemberId = 5,
                            AccountId = 4,
                            MemberId = 4
                        },
                        new
                        {
                            AccountMemberId = 6,
                            AccountId = 5,
                            MemberId = 4
                        },
                        new
                        {
                            AccountMemberId = 7,
                            AccountId = 5,
                            MemberId = 5
                        },
                        new
                        {
                            AccountMemberId = 8,
                            AccountId = 5,
                            MemberId = 6
                        });
                });

            modelBuilder.Entity("WebBankingApp.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SSNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Members");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateOfBirth = new DateOnly(2001, 5, 18),
                            Name = "Draven McConathy",
                            SSNumber = 123456789
                        },
                        new
                        {
                            Id = 2,
                            DateOfBirth = new DateOnly(2002, 3, 12),
                            Name = "Mercedes Helliangao",
                            SSNumber = 987654321
                        },
                        new
                        {
                            Id = 3,
                            DateOfBirth = new DateOnly(1987, 5, 1),
                            Name = "Terry Pratchet",
                            SSNumber = 278465792
                        },
                        new
                        {
                            Id = 4,
                            DateOfBirth = new DateOnly(1950, 10, 13),
                            Name = "Harry Dresdon",
                            SSNumber = 98764245
                        },
                        new
                        {
                            Id = 5,
                            DateOfBirth = new DateOnly(2004, 1, 27),
                            Name = "Adam Littlefinger",
                            SSNumber = 98536678
                        },
                        new
                        {
                            Id = 6,
                            DateOfBirth = new DateOnly(1979, 8, 19),
                            Name = "Mary Littlefinger",
                            SSNumber = 444886578
                        });
                });

            modelBuilder.Entity("WebBankingApp.Models.Share", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("CurrentBalance")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("InterestRate")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("MinimumBalance")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Shares");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountId = 1,
                            CurrentBalance = 500m,
                            InterestRate = 0.005m,
                            MinimumBalance = 25m,
                            Type = "Savings"
                        },
                        new
                        {
                            Id = 2,
                            AccountId = 1,
                            CurrentBalance = 100m,
                            InterestRate = 0m,
                            MinimumBalance = 0m,
                            Type = "Checking"
                        },
                        new
                        {
                            Id = 3,
                            AccountId = 1,
                            CurrentBalance = 2000m,
                            InterestRate = 0.1m,
                            MinimumBalance = 1000m,
                            Type = "Money Market"
                        },
                        new
                        {
                            Id = 4,
                            AccountId = 2,
                            CurrentBalance = 2500m,
                            InterestRate = 0.005m,
                            MinimumBalance = 25m,
                            Type = "Savings"
                        },
                        new
                        {
                            Id = 5,
                            AccountId = 2,
                            CurrentBalance = 400m,
                            InterestRate = 0m,
                            MinimumBalance = 0m,
                            Type = "Checking"
                        },
                        new
                        {
                            Id = 6,
                            AccountId = 3,
                            CurrentBalance = 1500m,
                            InterestRate = 0.005m,
                            MinimumBalance = 25m,
                            Type = "Savings"
                        },
                        new
                        {
                            Id = 7,
                            AccountId = 4,
                            CurrentBalance = 25m,
                            InterestRate = 0.005m,
                            MinimumBalance = 25m,
                            Type = "Savings"
                        },
                        new
                        {
                            Id = 8,
                            AccountId = 4,
                            CurrentBalance = 325m,
                            InterestRate = 0m,
                            MinimumBalance = 0m,
                            Type = "Checking"
                        },
                        new
                        {
                            Id = 9,
                            AccountId = 4,
                            CurrentBalance = 10000m,
                            InterestRate = 0.1m,
                            MinimumBalance = 1000m,
                            Type = "Money Market"
                        },
                        new
                        {
                            Id = 10,
                            AccountId = 5,
                            CurrentBalance = 100m,
                            InterestRate = 0.005m,
                            MinimumBalance = 25m,
                            Type = "Savings"
                        },
                        new
                        {
                            Id = 11,
                            AccountId = 5,
                            CurrentBalance = 4000m,
                            InterestRate = 0.1m,
                            MinimumBalance = 1000m,
                            Type = "Money Market"
                        });
                });

            modelBuilder.Entity("WebBankingApp.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("NewBalance")
                        .HasColumnType("TEXT");

                    b.Property<int>("ShareId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ShareId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("WebBankingApp.Models.AccountMember", b =>
                {
                    b.HasOne("WebBankingApp.Models.Account", "Account")
                        .WithMany("Members")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebBankingApp.Models.Member", "Member")
                        .WithMany("Accounts")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("WebBankingApp.Models.Share", b =>
                {
                    b.HasOne("WebBankingApp.Models.Account", "Account")
                        .WithMany("Shares")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("WebBankingApp.Models.Transaction", b =>
                {
                    b.HasOne("WebBankingApp.Models.Share", "Share")
                        .WithMany("Transactions")
                        .HasForeignKey("ShareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Share");
                });

            modelBuilder.Entity("WebBankingApp.Models.Account", b =>
                {
                    b.Navigation("Members");

                    b.Navigation("Shares");
                });

            modelBuilder.Entity("WebBankingApp.Models.Member", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("WebBankingApp.Models.Share", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
