using MVCWebBanking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBankingApp.Models
{
    public class Member
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        //public List<Account> Accounts { get; set; } = new List<Account>();
        public List<AccountMember> Accounts { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Int32 SSNumber { get; set; }
    }
}
