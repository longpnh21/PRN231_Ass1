using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BusinessObject
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        [Required]
        [MaxLength(40)]
        public string CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
