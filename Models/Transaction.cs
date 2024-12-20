﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBankingApp.Models
{
    public class Transaction
    {
        public Int32 Id { get; set; }
        public Decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
        public Decimal NewBalance { get; set; }
        public Int32 ShareId { get; set; }
        public virtual Share Share { get; set; }


        // Constructor
        public Transaction()
        {
            Id = 0;
            Amount = 0;
            DateTime = DateTime.MinValue;
            Share = null;
        }

        // Copy constructor
        public Transaction(Transaction transaction)
        {
            Id = transaction.Id;
            Amount = transaction.Amount;
            DateTime = transaction.DateTime;
            ShareId = transaction.ShareId;
            Share = transaction.Share;
        }
    }
}
