using BTRS.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});



builder.Services.AddDbContext<SystemDbContext>
    (item => item.UseSqlServer(builder.Configuration.GetConnectionString("conn"))) ;

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Passenger}/{action=Login}/{id?}");

app.Run();
