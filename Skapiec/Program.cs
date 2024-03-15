using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Skapiec.Entities;
using Skapiec.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Conneting entity do DB
builder.Services.AddDbContext<SkapiecDBcontext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("Skapiec"))
    );
//services
builder.Services.AddScoped<ScraperService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

//https://www.youtube.com/watch?v=720CURaATNg
//https://www.youtube.com/watch?v=_uSw8sh7xKs