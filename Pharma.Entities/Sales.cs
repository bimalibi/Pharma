using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Entities
{
    public class Sales
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime TransationDateAndTime { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int ManufacturerId { get; set; }
        public Manufacturer? Manufacturer { get; set;}
    }
}
