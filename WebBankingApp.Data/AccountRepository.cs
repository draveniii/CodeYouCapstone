using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebBankingApp.Models;

namespace WebBankingApp.Data
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankingContext _dbContext;
        
        public AccountRepository()
        {
            _dbContext = new BankingContext();
        }

        public void AddAccount(Account newAccount)
        {
            _dbContext.Accounts.Add(newAccount);
            _dbContext.SaveChanges();
        }

        public Account GetAccount(int accountId)
        {
            return _dbContext.Accounts.SingleOrDefault(x => x.Id == accountId);
        }

    }


}
