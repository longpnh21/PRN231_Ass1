using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BusinessObject
{
    public partial class Member
    {
        public Member()
        {
            Orders = new HashSet<Order>();
        }

        public int MemberId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MaxLength(40)]
        public string CompanyName { get; set; }
        [Required]
        [MaxLength(15)]
        public string City { get; set; }
        [Required]
        [MaxLength(15)]
        public string Country { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [MaxLength(30)]
        public string Password { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
