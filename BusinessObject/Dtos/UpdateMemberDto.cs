using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObject.Dtos
{
    public class UpdateMemberDto
    {
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
        [JsonIgnore]
        public string Password { get; set; }
    }
}
