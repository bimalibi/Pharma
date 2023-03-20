using System.ComponentModel.DataAnnotations;

namespace Pharma.Entities
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter a 10-digit number")]
        public long Phone { get; set; }
        public string Address { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
    }
}