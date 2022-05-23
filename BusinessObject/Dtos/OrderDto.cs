using System;
using System.Collections.Generic;

namespace BusinessObject.Dtos
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int? MemberId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal? Freight { get; set; } = 0;

        public IList<OrderDetailDto> OrderDetails { get; set; }
    }
}
