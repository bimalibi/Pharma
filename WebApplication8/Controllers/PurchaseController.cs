using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pharma.Application;
using Pharma.Contracts;
using Pharma.Entities;
using Pharma.EntityFramework;

namespace Pharma.UI.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IPurchaseServices _purchaseServices;
        private readonly IProductServices _productServices;
        private readonly ISupplierServices _supplierServices;
        private readonly IManufacturerServices _manufacturerServices;
        private readonly AppDbContext _context;


        public PurchaseController(IPurchaseServices purchaseServices, IProductServices productServices, ISupplierServices supplierServices, IManufacturerServices manufacturerServices, AppDbContext appDbContext)
        {
            _purchaseServices = purchaseServices;
            _productServices = productServices;
            _supplierServices = supplierServices;
            _manufacturerServices = manufacturerServices;
            _context = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var purchases = await _purchaseServices.GetPurchases();
            return View(purchases);
        }

        public async Task<IActionResult> Create()
        {
            var productList = await _productServices.GetProducts();
            ViewData["ProductId"] = new SelectList(productList, "Id", "Name");

            var supplierList = await _supplierServices.GetSupplier();
            ViewData["SupplierId"] = new SelectList(supplierList, "Id", "Name");

            var manufacturerList = await _manufacturerServices.GetManufacturer();
            ViewData["ManufacturerId"] = new SelectList(manufacturerList, "Id", "Name");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                purchase.PurchaseDate = DateTime.Now;
                await _purchaseServices.AddPurchases(purchase);
                return RedirectToAction("Index");
            }
            return View(purchase);

        }

        public async Task<IActionResult> Delete(int id)
        {
            Purchase toDeletePurchase = await _purchaseServices.ToDeletePurchase(id);
            return View(toDeletePurchase);
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
                await _purchaseServices.DeletePurchase(id);
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            Purchase ToEditPurchase = await _purchaseServices.ToEditPurchase(id);
            return View(ToEditPurchase);
        }
        public async Task<IActionResult> EditConfirm(Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                var sup = await _purchaseServices.EditPurchase(purchase);
                return RedirectToAction("Index");
            }
            return View(purchase);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var data = await _purchaseServices.ToEditPurchase(id);
            return View(data);
        }

        public async Task<IActionResult> Getprice(int id)
        {
            var price=await _context.Purchases.Where(x=>x.ProductId==id).FirstOrDefaultAsync();
            Sales sales = new Sales();
            sales.Price = price.SellPrice;
            sales.ProductId= id;
            ViewBag.Sales= sales;
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> GetStock()
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

            return View(groupProducts);
        }
    }

}
