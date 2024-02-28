using Exchange.Data.Abstract;
using Exchange.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exchange.Controllers
{
    
    public class RoleManageController : Controller
    {
        private UserManager<Users> _userManager;
        private RoleManager<UsersRole> _roleManager;
        private IMainCategoriesRepository _mainCategriesRepository;
        public RoleManageController(UserManager<Users>  userManager, RoleManager<UsersRole> roleManager, IMainCategoriesRepository mainCategriesRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager; 
            _mainCategriesRepository = mainCategriesRepository;
        }

        public IActionResult Index(){
            ViewBag.MainCategories = _mainCategriesRepository.MainCategories.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
            return View(_roleManager.Roles);
        }

        public IActionResult AddRole(){
            ViewBag.MainCategories = _mainCategriesRepository.MainCategories.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRoleAsync(UsersRole model){
            ViewBag.MainCategories = _mainCategriesRepository.MainCategories.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
            if(ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(model);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }

            }
            return View(model);
        }

        public async Task<IActionResult> RoleEdit(string id){
            ViewBag.MainCategories = _mainCategriesRepository.MainCategories.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
            var role = await _roleManager.FindByIdAsync(id);

            if(role != null && role.Name != null){

                ViewBag.Users = await _userManager.GetUsersInRoleAsync(role.Name);
                return View(role);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RoleEdit(UsersRole model){
            ViewBag.MainCategories = _mainCategriesRepository.MainCategories.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
            if(ModelState.IsValid){
                var role = await _roleManager.FindByIdAsync(model.Id);
                if(role != null){
                    role.Name = model.Name;
                    var result = await _roleManager.UpdateAsync(role);
                    if(result.Succeeded){
                        return RedirectToAction("Index");
                    }
                    foreach (var err in result.Errors)
                    {
                    ModelState.AddModelError("", err.Description);
                    }

                    if(role.Name != null)
                        ViewBag.Users = await _userManager.GetUsersInRoleAsync(role.Name);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if(role != null)
            {
                await _roleManager.DeleteAsync(role);
            }

            return RedirectToAction("Index");
        }
    }
}