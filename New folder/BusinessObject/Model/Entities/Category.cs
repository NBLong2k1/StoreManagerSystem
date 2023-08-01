using System;
using System.Collections.Generic;

namespace BusinessObject.Model.Entities
{
    public partial class Category
    {
        public Category()
        {
            InverseSubCatCodeNavigation = new HashSet<Category>();
            Products = new HashSet<Product>();
        }

        public string Code { get; set; } = null!;
        public string? Name { get; set; }
        public string? SubCatCode { get; set; }
        public int? Tt { get; set; }

        public virtual Category? SubCatCodeNavigation { get; set; }
        public virtual ICollection<Category> InverseSubCatCodeNavigation { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
