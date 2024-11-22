using WebBankingApp.Data;
using WebBankingApp.Models;
using System.Text.Json;

namespace WebBankingApp.Controllers
{
    public class AccountController
    {
        private readonly IAccountRepository _accountRepo;

        public AccountController(IAccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public Boolean CreateNewAccount(String jsonInput)
        {
            Account newAccount = JsonSerializer.Deserialize<Account>(jsonInput);
            _accountRepo.AddAccount(newAccount);
        }
    }
}


MauiBlazor
crud to database
Quick easy simple first