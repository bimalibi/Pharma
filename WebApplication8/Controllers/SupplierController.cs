using Microsoft.AspNetCore.Mvc;
using Pharma.Application;
using Pharma.Contracts;
using Pharma.Entities;
using Pharma.EntityFramework;

namespace Pharma.UI.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierServices _supplierServices;
        public SupplierController(ISupplierServices supplierServices)
        {
            _supplierServices = supplierServices;
        }
        public async Task<IActionResult> Index(string searchStrin)
        {
            var supplier = await _supplierServices.GetSupplier(searchStrin);
            return View(supplier);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                await _supplierServices.AddSupplier(supplier);
                return RedirectToAction("Index");
            }
            return View(supplier);

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Supplier supplier = await _supplierServices.ToDeleteSupplier(id);   
            return View(supplier);
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
                await _supplierServices.DeleteSupplier(id);
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            Supplier ToEditSupplier = await _supplierServices.ToEditSupplier(id);
            return View(ToEditSupplier);
        }
        public async Task<IActionResult> EditConfirm(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                var sup = await _supplierServices.EditSupplier(supplier);
                return RedirectToAction("Index");
            }
            return View(supplier);
        }
    }
}
