using Microsoft.EntityFrameworkCore;
using Pharma.Contracts;
using Pharma.Entities;
using Pharma.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Application
{
    public class PurchaseServices : IPurchaseServices
    {
        private readonly AppDbContext _context;
        public PurchaseServices(AppDbContext context)
        {
            _context = context;
        }

        //Task<Object>
        public async Task<List<Purchase>> GetPurchases(string searchString)
        {
            var purhases = _context.Purchases.AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                var purhases1 = purhases.Where(x => x.Product.Name.Contains(searchString));
            }
            return await purhases.ToListAsync();
        }
        public async Task<List<Purchase>> GetPurchases()
        {
            var allPurchases = await _context.Purchases
                .Include(x => x.Manufacturer)
                .Include(x => x.Supplier)
                .Include(x => x.Product)
                .ToListAsync();
            return allPurchases;
        }
        public async Task<List<Purchase>> AddPurchases(Purchase purchase)
        {
            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();
            return null;
        }
        public async Task<Purchase> ToDeletePurchase(int id)
        {
            var purchase = await _context.Purchases
                 .Include(x => x.Manufacturer)
                .Include(x => x.Supplier)
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
            return purchase;
        }
        public async Task<Purchase> DeletePurchase(int id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase != null)
            {
                _context.Purchases.Remove(purchase);
                await _context.SaveChangesAsync();
            }
            return null;
        }

        public async Task<Purchase> ToEditPurchase(int id)
        {
            var purchase = await _context.Purchases
                .Include(x => x.Manufacturer)
                .Include(x => x.Supplier)
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
            return purchase;
        }
        public async Task<Purchase> EditPurchase(Purchase purchase)
        {
            _context.Update(purchase);
            await _context.SaveChangesAsync();
            return null;
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
            return groupProducts;
        }*/

    }
}
