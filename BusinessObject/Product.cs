using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BusinessObject
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public int? CategoryId { get; set; }
        [Required]
        [MaxLength(40)]
        public string ProductName { get; set; }
        [Required]
        [MaxLength(20)]
        public string Weight { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int UnitsInStock { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
