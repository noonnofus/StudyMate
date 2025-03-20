using Microsoft.EntityFrameworkCore;
using ASPDotNetProject.Models;
using MVCLesson5.Model;
using ASPDotNetProject.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IClassroomUserRepository, ClassroomUserRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();



var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
