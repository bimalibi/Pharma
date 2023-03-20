using Pharma.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Contracts
{
    public interface ISupplierServices
    {
        public Task<List<Supplier>> GetSupplier();
        public Task<List<Supplier>> GetSupplier(string searchString);
        public Task<Supplier> AddSupplier(Supplier supplier);
        public Task<Supplier> ToDeleteSupplier(int id);
        public Task<Supplier> DeleteSupplier(int id);
        public Task<Supplier> ToEditSupplier(int id);
        public Task<Supplier> EditSupplier(Supplier supplier);
    }
}
