using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StocksPortfolio.ViewModels;
using StocksPortfolio.Models;
using StocksPortfolio.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using StocksPortfolio.Entities;
using System.Threading.Tasks;
using System.Linq;

namespace StocksPortfolio.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IFoxStocksRepository _repository;
        private UserManager<FoxUser> _userManager;
        
        public HomeController(IFoxStocksRepository foxRepository,
                              UserManager<FoxUser> userManager)
        {
            _repository = foxRepository;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Portfolio()
        {
            var id = _userManager.GetUserId(User);
            var portfolioEntities = _repository.GetPortfolio(id);
            var results = Mapper.Map<IEnumerable<TransactionDTO>>(portfolioEntities);
            foreach (var stock in results)
            {
                var temp = await _repository.Lookup(stock.Symbol);
                stock.CurrentPrice = temp.Price;
                stock.Change = stock.CurrentPrice - stock.Price;
            }
            return View(results);
        }

        [HttpGet]
        public IActionResult Buy()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Buy(TransactionModel transaction)
        {
            if (transaction == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _userManager.GetUserId(User);
            Task<Transactions> lookup = _repository.Lookup(transaction.Symbol);
            Transactions newTransaction = await lookup;
            newTransaction.FoxUserId = id;
            newTransaction.Quantity = transaction.Quantity;
            newTransaction.Buy = true;
            _repository.AddTransaction(id, newTransaction);
            if (!_repository.Save())
            {
                return StatusCode(500, "Something went wrong.");
            }

            return RedirectToAction("Portfolio", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Sell(TransactionModel transaction)
        {
            if (transaction == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _userManager.GetUserId(User);
            Task<Transactions> lookup = _repository.Lookup(transaction.Symbol);
            Transactions newTransaction = await lookup;
            newTransaction.FoxUserId = id;
            newTransaction.Quantity = transaction.Quantity;
            newTransaction.Buy = false;
            _repository.AddTransaction(id, newTransaction);
            if (!_repository.Save())
            {
                return StatusCode(500, "Something went wrong.");
            }

            return RedirectToAction("Portfolio", "Home");
        }

        public IActionResult Quote()
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
