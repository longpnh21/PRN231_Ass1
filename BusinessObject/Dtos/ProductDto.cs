using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Dtos
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public int? CategoryId { get; set; }
        [MaxLength(40)]
        public string ProductName { get; set; }
        [MaxLength(20)]
        public string Weight { get; set; }
        public decimal UnitPrice { get; set; }
        [Range(0, int.MaxValue)]
        public int UnitsInStock { get; set; }
        public virtual CategoryDto Category { get; set; }
    }
}
