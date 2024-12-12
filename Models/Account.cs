﻿using MVCWebBanking.Models;

namespace WebBankingApp.Models
{
    public class Account
    {
        public virtual Int32 Id { get; set; }
        public virtual List<AccountMember> ?Members { get; set; }
        public virtual List<Share> ?Shares { get; } = new List<Share>();
    }
}
