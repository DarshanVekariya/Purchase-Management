using Access.Interface;
using Access.Repository;
using Business.User;
using Bussiness.db;
using Bussiness.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using PurchaseManagement.IService;
using PurchaseManagement.Service;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession(options =>
{
    // Set a short timeout for testing purposes
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Bussiness.db.MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PurchaseManagementDB")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<MyDbContext>()
        .AddDefaultTokenProviders();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<SignInManager<ApplicationUser>>();
builder.Services.AddScoped<Auth>();

builder.Services.AddAuthorization();
builder.Services.AddHttpClient("CustomHttpClient")
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        var configuration = builder.Configuration;
        bool enableSslValidation = configuration.GetValue<bool>("EnableSslValidation");

        // Create an HttpClientHandler to customize SSL certificate validation
        HttpClientHandler clientHandler = new HttpClientHandler();

        // If SSL validation is disabled, ignore certificate errors
        if (!enableSslValidation)
        {
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
        }

        return clientHandler;
    })
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri("http://localhost:5220");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
