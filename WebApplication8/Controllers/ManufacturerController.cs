using Microsoft.AspNetCore.Mvc;
using Pharma.Application;
using Pharma.Contracts;
using Pharma.Entities;

namespace Pharma.UI.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerServices _manufacturerServices;
        public ManufacturerController(IManufacturerServices manufacturerServices)
        {
            _manufacturerServices = manufacturerServices;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            var manufacturer = await _manufacturerServices.GetManufacturer(searchString);
            return View(manufacturer);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                await _manufacturerServices.AddManufacturer(manufacturer);
                return RedirectToAction("Index");
            }
            return View(manufacturer);

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Manufacturer manufacturer = await _manufacturerServices.ToDeleteManufacturer(id);
            return View(manufacturer);
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
                await _manufacturerServices.DeleteManufacturer(id);
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            Manufacturer ToEditManufacturer = await _manufacturerServices.ToEditManufacturer(id);
            return View(ToEditManufacturer);
        }
        public async Task<IActionResult> EditConfirm(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                var sup = await _manufacturerServices.EditManufacturer(manufacturer);
                return RedirectToAction("Index");
            }
            return View(manufacturer);
        }
    }
}
