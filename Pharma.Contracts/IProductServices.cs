using Pharma.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Contracts
{
    public interface IProductServices
    {
        public Task<List<Product>> GetProducts();
        public Task<List<Product>> GetProducts(string searchString);
        public Task<Product> AddProduct(Product product);
        public Task<Product> ToDeleteProduct(int id);
        public Task<Product> DeleteProduct(int id);
        public Task<Product> ToEditProduct(int id);
        public Task<Product> EditProduct(Product product);
    }
}
