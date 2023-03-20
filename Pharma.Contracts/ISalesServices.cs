using Pharma.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Contracts
{
    public interface ISalesServices
    {
        public Task<List<Sales>> GetSales();
        public Task<Sales> AddSales(Sales sales);
    }
}
