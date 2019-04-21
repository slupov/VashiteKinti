using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VashiteKinti.Data;
using VashiteKinti.Data.Models;
using VashiteKinti.Services;

namespace VashiteKinti.Web.Controllers
{
    public class DepositsController : Controller
    {
        private readonly IGenericDataService<Deposit> _deposits;

        public DepositsController(IGenericDataService<Deposit> deposits)
        {
            _deposits = deposits;
        }

        // GET: Deposits
        public async Task<IActionResult> Index()
        {
            var deposits = await _deposits.GetAllAsync();

            return View(deposits);
        }

        // GET: Deposits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deposit = await _deposits.GetSingleOrDefaultAsync(m => m.Id == id);

            if (deposit == null)
            {
                return NotFound();
            }

            return View(deposit);
        }

        // GET: Deposits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Deposits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BankId,Name,MinAmount,Interest,PaymentMethod,Currency")] Deposit deposit)
        {
            if (ModelState.IsValid)
            {
                _deposits.Add(deposit);

                return RedirectToAction(nameof(Index));
            }

            return View(deposit);
        }

        // GET: Deposits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deposit = await _deposits.GetSingleOrDefaultAsync(x => x.Id == id);
            if (deposit == null)
            {
                return NotFound();
            }

            return View(deposit);
        }

        // POST: Deposits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BankId,Name,MinAmount,Interest,PaymentMethod,Currency")] Deposit deposit)
        {
            if (id != deposit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _deposits.Update(deposit);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await DepositExists(deposit.Id))
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
            return View(deposit);
        }

        // GET: Deposits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deposit = await _deposits.GetSingleOrDefaultAsync(m => m.Id == id);
            if (deposit == null)
            {
                return NotFound();
            }

            return View(deposit);
        }

        // POST: Deposits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deposit = await _deposits.GetSingleOrDefaultAsync(x => x.Id == id);
            _deposits.Remove(deposit);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> DepositExists(int id)
        {
            return await _deposits.AnyAsync(e => e.Id == id);
        }
    }
}
