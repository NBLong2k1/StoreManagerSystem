using System;
using System.Collections.Generic;

namespace TechnologyShopManagement_v2.Models.Entities
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int? StaffId { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal? Discount { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime? DateOrder { get; set; }
        public DateTime? DateDelivery { get; set; }
        public DateTime? DateRecipt { get; set; }
        public int? Code { get; set; }
        public int? Status { get; set; }

        public virtual Account? Customer { get; set; }
        public virtual Account? Staff { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
