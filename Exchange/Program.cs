using Exchange.Data.Abstract;
using Exchange.Data.Concrete.EfCore;
using Exchange.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<IdentityContext>(
    options => options.UseSqlite(builder.Configuration["ConnectionStrings:SQLite_Connection"]));

builder.Services.AddScoped<IProductRepository, EfProductRepository>();
builder.Services.AddScoped<IMainCategoriesRepository, EfMainCategoriesRepository>();
builder.Services.AddScoped<ITagRepository, EfTagsRepository>();


builder.Services.AddIdentity<Users, UsersRole>().AddEntityFrameworkStores<IdentityContext>();

builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;

    options.User.RequireUniqueEmail = true;
});

builder.Services.ConfigureApplicationCookie(options =>{
    options.LoginPath ="/Account/Authentication";
});

var app = builder.Build();

SeedData.TestData(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "user_product",
    pattern: "AddProduct/",
    defaults: new {controller = "Products", action = "AddProduct" }
);

app.MapControllerRoute(
    name: "product_by_tag",
    pattern: "CategoriesList/{tag}",
    defaults: new {controller = "Home", action = "CategoriesList" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
