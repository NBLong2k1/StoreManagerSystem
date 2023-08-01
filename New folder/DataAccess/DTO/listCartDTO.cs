using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class listCartDTO
    {
        public int? Quantity { get; set; }
        public int? Id { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public string? CategoryCode { get; set; }
        public string? ProImage { get; set; }
        public string? ProDescription { get; set; }
        public int? Status { get; set; }
    }
}
