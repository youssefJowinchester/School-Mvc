using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using schoolMvc.DAL.Models;
using schoolMvc.PL.ViewModel.Role;

namespace schoolMvc.PL.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {

            _roleManager = roleManager;
            _userManager = userManager;
        }




        #region Index GetAll_Roles
        public async Task<IActionResult> Index(string SearchInput)
        {
            var roles = Enumerable.Empty<RoleViewModel>();

            if (string.IsNullOrEmpty(SearchInput))
            {
                roles = await _roleManager.Roles.Select(R => new RoleViewModel()
                {
                    Id = R.Id,
                    roleName = R.Name


                }).ToListAsync();
            }
            else
            {
                roles = await _roleManager.Roles.Where(R => R.Name.ToLower()
                                               .Contains(SearchInput.ToLower()))
                                               .Select(R => new RoleViewModel()
                                               {
                                                   Id = R.Id,
                                                   roleName = R.Name,

                                               }).ToListAsync();

            }

            return View(roles);
        }
        #endregion

        #region Create_Role
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {

            if (!ModelState.IsValid)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = model.roleName });
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
        #endregion

        #region Details_Role

        [HttpGet]
        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();
            }

            var roleFormDb = await _roleManager.FindByIdAsync(id);
            if (roleFormDb is null)
            {
                return NotFound();
            }

            var role = new RoleViewModel()
            {
                Id = roleFormDb.Id,
                roleName = roleFormDb.Name
            };

            return View(ViewName, role);

        }

        #endregion

        #region Edit_Role

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel model)
        {
            if (id != model.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                var roleFormDb = await _roleManager.FindByIdAsync(id);
                if (roleFormDb is null)
                    return NotFound();

                roleFormDb.Name = model.roleName;

                await _roleManager.UpdateAsync(roleFormDb);


                return RedirectToAction(nameof(Index));

            }
            return View(model);
        }


        #endregion

        #region Delete_Role

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, RoleViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {

                var roleFormDb = await _roleManager.FindByIdAsync(id);
                if (roleFormDb is null)
                    return NotFound();
                await _roleManager.DeleteAsync(roleFormDb);
                return RedirectToAction(nameof(Index));

            }
            return View(model);
        }

        #endregion

        #region AddOrRemoveUser

        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUser(string roleId)
        {

            var role = await _roleManager.FindByIdAsync(roleId);

            if (role is null) return NotFound();

            ViewData["RoleId"] = roleId;

            var usersInrole = new List<UserInRoleViewModel>();
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var userInrole = new UserInRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userInrole.IsSelected = true;
                }
                else
                {
                    userInrole.IsSelected = false;
                }
                usersInrole.Add(userInrole);
            }

            return View(usersInrole);
        }



        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUser(string roleId, List<UserInRoleViewModel> users)
        {

            var role = await _roleManager.FindByIdAsync(roleId);

            if (role is null) return NotFound();

            if (ModelState.IsValid)
            {


                foreach (var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                    if (appUser is not null)
                    {
                        if (user.IsSelected && !await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.AddToRoleAsync(appUser, role.Name);
                        }
                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                        }

                    }
                }

                return RedirectToAction(nameof(Index), new { id = roleId });
            }
            return View(users);
        }

        #endregion
    }
}
