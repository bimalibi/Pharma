using Pharma.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Contracts
{
    public interface IPurchaseServices
    {
        public Task<List<Purchase>> GetPurchases();
        public Task<List<Purchase>> AddPurchases(Purchase purchase);
        public Task<Purchase> ToDeletePurchase(int id);
        public Task<Purchase> DeletePurchase(int id);
        public Task<Purchase> ToEditPurchase(int id);
        public Task<Purchase> EditPurchase(Purchase purchase);
        
    }
}
