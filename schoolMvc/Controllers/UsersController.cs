using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using schoolMvc.DAL.Models;
using schoolMvc.PL.ViewModel.User;

namespace schoolMvc.PL.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        #region Index GetAll_Users
        public async Task<IActionResult> Index(string SearchInput)
        {
            var users = Enumerable.Empty<UserViewModel>();

            if (string.IsNullOrEmpty(SearchInput))
            {
                users = await _userManager.Users.Select(U => new UserViewModel()
                {
                    Email = U.Email,
                    UserName = U.UserName,
                    Age = U.Age,
                    Id = U.Id,
                    City = U.City,
                    Phone = U.PhoneNumber,
                    Address = U.Address,
                    Roles = _userManager.GetRolesAsync(U).Result
                }).ToListAsync();
            }
            else
            {
                users = await _userManager.Users.Where(U => U.Email.ToLower()
                                               .Contains(SearchInput.ToLower()))
                                               .Select(U => new UserViewModel()
                                               {
                                                   Email = U.Email,
                                                   UserName = U.UserName,
                                                   Age = U.Age,
                                                   Id = U.Id,
                                                   City = U.City,
                                                   Phone = U.PhoneNumber,
                                                   Address = U.Address,
                                                   Roles = _userManager.GetRolesAsync(U).Result
                                               })
                                                 .ToListAsync();
            }

            return View(users);
        }
        #endregion

        #region Details_User
        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();
            }

            var userFormDB = await _userManager.FindByIdAsync(id);
            if (userFormDB is null)
            {
                return NotFound();
            }

            var user = new UserViewModel()
            {
                Email = userFormDB.Email,
                UserName = userFormDB.UserName,
                Address = userFormDB.Address,
                Age = userFormDB.Age,
                Phone = userFormDB.PhoneNumber,
                City = userFormDB.City,
                Id = userFormDB.Id,
                Roles = _userManager.GetRolesAsync(userFormDB).Result
            };

            return View(ViewName, user);

        }
        #endregion

        #region Edit_User

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel model)
        {
            if (id != model.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                var userFormDb = await _userManager.FindByIdAsync(id);
                if (userFormDb is null)
                    return NotFound();

                userFormDb.Email = model.Email;
                userFormDb.UserName = model.UserName;
                userFormDb.Address = model.Address;
                userFormDb.Age = model.Age;
                userFormDb.PhoneNumber = model.Phone;
                userFormDb.City = model.City;
                await _userManager.UpdateAsync(userFormDb);


                return RedirectToAction(nameof(Index));

            }
            return View(model);
        }

        #endregion

        #region Delete_User

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string? id, UserViewModel model)
        {
            if (id != model.Id)
                return BadRequest();


            if (ModelState.IsValid)
            {

                var userFormDb = await _userManager.FindByIdAsync(id);
                if (userFormDb is null)
                    return NotFound();
                await _userManager.DeleteAsync(userFormDb);
                return RedirectToAction(nameof(Index));

            }
            return View(model);
        }

        #endregion

    }
}
