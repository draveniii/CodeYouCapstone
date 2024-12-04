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

    }
}
