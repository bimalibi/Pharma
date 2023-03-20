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
    public class SupplierServices: ISupplierServices
    {
        private readonly EntityFramework.AppDbContext _context;
        public SupplierServices(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<Supplier> AddSupplier(Supplier supplier)
        {
            var supplierName = supplier.Name;
            int count = _context.Suppliers.Count(item => item.Name == supplierName);
            if (count == 0)
            {
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return supplier;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Supplier>> GetSupplier(string searchString)
        {
            var supplier = _context.Suppliers.AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                supplier = supplier.Where(x => x.Name.Contains(searchString));
            }
            return await supplier.ToListAsync();
        }
        public async Task<List<Supplier>> GetSupplier()
        {
            var supplier = await _context.Suppliers.ToListAsync();
            return supplier;
        }
        public async Task<Supplier> ToDeleteSupplier(int id)
        {
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(x => x.Id == id);
            return supplier;
        }

        public async Task<Supplier> DeleteSupplier(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
                await _context.SaveChangesAsync();
            }
            return null;
        }
        public async Task<Supplier> ToEditSupplier(int id)
        {
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(x => x.Id == id);
            return supplier;
        }
        public async Task<Supplier> EditSupplier(Supplier supplier)
        {
            _context.Update(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }
    }
}
