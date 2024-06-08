// FINAL CODE ************

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DiningOrderingSystem.Areas.Identity.Data;
using DiningOrderingSystem.Data;
using DiningOrderingSystem.Models;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationAuthDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationAuthDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDbContext<FoodItemDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDbContext<FoodOrderDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDbContext<NoticeDbContext>(options =>
    options.UseSqlite(connectionString));

//builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
//  .AddRoles<IdentityRole>()
//.AddEntityFrameworkStores<ApplicationAuthDbContext>();

builder.Services.AddDefaultIdentity<AppUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(24);
    //options.MaxFailedAccessAttemptsBeforeLockout = 3;
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationAuthDbContext>();


// Add services to the container.
builder.Services.AddControllersWithViews();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseExceptionHandler("/Home/Error");
// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
// app.UseHsts();

app.Use(async (context, next) =>
{
    await next();
    
    if(context.Response.StatusCode == 404)
    {
        context.Request.Path = "/Home/PageNotFoundPage";
        await next();
    }
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
