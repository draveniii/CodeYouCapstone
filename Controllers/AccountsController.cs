using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBankingApp.Data;
using WebBankingApp.Models;

namespace MVCWebBanking.Controllers
{
    public class AccountsController : Controller
    {
        private readonly BankingContext _context;

        public AccountsController(BankingContext context)
        {
            _context = context;
        }

        // GET: Accounts/Members
        public async Task<IActionResult> Members(int? id )
        {

            if (id == null)
            {
                return NotFound();
            }

            Account account = _context.Accounts
                .Include(x => x.Members)
                .ThenInclude(y => y.Member)
                .FirstOrDefault(a => a.Id == id);

            return View(account);
        }

        // Creates a cookie so that the current active account persists through each controller
        private void CreateAccountCookie(int accountId)
        {
            CookieOptions options = new CookieOptions
            {
                Domain = "localhost", // Set the domain for the cookie
                Expires = DateTime.Now.AddHours(1), // Set expiration date to 1 hour from now
                Path = "/", // Cookie is available within the entire application
                Secure = true, // Ensure the cookie is only sent over HTTPS
                HttpOnly = true, // Prevent client-side scripts from accessing the cookie
                IsEssential = true // Indicates the cookie is essential for the application to function
            };

            Response.Cookies.Append("accountId", accountId.ToString(), options);
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        // To protect fom overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Id")] Account account)
        {
            try
            {
                account = _context.Accounts.Single(a => a.Id == account.Id);

                CreateAccountCookie(account.Id);

                return RedirectToAction(nameof(Details), new { id = account.Id });
            }
            catch (InvalidOperationException ioe) 
            {
                ModelState.AddModelError("Id", "Login Failed");
            }     

            return View();
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account =  _context.Accounts
                .Include(s => s.Shares)
                .FirstOrDefault(m => m.Id == id);

            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }
        
    }
}
