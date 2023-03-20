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
    public class SalesServices:ISalesServices
    {
        private readonly AppDbContext _context;
        public SalesServices(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<List<Sales>> GetSales()
        {
            var sales = await _context.Sales
                .Include(x => x.Manufacturer)
                .Include(x => x.Product)
                .ToListAsync();
            return sales;
        }
        public async Task<Sales> AddSales(Sales sales)
        {
            _context.Sales.Add(sales);
            await _context.SaveChangesAsync();
            return null;
        }

   }
}
