using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Dtos
{
    public class CreateCategoryDto
    {
        [Required]
        [MaxLength(40)]
        public string CategoryName { get; set; }
    }
}
