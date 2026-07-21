using Microsoft.EntityFrameworkCore;
using PortfolioMH.Models;

var builder = WebApplication.CreateBuilder(args);

// Add MVC
builder.Services.AddControllersWithViews();

// Add EF Core with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 8,
            maxRetryDelay: TimeSpan.FromSeconds(15),
            errorNumbersToAdd: null)));

// Add Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure HTTP pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Auto-create database and seed data
// Auto-create database and seed data
using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();
        DbSeeder.Seed(context);
    }
    catch (Exception ex)
    {
        Console.WriteLine("=================================================");
        Console.WriteLine("DATABASE STARTUP FAILED:");
        Console.WriteLine(ex.Message);
        Console.WriteLine("=================================================");
        Console.WriteLine("Check that SQL Server (SQLEXPRESS) is running and");
        Console.WriteLine("that the connection string in appsettings.json is correct.");
        throw;
    }
}

app.Run();
