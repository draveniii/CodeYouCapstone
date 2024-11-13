using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBankingApplication.Models
{
    internal class Transaction
    {
        public String Type {  get; set; }
        public Decimal Amount {  get; set; }
        public DateTime Date {  get; set; }
    }
}
