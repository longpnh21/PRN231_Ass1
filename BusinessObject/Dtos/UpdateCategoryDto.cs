using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Dtos
{
    public class UpdateCategoryDto
    {
        [Required]
        [MaxLength(40)]
        public string CategoryName { get; set; }
    }
}
