using Microsoft.EntityFrameworkCore;
using Pharma.Contracts;
using Pharma.Entities;
using Pharma.EntityFramework;

namespace Pharma.Application
{
    public class ProductServices: IProductServices
    {
        private readonly EntityFramework.AppDbContext _context;
        public ProductServices(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<Product> AddProduct(Product product)
        {
            var productName = product.Name;
            int count = _context.Products.Count(item => item.Name == productName);
            if (count == 0)
            {
               await _context.AddAsync(product);
                await _context.SaveChangesAsync();
                return product;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Product>> GetProducts()
        {
            var product=await _context.Products.Include(x => x.Manufacturer).ToListAsync();
            return product;
        }
        public async Task<List<Product>> GetProducts(string searchString)
        {
            var products= _context.Products.AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(x => x.Name.Contains(searchString));
            }
                return await products.ToListAsync();
        }
        public async Task<Product> ToDeleteProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }

        public async Task<Product> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return null;
        }
        public async Task<Product> ToEditProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }
        public async Task<Product> EditProduct(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}