using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StocksPortfolio.Entities;
using StocksPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksPortfolio.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<FoxUser> _signInManager;

        public AuthController(SignInManager<FoxUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Home", "Portfolio");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, true, false);

                if(signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username or password incorrect");
                }
            }

            return View();
        }
    }
}
