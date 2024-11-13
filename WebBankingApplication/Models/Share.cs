using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBankingApplication.Models
{
    public class Share
    {
        public Int32 ShareID { get; internal set; }
        public String ShareName { get; set; }
        public Decimal Balance { get; set; }
        public Decimal MinimumBalance { get; set; }
        public Decimal InterestRate { get; set; }
    }
}
