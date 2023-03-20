using Microsoft.EntityFrameworkCore;
using Pharma.Application;
using Pharma.Contracts;
using Pharma.EntityFramework;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection") ?? throw new InvalidOperationException("Connection string 'InternshipMvcContext' not found.")));

builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<IManufacturerServices, ManufacturerServices>();
builder.Services.AddScoped<ISupplierServices, SupplierServices>();
builder.Services.AddScoped<IPurchaseServices, PurchaseServices>();
builder.Services.AddScoped<ISalesServices, SalesServices>();


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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
