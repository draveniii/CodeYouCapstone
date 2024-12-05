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
    [Migration("20241205035233_Add_Share_Current_Balance")]
    partial class Add_Share_Current_Balance
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("MVCWebBanking.Models.AccountMember", b =>
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
                });

            modelBuilder.Entity("WebBankingApp.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
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
                });

            modelBuilder.Entity("WebBankingApp.Models.Share", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrentBalance")
                        .HasColumnType("INTEGER");

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
                });

            modelBuilder.Entity("WebBankingApp.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("ShareId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("ShareId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("MVCWebBanking.Models.AccountMember", b =>
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
                    b.HasOne("WebBankingApp.Models.Account", null)
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId");

                    b.HasOne("WebBankingApp.Models.Share", "Share")
                        .WithMany()
                        .HasForeignKey("ShareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Share");
                });

            modelBuilder.Entity("WebBankingApp.Models.Account", b =>
                {
                    b.Navigation("Members");

                    b.Navigation("Shares");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("WebBankingApp.Models.Member", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
