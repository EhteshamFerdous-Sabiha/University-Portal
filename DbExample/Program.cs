using DbExample.CustomAttribute;
using DbExample.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Database connection string 
builder.Services.AddDbContext<AtpdbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ATPDbContext")
    ));

// Session
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromMinutes(30);
    opt.Cookie.HttpOnly = true;
    opt.Cookie.IsEssential = true;
});

// Authentication
builder.Services.AddAuthentication("DbAuth")
    .AddCookie("DbAuth", opt =>
    {
        opt.AccessDeniedPath = "/Credential/Denied";
        opt.LoginPath = "/Credential/Login";
        opt.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });


builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));


builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();