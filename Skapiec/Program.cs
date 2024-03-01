using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Skapiec.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

/*builder.Services.AddDbContext<SkapiecDBcontext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("SkapiecDbConnectionString"))
    );*/

using (var context = new SkapiecDBcontext())
{
    //creates db if not exists 
    context.Database.EnsureCreated();

    var product = new Product()
    {
        name = "Test",
        value = 2137,
        link = "www.google.com"
    };

    context.Products.Add(product);
    context.SaveChanges();

}
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

app.MapRazorPages();
app.Run();



//Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;
//https://www.youtube.com/watch?v=720CURaATNg