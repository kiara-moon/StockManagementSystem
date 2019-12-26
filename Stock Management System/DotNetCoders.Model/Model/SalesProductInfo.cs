using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoders.Model.Model
{
    public class SalesProductInfo
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double MRP { get; set; }
        public double DiscountAmount { get; set; }
        public double PayableAmount { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int SalesInfoId { get; set; }
        public SalesInfo SalesInfo { get; set; }
    }
}
