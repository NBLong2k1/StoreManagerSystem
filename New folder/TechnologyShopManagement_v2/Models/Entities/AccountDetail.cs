using System;
using System.Collections.Generic;

namespace TechnologyShopManagement_v2.Models.Entities
{
    public partial class AccountDetail
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string? Permission { get; set; }

        public virtual Account? User { get; set; }
    }
}
