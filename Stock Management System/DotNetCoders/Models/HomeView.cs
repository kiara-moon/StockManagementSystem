using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetCoders.Model.Model;
using System.ComponentModel.DataAnnotations;

namespace DotNetCoders.Models
{
    public class HomeView
    {
        public int CategoryNumber { get; set; }
        public int ProductNumber { get; set; }
        public int CustomerNumber { get; set; }
        public int SupplierNumber { get; set; }
    }
}