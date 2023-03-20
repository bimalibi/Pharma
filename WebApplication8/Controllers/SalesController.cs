using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pharma.Application;
using Pharma.Contracts;
using Pharma.Entities;
using Pharma.EntityFramework;

namespace Pharma.UI.Controllers
{
    public class SalesController : Controller
    {
        private readonly ISalesServices _salesServices;
        private readonly IProductServices _productServices;
        private readonly IManufacturerServices _manufacturerServices;
        private readonly AppDbContext _context;
        public SalesController(ISalesServices salesServices, IProductServices productServices, IManufacturerServices manufacturerServices, AppDbContext appDbContext)
        {
            _salesServices = salesServices;
            _productServices = productServices;
            _manufacturerServices = manufacturerServices;
            _productServices = productServices;
            _manufacturerServices = manufacturerServices;
            _context = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var sales = await _salesServices.GetSales();
            return View(sales);
        }
 /*       public async Task<IActionResult> SelectProduct()
        {
            var productList = await _productServices.GetProducts();
            ViewData["ProductId"] = new SelectList(productList, "Id", "Name");
            return View();

        }*/

        public async Task<IActionResult> Create()
        {
            var productList = await _productServices.GetProducts();
            ViewData["ProductId"] = new SelectList(productList, "Id", "Name");
            var manufacturerList = await _manufacturerServices.GetManufacturer();
            ViewData["ManufacturerId"] = new SelectList(manufacturerList, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Sales sales)
        {
            int id = sales.ProductId;
            var data= _context.Purchases.Where(x=>x.ProductId==id).ToList();
            var td = data.Sum(x => x.Quantity);
            var data1 = _context.Sales.Where(x => x.ProductId == id).ToList();
            var td1= data1.Sum(x => x.Quantity);
            int q = sales.Quantity;
            if (ModelState.IsValid)
            {
                if ((td-td1) >= q)
                {
                    sales.TransationDateAndTime = DateTime.Now;
                    await _salesServices.AddSales(sales);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Quantity not available";
                    return RedirectToAction("Create");
                }
            }
            return View(sales);

        }

    }
}
