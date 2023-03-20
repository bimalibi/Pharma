using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Entities
{
    public class Purchase
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int CostPrice { get; set; }
        public int SellPrice { get; set; }
        public int OrderLot { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ManufacturedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int ProductId { get; set; }   
        public Product? Product { get; set; }
        public int ManufacturerId { get; set; }
        public Manufacturer? Manufacturer { get; set; }
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

    }
}
