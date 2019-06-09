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
using VashiteKinti.Web.Models;

namespace VashiteKinti.Web.Controllers
{
    public class DepositsController : Controller
    {
        private readonly IGenericDataService<Deposit> _deposits;
        private readonly IGenericDataService<Bank> _banks;

        public DepositsController(IGenericDataService<Deposit> deposits,
            IGenericDataService<Bank> banks)
        {
            _deposits = deposits;
            _banks = banks;
        }

        // GET: Deposits
        public async Task<IActionResult> Index()
        {
            DepositEditViewModel viewModel = new DepositEditViewModel();
            viewModel.Deposits = await _deposits.GetAllAsync();
            viewModel.Deposits = viewModel.Deposits.OrderByDescending(x => x.Interest).ToList();
            //viewModel.Deposits.First().Interest = 5;
            return View(viewModel);
        }

        public async Task<IActionResult> FilterCatalogueDeposits(DepositEditViewModel vm)
        {
            DepositEditViewModel viewModel = new DepositEditViewModel();
            viewModel.Deposits = await _deposits.GetFilteredDeposits(vm.CurrencyId, vm.InterestId);
            viewModel.Deposits = viewModel.Deposits.OrderByDescending(x => x.Interest).ToList();

            return View("Index", viewModel);
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
        public async Task<IActionResult> Create()
        {
            var banks = await _banks.GetAllAsync();
            ViewBag.ListOfBanks = banks;

            return View();
        }

        // POST: Deposits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Deposit deposit)
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

            var banks = await _banks.GetAllAsync();
            ViewBag.ListOfBanks = banks;

            return View(deposit);
        }

        // POST: Deposits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Deposit deposit)
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



        //Deposits
        public async Task<IActionResult> SearchDeposits()
        {
            DepositEditViewModel viewModel = new DepositEditViewModel();
            viewModel.Deposits = await _deposits.GetAllAsync();
            viewModel.Deposits = viewModel.Deposits.OrderByDescending(x => x.Interest).ToList();
            //viewModel.Deposits.First().Interest = 5;
            return View("Deposits",viewModel);
        }

        public async Task<IActionResult> FilterDeposits(DepositEditViewModel vm)
        {
            DepositEditViewModel viewModel = new DepositEditViewModel();
            viewModel.Deposits = await _deposits.SearchDepositsByCriterias(vm.DepositSize,vm.CurrencyId, 
                                                                            vm.DepositPeriodId,vm.InterestId,
                                                                            vm.DepositHolderId,vm.InterestTypeId,
                                                                            vm.ExtraMoneyPayInId,vm.OverdraftOpportunityId,
                                                                            vm.CreditOpportunityId);

            viewModel.Deposits = viewModel.Deposits.OrderByDescending(x => x.Interest).ToList();
            //viewModel.Deposits.First().Interest = 5;
            return View("Deposits", viewModel);
        }
    }
}
