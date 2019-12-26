using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetCoders.Models
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