using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBankingApp.Models;

namespace WebBankingApp.Data
{
    public class MemberRepository
    {
        private readonly BankingContext _dbContext;

        public MemberRepository()
        {
            _dbContext = new BankingContext();
        }

        public void AddMember(Member newMember)
        {
            _dbContext.Members.Add(newMember);
            _dbContext.SaveChanges();
        }

        //public List<Account> GetAccounts(Member member)
        //{
        //}

    }
}
