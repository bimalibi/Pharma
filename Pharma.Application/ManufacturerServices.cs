using Microsoft.EntityFrameworkCore;
using Pharma.Contracts;
using Pharma.Entities;
using Pharma.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Application
{
    public class ManufacturerServices:IManufacturerServices
    {
        private readonly EntityFramework.AppDbContext _context;
        public ManufacturerServices(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<Manufacturer> AddManufacturer(Manufacturer manufacturer)
        {
            var manufacturerName = manufacturer.Name;
            int count = _context.Manufacturers.Count(item => item.Name == manufacturerName);
            if (count == 0)
            {
                _context.Add(manufacturer);
                await _context.SaveChangesAsync();
                return manufacturer;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Manufacturer>> GetManufacturer()
        {
            var manufacturer = await _context.Manufacturers.ToListAsync();
            return manufacturer;
        }
        public async Task<List<Manufacturer>> GetManufacturer(string searchString)
        {
            var manufacturers = _context.Manufacturers.AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                manufacturers = manufacturers.Where(x => x.Name.Contains(searchString));
            }
            return await manufacturers.ToListAsync();
        }
        public async Task<Manufacturer> ToDeleteManufacturer(int id)
        {
            var manufacturer = await _context.Manufacturers.FirstOrDefaultAsync(x => x.Id == id);
            return manufacturer;
        }

        public async Task<Manufacturer> DeleteManufacturer(int id)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer != null)
            {
                _context.Manufacturers.Remove(manufacturer);
                await _context.SaveChangesAsync();
            }
            return null;
        }
        public async Task<Manufacturer> ToEditManufacturer(int id)
        {
            var manufacture = await _context.Manufacturers.FirstOrDefaultAsync(x => x.Id == id);
            return manufacture;
        }
        public async Task<Manufacturer> EditManufacturer(Manufacturer manufacturer)
        {
            _context.Update(manufacturer);
            await _context.SaveChangesAsync();
            return manufacturer;
        }
    }
}
