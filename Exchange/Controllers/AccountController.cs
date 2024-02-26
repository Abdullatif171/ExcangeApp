using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Exchange.Entity;
using Exchange.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Exchange.Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Exchange.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<Users> _userManager;
        private RoleManager<UsersRole> _roleManager;
        private SignInManager<Users> _signInManager;
        private IdentityContext _context;

        public AccountController(UserManager<Users> userManager, RoleManager<UsersRole> roleManager, SignInManager<Users> signInManager, IdentityContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
        }

        [Authorize]
        public IActionResult Profile()
        {
            var mainCategories = _context.MainCategries.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
            ViewBag.MainCategories = mainCategories;

            var user = _context.Users.FirstOrDefault();

            return View(user);
        }

        public IActionResult Authentication()
        {
            var mainCategories = _context.MainCategries.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
            ViewBag.MainCategories = mainCategories;

            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Profile");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();

                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Profile");

                    }


                }
                else
                {
                    ModelState.AddModelError("", "E-posta Veya Şifre yanlış");
                    return RedirectToAction("Authentication", model);
                }

            }
            return RedirectToAction("Authentication", model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Users { UserName = model.UserName, Email = model.Email, Name = model.Name, Surname = model.Surname, };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Authentication");
                }

                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Authentication");
        }

    }

}

