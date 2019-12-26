using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using DotNetCoders.Model.Model;

namespace DotNetCoders.Models
{
    public class PurchaseView
    {
        public PurchaseView()
        {
            PurchaseProductInfo = new PurchaseProductInfo();
            PurchaseInfos = new List<PurchaseInfo>();
            PurchaseProductInfos = new List<PurchaseProductInfo>();
            Supplier = new Supplier();
            Product = new Product();
            Category = new Category();
            ProductSelectListItems = new List<SelectListItem>();
            ProductName = new List<string>();
            ProductCode = new List<string>();
            TotalPrices = new List<string>(); 
        }
        //selfproperty
        public Supplier Supplier { get; set; }
        public List<SelectListItem> SupplierSelectListItems { get; set; }
        public Category Category { get; set; }
        public List<SelectListItem> CategorySelectListItems { get; set; }
        public Product Product { get; set; }
        public List<SelectListItem> ProductSelectListItems { get; set; }
        public int AvailableQuantity { get; set; }
        public double PreviousUnitPrice { get; set; }
        public double PreviousMRP { get; set; }
        public double TotalPrice { get; set; }
        public List<string> ProductName { get; set; }
        public List<string> ProductCode { get; set; }
        public List<string> TotalPrices { get; set; }

        //purchase property
        [Required]
        public PurchaseInfo PurchaseInfo { get; set; }


        //purchaseproduct property

        public List<PurchaseInfo> PurchaseInfos { get; set; }
        public List<PurchaseProductInfo> PurchaseProductInfos { get; set; }
        [Required]
        public PurchaseProductInfo PurchaseProductInfo { get; set; }

    }
}