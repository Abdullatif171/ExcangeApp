
using System.Security.Claims;
using Exchange.Data.Abstract;
using Exchange.Data.Concrete.EfCore;
using Exchange.Entity;
using Exchange.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Exchange.Controllers
{
    public class ProductsController : Controller
    {
        private IProductRepository _ProductRepository;
        private IMainCategoriesRepository _MainCategriesRepository;
        private ITagRepository _TagRepository;
        private ICommentRepository _CommentRepository;
        private UserManager<Users> _userManager;
        private RoleManager<UsersRole> _roleManager;

        public ProductsController(IProductRepository context, IMainCategoriesRepository MainCategriesRepository, ITagRepository TagRepository, ICommentRepository CommentRepository, UserManager<Users> userManager, RoleManager<UsersRole> roleManager)
        {
            _ProductRepository = context;
            _MainCategriesRepository = MainCategriesRepository;
            _TagRepository = TagRepository;
            _CommentRepository = CommentRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Authorize]
        public IActionResult AddProduct()
        {
            ViewBag.Tags = _TagRepository.Tags.ToList();
            ViewBag.MainCategories = _MainCategriesRepository.MainCategories.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddProduct(ProductViewModel model){
            if(ModelState.IsValid)
            {
                ViewBag.Tags = _TagRepository.Tags.ToList();
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());

                _ProductRepository.CreateProducts(
                    new Products {
                        Title = model.Title,
                        Description = model.Description,
                        Price = model.Price,
                        ExchangeState = model.ExchangeState,
                        SellState = model.SellState,
                        Tags = model.Tags,
                        UserId = userId,
                        Image = "user.jpeg",
                    }
                );
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    

        [HttpPost]
        public JsonResult AddComment(int ProductId, string Text)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = User.FindFirstValue(ClaimTypes.Name);
            var avatar = User.FindFirstValue(ClaimTypes.UserData);

            var entity = new Comment {
                ProductId = ProductId,
                Text = Text,
                PublishedOn = DateTime.Now,
                UserId = int.Parse(userId ?? "")
            };
            _CommentRepository.CreateComment(entity);

            return Json(new { 
                username,
                Text,
                entity.PublishedOn,
                avatar
            });

        }

        public async Task<IActionResult> ProductDetails(int Id){

            ViewBag.MainCategories = _MainCategriesRepository.MainCategories.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();

            return View(await _ProductRepository
                        .Products
                        .Include(x => x.User)
                        .Include(x => x.Tags)
                        .Include(x => x.Comment)
                        .FirstOrDefaultAsync(p => p.ProductId == Id));
        }

        [Authorize]
        public async Task<IActionResult> UsersProducts()
        {
            ViewBag.MainCategories = _MainCategriesRepository.MainCategories.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
  
            var products = _ProductRepository.Products;

            return View(await products.ToListAsync());
        }

        [Authorize]
        public IActionResult ProductsEdit(int? id)
        {
            ViewBag.MainCategories = _MainCategriesRepository.MainCategories.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
            if (id == null)
            {
                return NotFound();
            }
            var model = _ProductRepository.Products.Include(i => i.Tags).FirstOrDefault(i => i.ProductId == id);
            if (model == null)
            {
                return NotFound();
            }

            ViewBag.Tags = _TagRepository.Tags.ToList();

            var entityUpdate = new ProductViewModel
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                ExchangeState = model.ExchangeState,
                SellState = model.SellState,
                Tags = model.Tags,
            };

            return View(entityUpdate);
        }

        [Authorize]
        [HttpPost]
        public IActionResult ProductsEdit(ProductViewModel model, int[] tagIds)
        {
            ViewBag.MainCategories = _MainCategriesRepository.MainCategories.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
            if (ModelState.IsValid)
            {
                var entityUpdate = new Products
                {
                    Title = model.Title,
                    Description = model.Description,
                    Price = model.Price,
                    ExchangeState = model.ExchangeState,
                    SellState = model.SellState,
                    Tags = model.Tags,
                };

                if (model.Image != null)
                {
                    var extension = Path.GetExtension(model.Image.FileName);
                    var newImageName = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    model.Image.CopyTo(stream);
                    entityUpdate.Image = newImageName;
                    _ProductRepository.ProductEdit(entityUpdate, tagIds);
                }
                else
                {
                    _ProductRepository.ProductEdit(entityUpdate, tagIds);
                }

                return RedirectToAction("UsersProducts");
            }
            ViewBag.Tags = _TagRepository.Tags.ToList();
            return View(model);
        }
    }
}

