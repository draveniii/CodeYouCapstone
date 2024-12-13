using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBankingApp.Data;
using WebBankingApp.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace MVCWebBanking.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly BankingContext _context;

        public TransactionsController(BankingContext context)
        {
            _context = context;
        }

        // GET: Transactions/Deposit
        public IActionResult Deposit(int? shareId)
        {
            ViewData["shareId"] = shareId;
            return View();
        }

        // Helper function that checks to make sure input amounts are positive
        public bool VerifyAmountNonNegative(Transaction transaction)
        {
            if (transaction.Amount < 0)
            {
                ModelState.AddModelError("Amount", "Amount must be a positive value");
                return false;
            }

            return true;
        }

        // POST: Transactions/Deposit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deposit(int shareId, [Bind("Amount")] Transaction transaction)
        {
            ViewData["shareId"] = shareId;

            VerifyAmountNonNegative(transaction);

            // Loads share from database and performs deposit
            Share share = _context.Shares.Include(s => s.Account).Where(w => w.Id == shareId).Single();
            share.CurrentBalance += transaction.Amount;

            // Sets transaction values
            transaction.DateTime = DateTime.Now;
            transaction.Share = share;
            transaction.NewBalance = share.CurrentBalance;

            ModelState.ClearValidationState(nameof(Share));

            // Verify's share model is valid and saves changes to database if so,
            // if not, reload page
            if (TryValidateModel(transaction, nameof(Share)))
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Shares", new { id = shareId });
            }

            return View();
        }

        // GET: Transactions/Transfer
        public IActionResult Transfer(int? shareId)
        {
            ViewData["fromShareId"] = shareId;

            Share share;

            try
            {
                share = _context.Shares
                    .Include(a => a.Account)
                    .ThenInclude(s => s.Shares)
                    .Where(i => i.Id == shareId)
                    .Single();
            }
            catch (Exception InvalidOperation)
            {
                // If shares is null return to shares details page
                return RedirectToAction("Details", "Shares", new { id = shareId });
            }

            List<Share> shares = share.Account.Shares;

            // Removes the from share from the list of possible to shares
            shares.Remove(share);

            // If there is no other shares you cannot perform a transfer to other shares
            if (shares.Count == 0)
            {
                return RedirectToAction("Details", "Shares", new { id = shareId });
            }
                        
            ViewData["shares"] = shares;
            ViewData["fromShare"] = share;

            return View(new Transaction());
        }

        // POST: Transactions/Transfer
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Transfer(int fromShareId, int toShareId, [Bind("Amount")] Transaction transaction)
        {         
            // Set up view state in case of errors
            ViewData["fromShareId"] = fromShareId;
            Share share = _context.Shares
               .Include(a => a.Account)
               .ThenInclude(s => s.Shares)
               .Where(i => i.Id == fromShareId)
               .Single();
            List<Share> shares = share.Account.Shares;


            bool validAmount = VerifyAmountNonNegative(transaction);

            // Checks to see if a toShare was selected
            if (toShareId == 0)
            {
                shares.Remove(share);
                ViewData["shares"] = shares;
                ViewData["fromShare"] = share;
                ModelState.AddModelError("Amount", "Please select a share to transfer to.");
                return View();
            }

            transaction.DateTime = DateTime.Now;
            
            // Loads fromShare and toShare from database 
            Share fromShare = _context.Shares
                .Include(s => s.Account)
                .Where(w => w.Id == fromShareId)
                .Single();
            Share toShare = _context.Shares
                .Include(s => s.Account)
                .Where(w => w.Id == toShareId)
                .Single();

            // Verifies from share has the balance available to transfer
            if (fromShare.CurrentBalance - transaction.Amount >= fromShare.MinimumBalance)
            {
                fromShare.CurrentBalance -= transaction.Amount;
                toShare.CurrentBalance += transaction.Amount;
            }
            else
            {
                shares.Remove(share);
                ViewData["shares"] = shares;
                ViewData["fromShare"] = share;
                ModelState.AddModelError("Amount", "Insufficient Balance");
                return View();
            }          

            // Sets transaction values
            Transaction fromTransaction = new Transaction(transaction);
            Transaction toTransaction = new Transaction(transaction);

            ModelState.Remove("Share");
            ModelState.Remove("Amount");

            // Makes the amount on the fromShare report as a credit vs a debit
            fromTransaction.Amount = -1 * fromTransaction.Amount;        

            // Updates NewBalance on transactions
            fromTransaction.NewBalance = fromShare.CurrentBalance;
            toTransaction.NewBalance = toShare.CurrentBalance;

            fromShare.Transactions.Add(fromTransaction);
            toShare.Transactions.Add(toTransaction);


            // Validate transactions before save
            ValidationContext fromContext = new ValidationContext(fromTransaction);
            ValidationContext toContext = new ValidationContext(toTransaction);
            List<ValidationResult> results = new List<ValidationResult>();
            bool fromTransactionValid = Validator.TryValidateObject(fromTransaction, fromContext, results, true);
            bool toTransactionValid = Validator.TryValidateObject(toTransaction, toContext, results, true);

            if (validAmount)
            {
                if (fromTransactionValid && toTransactionValid)
                {
                    _context.SaveChanges();
                    return RedirectToAction("Details", "Shares", new { id = fromShareId });
                }
                else
                {
                    ModelState.AddModelError("Amount", "Transaction Not Valid");
                }
            }

            shares.Remove(share);
            ViewData["shares"] = shares;
            ViewData["fromShare"] = share;
            return View();
        }

        // GET: Transactions/ExternalTransfer
        public IActionResult ExternalTransfer(int? shareId)
        {
            ViewData["fromShareId"] = shareId;

            Share share;

            try
            {
                share = _context.Shares
                    .Include(a => a.Account)
                    .ThenInclude(s => s.Shares)
                    .Where(i => i.Id == shareId)
                    .Single();
            }
            catch (Exception InvalidOperation)
            {
                // If shares is null return to shares details page
                return RedirectToAction("Details", "Shares", new { id = shareId });
            }

            ViewData["fromShare"] = share;

            return View();
        }

        // POST: Transactions/Deposit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalTransfer(int shareId, [Bind("Amount")] Transaction transaction)
        {
            ViewData["fromShareId"] = shareId;

            VerifyAmountNonNegative(transaction);

            // Loads share from database and performs deposit
            Share share = _context.Shares.Include(s => s.Account).Where(w => w.Id == shareId).Single();

            if (share.CurrentBalance - share.MinimumBalance >= transaction.Amount)
            {
                share.CurrentBalance -= transaction.Amount;
            }
            else
            {
                ModelState.AddModelError("Amount", "Insufficient Balance");
                return View();
            }

            // Sets transaction values
            transaction.DateTime = DateTime.Now;
            transaction.Share = share;
            transaction.NewBalance = share.CurrentBalance;
            transaction.Amount = -1 * transaction.Amount;

            ModelState.ClearValidationState(nameof(Share));

            // Verify's share model is valid and saves changes to database if so,
            // if not, reload page
            if (TryValidateModel(transaction, nameof(Share)))
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Shares", new { id = shareId });
            }

            return View();
        }
    }
}
