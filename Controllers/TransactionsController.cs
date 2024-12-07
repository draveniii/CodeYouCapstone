﻿using System;
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

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transactions.ToListAsync());
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,DateTime")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Deposit(int? shareId)
        {
            ViewData["shareId"] = shareId;
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deposit(int shareId, [Bind("Amount")] Transaction transaction)
        {
            ViewData["shareId"] = shareId;

            // Loads share from database and performs deposit
            Share share = _context.Shares.Include(s => s.Account).Where(w => w.Id == shareId).Single();
            share.CurrentBalance += transaction.Amount;

            // Sets transaction values
            transaction.DateTime = DateTime.Now;
            transaction.Share = share;

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

        // GET: Transactions/Create
        public IActionResult Transfer(int? shareId)
        {
            ViewData["fromShareId"] = shareId;

            Share share = _context.Shares
                .Include(a => a.Account)
                .ThenInclude(s => s.Shares)
                .Where(i => i.Id == shareId)
                .Single();

            List<Share> shares = share.Account.Shares;

            // If shares is null return to shares details page
            if (shares == null)
            {
                return RedirectToAction("Details", "Shares", shareId);
            }

            // Removes the from share from the list of possible to shares
            shares.Remove(share);

            // If there is no other shares you cannot perform a transfer to other shares
            if (shares.Count == 0)
            {
                return RedirectToAction("Details", "Shares", shareId);
            }
                        
            ViewData["shares"] = shares;

            return View(new Transaction());
        }

        // POST: Transactions/Create
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
            ViewData["shares"] = shares;

            transaction.DateTime = DateTime.Now;
            
            // Loads fromShare and toShare from database 
            Share fromShare = _context.Shares
                .Include(s => s.Account)
                .Where(w => w.Id == fromShareId)
                .Single();
            Share toShare = _context.Shares.Include(s => s.Account).Where(w => w.Id == toShareId).Single();

            // Verifies from share has the balance available to transfer
            if (fromShare.CurrentBalance - transaction.Amount >= fromShare.MinimumBalance)
            {
                fromShare.CurrentBalance -= transaction.Amount;
            }
            else
            {
                ModelState.AddModelError("Amount", "Insufficient Balance");
                return View();
            }
            
            toShare.CurrentBalance += transaction.Amount;

            // Sets transaction values
            Transaction fromTransaction = new Transaction(transaction);
            Transaction toTransaction = new Transaction(transaction);

            // Makes the amount report as a credit vs a debit
            fromTransaction.Amount = -1 * fromTransaction.Amount;

            fromTransaction.Share = fromShare;
            toTransaction.Share = toShare;

            // Validate transactions before save
            ValidationContext fromContext = new ValidationContext(fromTransaction);
            ValidationContext toContext = new ValidationContext(toTransaction);
            List<ValidationResult> results = new List<ValidationResult>();
            bool fromTransactionValid = Validator.TryValidateObject(fromTransaction, fromContext, results, true);                        
            bool toTransactionValid = Validator.TryValidateObject(toTransaction, toContext, results, true);

            // If both transactions are valid save
            if (fromTransactionValid && toTransactionValid) 
            {
                _context.Add(fromTransaction);
                _context.Add(toTransaction);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Shares", new { id = fromShareId });
            }
            else
            {
                ModelState.AddModelError("Amount", "Transaction Not Valid");
            }

            return View();
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,DateTime")] Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }
}
