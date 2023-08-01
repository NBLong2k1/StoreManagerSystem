using System;
using System.Collections.Generic;

namespace BusinessObject.Model.Entities
{
    public partial class Cart
    {
        public int AccountId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public DateTime? DateAdd { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
