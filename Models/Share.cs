using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBankingApp.Models
{
    public class Share
    {
        public Int32 Id { get; set; }
        public String Type { get; set; }
        public Decimal MinimumBalance { get; set; }
        public Decimal InterestRate { get; set; }
        public Account Account { get; set; }    
        public Int32 CurrentBalance { get; set; }
        public virtual List<Transaction>? Transactions { get; set; }
    }
}
