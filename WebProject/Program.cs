﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebProject.DAL.Contexts;
using WebProject.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProjectDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProjectDbContextConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ProjectDbContext>();
builder.Services.AddScoped<DepartmentRepository>();
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}


app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
