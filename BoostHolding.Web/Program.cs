global using BoostHolding.DataAccessLayer;
global using BoostHolding.Web.Models;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
using BoostHolding.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSession();
var cs = builder.Configuration.GetConnectionString("AppDbContext");
builder.Services.AddDbContext<ApplicationDbContext>(optionsBuilder => optionsBuilder.UseSqlServer(cs));
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<UserSignedIn>();
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

app.UseRouting();

app.UseSession();

app.UseAuthorization();
app.MapAreaControllerRoute(
    name: "areas",
    areaName: "Admin",
    pattern: "{controller=Login}/{action=Index}/{id?}"
    );
//app.MapAreaControllerRoute(
//    name: "areas",
//    areaName: "Admin",
//    pattern: "{controller=Manager}/{action=Index}/{id?}"
//    );

app.MapAreaControllerRoute(
    name: "YonetimArea",
    areaName: "Yonetim",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
    );

//app.MapControllerRoute(
//            name: "areas",
//            pattern: "{area:exists}/{controller=Employee}/{action=Create}/{id?}"
//  );


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
