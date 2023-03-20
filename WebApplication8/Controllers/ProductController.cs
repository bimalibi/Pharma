using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pharma.Application;
using Pharma.Contracts;
using Pharma.Entities;

namespace Pharma.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductServices _productServices;
        private readonly IManufacturerServices _manufacturerServices;
        public ProductController(IProductServices productServices, IManufacturerServices manufacturerServices)
        {
            _productServices = productServices;
            _manufacturerServices = manufacturerServices;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            var product = await _productServices.GetProducts(searchString);
            return View(product);
        }

        public async Task<IActionResult> Create()
        {
            var manufacturerList = await _manufacturerServices.GetManufacturer();
            ViewData["ManufacturerId"] = new SelectList(manufacturerList, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productServices.AddProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Product product = await _productServices.ToDeleteProduct(id);
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            if (id == null)
            {
                return Problem("Entity set 'AppDbContext.Employee'  is null.");
            }
            else
            {
                await _productServices.DeleteProduct(id);
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            Product ToEditProduct = await _productServices.ToEditProduct(id);
            return View(ToEditProduct);
        }
        public async Task<IActionResult> EditConfirm(Product product)
        {
            if (ModelState.IsValid)
            {
                var sup = await _productServices.EditProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

/*        public async void GetStock()
        {
            var groupProducts = await _context.Purchases
                .Include(x => x.Manufacturer)
                .Include(x => x.Product)
                .GroupBy(p => new { p.Product, p.Manufacturer })
                .Select(g => new
                {
                    Name = g.Key.Product.Name,
                    Manufacturer = new { g.Key.Manufacturer.Name },
                    TotalQuantity = g.Sum(p => p.Quantity)
                })
                .ToListAsync();
            return groupProducts;*/
        
    }
}
