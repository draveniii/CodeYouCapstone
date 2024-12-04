using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBankingApp.Models;

namespace WebBankingApp.Data
{
    public interface IAccountRepository
    {
        public void AddAccount(Account newAccount);

        public Account GetAccount(int accountId);
    }
}
