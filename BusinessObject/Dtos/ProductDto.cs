using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Dtos
{
    public class ProductDto
    {
        [Required]
        public int ProductId { get; set; }
        public int? CategoryId { get; set; }
        [Required]
        [MaxLength(40)]
        public string ProductName { get; set; }
        [MaxLength(20)]
        public string Weight { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int UnitsInStock { get; set; }
        public virtual CategoryDto Category { get; set; }
    }
}
