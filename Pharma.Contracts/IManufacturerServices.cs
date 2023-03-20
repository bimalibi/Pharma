using Pharma.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Contracts
{
    public interface IManufacturerServices
    {
        public Task<List<Manufacturer>> GetManufacturer();
        public Task<List<Manufacturer>> GetManufacturer(string searchString);
        public Task<Manufacturer> AddManufacturer(Manufacturer manufacturer);
        public Task<Manufacturer> ToDeleteManufacturer(int id);
        public Task<Manufacturer> DeleteManufacturer(int id);
        public Task<Manufacturer> ToEditManufacturer(int id);
        public Task<Manufacturer> EditManufacturer(Manufacturer manufacturer);
    }
}
