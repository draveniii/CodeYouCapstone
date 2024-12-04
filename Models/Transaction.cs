using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBankingApp.Models
{
    public class Transaction
    {
        public Int32 Id { get; set; }
        public Int32 Amount { get; set; }
        public DateTime DateTime { get; set; }

    }
}
