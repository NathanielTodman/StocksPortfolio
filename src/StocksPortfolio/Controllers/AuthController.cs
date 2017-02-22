using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StocksPortfolio.Entities;
using StocksPortfolio.ViewModels;
using System.Threading.Tasks;

namespace StocksPortfolio.Controllers
{
    public class AuthController : Controller
    {
        
        private SignInManager<FoxUser> _signInManager;
        private UserManager<FoxUser> _userManager;

        public AuthController(UserManager<FoxUser> userManager, 
                              SignInManager<FoxUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        [HttpGet]        
        public IActionResult Register()
        {
            return View();
        }
        
        //Register user
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new FoxUser { UserName = model.Username, Email = model.Email };
                var createResult =  await _userManager.CreateAsync(user, model.Password);
                if (createResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach(var error in createResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");
            }

            return View();
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //Attempt login
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel vm)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(
                                            vm.Username, vm.Password, 
                                            vm.RememberMe, false);

                if(signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(vm.ReturnUrl))
                    {
                        return RedirectToAction("Index","Home");
                    }
                    else if(!Url.IsLocalUrl(vm.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(vm.ReturnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username or password incorrect");
                }
            }

            return View();
        }

        //Logout user
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
