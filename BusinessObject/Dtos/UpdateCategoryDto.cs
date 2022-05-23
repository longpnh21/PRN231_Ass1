using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Dtos
{
    public class UpdateCategoryDto
    {
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(40)]
        public string CategoryName { get; set; }
    }
}
