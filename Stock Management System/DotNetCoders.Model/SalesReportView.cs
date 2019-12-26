using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoders.Model
{
    public class SalesReportView
    {
        public string Code { get; set; }
        public string Product { get; set; }
        public string Category { get; set; }
        public int SoldQuantity { get; set; }
        public double CostPrice { get; set; }
        public double SalesPrice { get; set; }
        public double Profit { get; set; }
    }
}
