using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StocksPortfolio.ViewModels;
using StocksPortfolio.Models;
using Microsoft.Extensions.Configuration;
using StocksPortfolio.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace StocksPortfolio.Controllers
{
    public class HomeController : Controller
    {
        private IFoxStocksRepository _repository;

        public HomeController(IFoxStocksRepository foxRepository)
        {
            _repository = foxRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Portfolio()
        {

            var data = _repository.GetPortfolio("1");
            if (data == null)
            {
                return View();
            }
            var results = Mapper.Map<IEnumerable<TransactionDTO>>(data);

            return View(results);
        }

        public IActionResult Buy()
        {
            return View();
        }

        public IActionResult Sell()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Register user
                ModelState.Clear();
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");
            }

            return View();
        }


        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            //Logout user
            return View(Index());
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
