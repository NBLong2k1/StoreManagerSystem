using System;
using System.Collections.Generic;

namespace TechnologyShopManagement_v2.Models.Entities
{
    public partial class Product
    {
        public Product()
        {
            Carts = new HashSet<Cart>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public string? CategoryCode { get; set; }
        public string? ProImage { get; set; }
        public string? ProDescription { get; set; }
        public int? Status { get; set; }

        public virtual Category? CategoryCodeNavigation { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
