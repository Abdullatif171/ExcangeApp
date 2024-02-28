using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Exchange.Entity;
using Exchange.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Exchange.Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Exchange.Data.Abstract;

namespace Exchange.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<Users> _userManager;
        private RoleManager<UsersRole> _roleManager;
        private SignInManager<Users> _signInManager;
        private IdentityContext _context;
        private IEmailSender _emailSender;

        public AccountController(UserManager<Users> userManager, RoleManager<UsersRole> roleManager, SignInManager<Users> signInManager, IdentityContext context, IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
            _emailSender = emailSender;
        }

        [Authorize]
        public IActionResult Profile()
        {
            ViewBag.MainCategories = _context.MainCategries.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();

            var user = _userManager
                   .Users
                   .Include(x=>x.Product)
                   .Include(x=>x.Comment)
                   .ThenInclude(x=>x.Product)
                   .FirstOrDefault();

                   if(user == null){
                    return NotFound();
                   }
                   

            return View(user);
        }

        public IActionResult Login()
        {
            ViewBag.MainCategories = _context.MainCategries.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Profile");
            }

            return View();
        }

        [HttpPost]
         public async Task<IActionResult> Login(LoginViewModel model){
            ViewBag.MainCategories = _context.MainCategries.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
            if(ModelState.IsValid){
                var user = await _userManager.FindByEmailAsync(model.Email);

                if(user != null){
                    await _signInManager.SignOutAsync();

                    if(!await _userManager.IsEmailConfirmedAsync(user)){
                        ModelState.AddModelError("", "Hesabınızı onaylayınız!");
                        return View(model);
                    }
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password,model.RememberMe,true);

                    if(result.Succeeded){
                        await _userManager.ResetAccessFailedCountAsync(user);
                        await _userManager.SetLockoutEndDateAsync(user,null);

                       return RedirectToAction("Index","Home");
                    }
                    else if(result.IsLockedOut){
                        var lockoutDate = await _userManager.GetLockoutEndDateAsync(user);
                        var timeLeft = lockoutDate.Value - DateTime.Now;
                        ModelState.AddModelError("",$"Hesaabınız kilitlendi. Lütfen {timeLeft.Minutes} dakika sonra deneyiniz.");
                    }
                    else{
                    ModelState.AddModelError("","Parolanız hatalı!");
                }
                }
                else{
                    ModelState.AddModelError("","Bu Email adresiyle ilişkili bir hesap bulunamadı!");
                }
            }

            return View(model);
        }

        public IActionResult Register()
        {
            ViewBag.MainCategories = _context.MainCategries.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Profile");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            ViewBag.MainCategories = _context.MainCategries.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
            if (ModelState.IsValid)
            {
                var user = new Users { 
                    UserName = model.UserName,
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname,
                    Image = "user.jpeg"
                    };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var url = Url.Action("ConfirmEmail","Account", new{user.Id, token});

                     await _emailSender.SendEmailAsync(user.Email,"Hesap Onayı",$"Lütfen email hesbınızı onaylamak için linke <a href='http://localhost:5039{url}'>tıklayınız.</a>");


                    TempData["message"] = "Email adresinize gelen onay linkine tıklayınız.";

                    return RedirectToAction("Login");
                }

                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string Id, string token){
            ViewBag.MainCategories = _context.MainCategries.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
            if(Id == null || token == null){
                TempData["message"] = "Geçersiz token bilgisi";
                return RedirectToAction("Register");
            }
            var user = await _userManager.FindByIdAsync(Id);

            if(user != null){
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if(result.Succeeded){
                    TempData["message"] = "Hesabınız Onaylandı!";
                    return RedirectToAction("Login");
                }
            }
            TempData["message"] = "Kullanıcı bulunamadı!";
            return RedirectToAction("Register");
        }

        public async Task<IActionResult> Logout()
        {
            ViewBag.MainCategories = _context.MainCategries.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult ForgetPassword(){
            ViewBag.MainCategories = _context.MainCategries.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string Email){
            ViewBag.MainCategories = _context.MainCategries.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
            if(string.IsNullOrEmpty(Email)){
                TempData ["message"] = "Lütfen Eposta adresinizi giriniz.";
                return View();
            }

            var user = await _userManager.FindByEmailAsync(Email);
            if(user == null){
                TempData ["message"] = "Eposta adresine kayıtlı kullanıcı bulunamadı.";
                return View();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = Url.Action("ResetPassword","Account", new {user.Id, token});

            await _emailSender.SendEmailAsync(Email,"Parola Sıfırlama",$"Lütfen Şifrenizi değiştirmek için linke <a href='http://localhost:5034{url}'>tıklayınız.</a>");

            TempData ["message"] = "Eposta adresinize gelen link ile şifrenizi sıfırlayabilirsiniz.";

            return View();
        }

        public IActionResult ResetPassword(string Id, string token){
            ViewBag.MainCategories = _context.MainCategries.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
            if(Id == null || token == null){
                return RedirectToAction("Login");
            }

            var model = new ResetPasswordModel{Token = token};
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model){
            ViewBag.MainCategories = _context.MainCategries.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
            if(ModelState.IsValid){
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null){
                TempData ["message"] = "Bu mail adresiyle eşleşen kullanıcı yok";
                return RedirectToAction("Login");
            }

            var result = await _userManager.ResetPasswordAsync(user,model.Token,model.Password);
            if(result.Succeeded){
                TempData ["message"] = "Şifreniz değiştirildi.";
               return RedirectToAction("Login");
            }
            }
            return View(model);
        }

    }

}

