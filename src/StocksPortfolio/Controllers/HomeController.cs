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
            var results = Mapper.Map<IEnumerable<PortfolioDTO>>(portfolioEntities);
            foreach (var stock in results)
            {
                var temp = await _repository.Lookup(stock.Symbol);
                stock.CurrentPrice = temp.Price;
                stock.Change = stock.CurrentPrice - stock.Price;
            }
            var user = _repository.GetUser(id);
            ViewData["Cash"] = user.Cash;
            return View(results);
        }

        [HttpGet]
        public IActionResult Buy()
        {
            return View();            
        }

        [HttpGet("/Home/Buy/{id}")]
        public IActionResult Buy(string id)
        {
            if(id == null)
            {
                return View();
            }
            else
            {
                ViewData["Symbol"] = id;
                return View();
            }
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

        [HttpGet]
        public IActionResult Sell()
        {
            return View();
        }

        [HttpGet("/Home/Sell/{id}")]
        public IActionResult Sell(string id)
        {
            if (id == null)
            {
                return View();
            }
            else
            {
                ViewData["Symbol"] = id;
                return View();
            }
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
            transaction.Symbol.ToUpper();

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

        [HttpPost]
        public async Task<IActionResult> Quote(TransactionModel transaction)
        {
            if (transaction == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Task<Transactions> lookup = _repository.Lookup(transaction.Symbol);
            Transactions newTransaction = await lookup;
            var result = Mapper.Map<TransactionDTO>(newTransaction);
            result.Symbol.ToUpper();
            return View("QuoteReturn", result);
        }


        [HttpGet]
        public IActionResult History()
        {
            var id = _userManager.GetUserId(User);
            var results = _repository.GetTransactions(id);
            var history = Mapper.Map<IEnumerable<TransactionDTO>>(results);
            return View(history);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
