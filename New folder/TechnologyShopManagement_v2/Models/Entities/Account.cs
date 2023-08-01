using System;
using System.Collections.Generic;

namespace TechnologyShopManagement_v2.Models.Entities
{
    public partial class Account
    {
        public Account()
        {
            AccountDetails = new HashSet<AccountDetail>();
            Carts = new HashSet<Cart>();
            OrderCustomers = new HashSet<Order>();
            OrderStaffs = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string? Name { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int? Role { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<AccountDetail> AccountDetails { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> OrderCustomers { get; set; }
        public virtual ICollection<Order> OrderStaffs { get; set; }
    }
}
