using Exchange.Data.Abstract;
using Exchange.Data.Concrete.EfCore;
using Exchange.Entity;
using Exchange.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exchange.Controllers;

public class HomeController : Controller
{
    private IProductRepository _ProductRepository;
    private IMainCategoriesRepository _MainCategriesRepository;
    private ITagRepository _TagRepository;
    public HomeController(IProductRepository context, IMainCategoriesRepository MainCategriesRepository, ITagRepository TagRepository)
    {
        _ProductRepository = context;
        _MainCategriesRepository = MainCategriesRepository;
        _TagRepository = TagRepository;
    }
    public IActionResult Index()
    {
        var mainCategories = _MainCategriesRepository.MainCategories.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
        ViewBag.MainCategories = mainCategories;

        var Products = _ProductRepository.Products.Include(x => x.Tags).Include(x => x.Comment).ToList();

        return View(Products);
    }

    public async Task<IActionResult> CategoriesList(string tag)
    {
        var mainCategories = _MainCategriesRepository.MainCategories.Include(x => x.Categories).ThenInclude(x => x.Tags).ToList();
        ViewBag.MainCategories = mainCategories;

        var products = await _ProductRepository.Products.ToListAsync();

        if (!string.IsNullOrEmpty(tag))
        {
            var tags = _ProductRepository.Products.Where(x => x.Tags.Any(t => t.Url == tag));
            return View(new TagsViewModel { Products = await tags.ToListAsync() });
        }


        return View(new TagsViewModel { Products = products });
    }

}

