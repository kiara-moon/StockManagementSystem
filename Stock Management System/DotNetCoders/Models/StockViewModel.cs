using DotNetCoders.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetCoders.Models
{
    public class StockViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Stock> Stocks { get; set; }
        public List<Product> Products { get; set; }
    }
}