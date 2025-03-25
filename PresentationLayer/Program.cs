using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Models;
using DataAccessLayer.Data;
using BusinessLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure Entity Framework and SQL Server
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity with Roles
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AppDBContext>()
    .AddSignInManager<SignInManager<ApplicationUser>>()
    .AddDefaultTokenProviders();

builder.Services.AddHttpContextAccessor();
// Register Repositories
builder.Services.AddScoped<IClassroomUserRepository, ClassroomUserRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();

// Configure Authentication and Authorization
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login/Index";
    options.AccessDeniedPath = "/Login/AccessDenied";
});

builder.Services.AddAuthentication().AddGoogle(options => {
    options.ClientId = "51060187860-nu54lvcluprkvibjpne6rurm3uo4k725.apps.googleusercontent.com";
    options.ClientSecret = "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";
});

// Enable Session
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await CreateAdminAccount(services);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) 
{
    Console.WriteLine("Running in Development Mode");
    app.UseDeveloperExceptionPage();

    app.Use(async (context, next) =>
    {
        await next();

        if (context.Response.StatusCode == 404)
        {
            throw new Exception($"404 Not Found: {context.Request.Path}");
        }
    });
} 
else 
{
    Console.WriteLine("Running in Production Mode");
    app.UseExceptionHandler("/Home/Error");
    app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


async Task CreateAdminAccount(IServiceProvider serviceProvider)
{
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();


   string[] roles = { "Admin", "User" };
    foreach (var role in roles)
    {
        var roleExists = await roleManager.RoleExistsAsync(role);
        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = role, NormalizedName = role.ToUpper() });
        }
    }

    string adminEmail = "admin@example.com";
    string adminPassword = "Admin@123";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        var newAdmin = new ApplicationUser 
        { 
            UserName = adminEmail, 
            Email = adminEmail, 
            Role = "Admin",
            Address = "123 Admin Street"
        };

        var result = await userManager.CreateAsync(newAdmin, adminPassword);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newAdmin, "Admin");
        }
        else
        {
            Console.WriteLine("❌ Cannot create an admin account");
        }
    }
}


