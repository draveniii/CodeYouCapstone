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
    public class SharesController : Controller
    {
        private readonly BankingContext _context;

        public SharesController(BankingContext context)
        {
            _context = context;
        }

        // GET: Shares/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Share share = await _context.Shares
                .Include(a => a.Account)
                .Include(t => t.Transactions)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (share == null)
            {
                return NotFound();
            }

            return View(share);
        }
    }
}
