
using System.Security.Claims;
using Exchange.Data.Abstract;
using Exchange.Data.Concrete.EfCore;
using Exchange.Entity;
using Exchange.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exchange.Controllers
{
    public class ProductsController : Controller
    {
        private IProductRepository _ProductRepository;
        private IMainCategoriesRepository _MainCategriesRepository;
        private ITagRepository _TagRepository;

        public ProductsController(IProductRepository context, IMainCategoriesRepository MainCategriesRepository, ITagRepository TagRepository)
        {
            _ProductRepository = context;
            _MainCategriesRepository = MainCategriesRepository;
            _TagRepository = TagRepository;
        }

        [Authorize]
        public IActionResult AddProduct(){
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
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());

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
    }
}