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

namespace StocksPortfolio.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
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
