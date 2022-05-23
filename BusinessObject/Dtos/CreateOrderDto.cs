using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Dtos
{
    public class CreateOrderDto
    {
        [Required]
        public int MemberId { get; set; }
        public DateTime OrderDate { get; } = DateTime.Now;
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal? Freight { get; set; } = 0;

        public IList<OrderDetailDto> OrderDetails { get; set; }
    }
}
