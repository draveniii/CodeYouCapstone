﻿using MVCWebBanking.Models;

namespace WebBankingApp.Models
{
    public class Account
    {
        public Int32 Id { get; set; }
        //public ICollection<Member>? Members { get; set; }
        public List<AccountMember> Members { get; set; }
        public List<Share> Shares { get; } = new List<Share>();
        public List <Transaction> Transactions { get; } = new List<Transaction>();
    }
}
