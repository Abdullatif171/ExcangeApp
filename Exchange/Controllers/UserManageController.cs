using Exchange.Data.Abstract;
using Exchange.Entity;
using Exchange.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exchange.Controllers
{
    public class UserManageController : Controller
    {
        private UserManager<Users> _userManager;
        private RoleManager<UsersRole> _roleManager;
        private IMainCategoriesRepository _MainCategriesRepository;
        public UserManageController(UserManager<Users>  userManager, RoleManager<UsersRole> roleManager, IMainCategoriesRepository MainCategriesRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager; 
            _MainCategriesRepository = MainCategriesRepository;
        }
        
        public IActionResult Index()
        {
            ViewBag.MainCategories = _MainCategriesRepository.MainCategories.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
            return View(_userManager.Users);
        }
        public IActionResult AddUser()
        {
            ViewBag.MainCategories = _MainCategriesRepository.MainCategories.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(RegisterViewModel model)
        {
            ViewBag.MainCategories = _MainCategriesRepository.MainCategories.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();

            if (ModelState.IsValid)
            {
                var user = new Users { 
                    UserName = model.UserName,
                    Email = model.Email,
                    Name = model.Name,
                    PhoneNumber = model.Phone,
                    Surname = model.Surname,
                    Image = "user.jpeg"
                    };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> UserEdit(string id)
        {
            ViewBag.MainCategories = _MainCategriesRepository.MainCategories.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
            if(id == null)
            {
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByIdAsync(id);

            if(user != null)
            {
                ViewBag.Roles = await _roleManager.Roles.Select(i => i.Name).ToListAsync();

                return View(new EditViewModel {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    UserName = user.UserName,
                    Phone = user.PhoneNumber,
                    Email = user.Email,
                    SelectRole = await _userManager.GetRolesAsync(user)
                });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(string id, EditViewModel model)
        {
            ViewBag.MainCategories = _MainCategriesRepository.MainCategories.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
            if(id != model.Id)
            {
                return RedirectToAction("Index");
            }

            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                if(user != null)
                {
                    user.Name = model.Name;
                    user.Surname = model.Surname;
                    user.UserName = model.UserName;
                    user.PhoneNumber = model.Phone;
                    user.Email = model.Email;

                    var result = await _userManager.UpdateAsync(user);

                    if(result.Succeeded && !string.IsNullOrEmpty(model.Password))
                    {
                        await _userManager.RemovePasswordAsync(user);
                        await _userManager.AddPasswordAsync(user, model.Password);
                    }

                    if(result.Succeeded)
                    {
                        await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
                        if(model.SelectRole != null)
                        {
                            await _userManager.AddToRolesAsync(user, model.SelectRole);
                        }
                        return RedirectToAction("Index");
                    }

                    foreach(IdentityError err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if(user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("Index");
        }

    }
}