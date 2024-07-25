using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using schoolMvc.Controllers;
using schoolMvc.DAL.Models;
using schoolMvc.PL.ViewModel.Account;

namespace schoolMvc.PL.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }



        #region Sign Up 

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(model.UserName);
                    user = await _userManager.FindByNameAsync(model.Email);
                    if (user is null)
                    {

                        if (model.Age < 25)
                        {

                            user = new Student()
                            {
                                UserName = model.UserName,
                                Email = model.Email,
                                Age = model.Age,
                                PhoneNumber = model.Phone,
                                City = model.City,
                                Address = model.Address,
                            };
                        }
                        else
                        {
                            user = new Teacher()
                            {
                                UserName = model.UserName,
                                Email = model.Email,
                                Age = model.Age,
                                PhoneNumber = model.Phone,
                                City = model.City,
                                Address = model.Address,
                            };
                        }

                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                            return RedirectToAction(nameof(SignIn));


                    }
                    ModelState.AddModelError(string.Empty, "User Is AReady Exits (:");
                }
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }

        }




        #endregion


        #region Sign In
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login!!!");
            }

            return View(model);
        }

        #endregion

        #region Sign Out
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }

        #endregion


        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
