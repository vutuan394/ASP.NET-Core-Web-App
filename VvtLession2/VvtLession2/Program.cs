using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

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

// Cấu hình routing - THÊM CÁC ROUTE MỚI Ở ĐÂY
app.MapControllerRoute(
    name: "products",
    pattern: "danh-sach-san-pham",
    defaults: new { controller = "Product", action = "GetAllProducts" }
);

app.MapControllerRoute(
    name: "product-details",
    pattern: "san-pham/{id}",
    defaults: new { controller = "Product", action = "Details" },
    constraints: new { id = @"\d+" } // Chỉ nhận số
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();