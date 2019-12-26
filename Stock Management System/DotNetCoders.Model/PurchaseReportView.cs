using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoders.Model
{
    public class PurchaseReportView
    {
        public string Code { get; set; }
        public string Product { get; set; }
        public string Category { get; set; }
        //public int SoldQuantity { get; set; }
        public int AvailableQuantity { get; set; }
        public double CostPrice { get; set; }
        //public double SalesPrice { get; set; }
        public double MRP { get; set; }
        public double Profit { get; set; }
    }
}
