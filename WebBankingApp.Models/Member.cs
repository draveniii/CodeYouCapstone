﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBankingApp.Models
{
    public class Member
    {
        public string Name { get; set; }
        public List<Account> Accounts { get; set; } = new List<Account>();
        public DateOnly DateOfBirth { get; set; }
        public int SSNumber { get; set; }
    }
}
