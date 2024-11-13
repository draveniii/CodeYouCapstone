using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBankingApplication.Models
{
    internal class Account
    {
        public Int32 AccountNumber { get; set; }
        public List<Member> Members { get; set; }
        public List<Share> Shares { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
