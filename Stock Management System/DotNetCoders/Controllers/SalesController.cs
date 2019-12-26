using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DotNetCoders.Manager.Manager;
using DotNetCoders.Model.Model;
using DotNetCoders.Models;

namespace DotNetCoders.Controllers
{
    public class SalesController : Controller
    {
        CustomerManager _customerManager = new CustomerManager();
        PurchaseManager _PurchaseManager = new PurchaseManager();
        CategoryManager _categoryManager = new CategoryManager();
        ProductManager _productManager = new ProductManager();
        SalesManager _salesManager = new SalesManager();
        SalesView aSalesView = new SalesView();
        // GET: Sales
        [HttpGet]
        public ActionResult Add()
        {
            SalesView salesView = new SalesView();
            InitialSalesView(salesView);
            return View(salesView);
        }

        private void InitialSalesView(SalesView salesView)
        {
            salesView.CustomerSelectListItems = _customerManager.GetAll()
                .Select(c => new SelectListItem()
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }
                ).ToList();
            salesView.CategorySelectListItems = _categoryManager.GetAll()
                .Select(c => new SelectListItem()
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }
                ).ToList();
        }

        [HttpPost]
        public ActionResult Add(SalesView salesView)
        {
            //CustomerView _customerView = new CustomerView();
            string message = null;
            ViewBag.ActionName = "Add";
            ViewBag.ButtonName = "Save";
            SalesInfo salesInfo = salesView.SalesInfo;
            int LoyaltyPoint = salesView.Customer.LoyaltyPoint - salesView.Discount + (int) (salesView.GrantTotal / 1000);
            int Id = salesView.SalesInfo.CustomerId;
            Customer customer = new Customer();
            customer = _customerManager.GetById(Id);
            customer.LoyaltyPoint = LoyaltyPoint;
            _customerManager.Update(customer);
            message = _salesManager.Insert(salesInfo) ? "Saved" : "Does not save";
            ViewBag.Message = message;
            SalesView aSalesView = new SalesView();
            InitialSalesView(aSalesView);
            return View(aSalesView);
        }

        [HttpGet]
        public ActionResult Show()
        {
            SalesView aSalesView = new SalesView();
            aSalesView.SalesInfos = _salesManager.Show();
            ViewBag.product = null;
            return View(aSalesView);
        }

        [HttpPost]
        public ActionResult Show(string search)
        {
            SalesView aSalesView = new SalesView();
            aSalesView.SalesInfos = _salesManager.Search(search);
            ViewBag.product = null;
            return View(aSalesView);
        }
        [HttpPost]
        public JsonResult Details(int id)
        {
            aSalesView.SalesProductInfos = _salesManager.Details(id);
            //PurchaseView aPurchaseView = new PurchaseView();
            //aPurchaseView.PurchaseInfos = _PurchaseManager.Show();
            //List<PurchaseProductInfo> query = new List<PurchaseProductInfo>();
            //query = 
            foreach (var product in aSalesView.SalesProductInfos)
            {
                var name = _productManager.GetById(product.ProductId);
                aSalesView.ProductName.Add(name.Name);
                aSalesView.ProductCode.Add(name.Code);
                var totalPrice = product.Quantity * product.MRP;
                aSalesView.TotalPrices.Add(totalPrice.ToString());
            }
            var x = aSalesView;
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AjaxMethod(string type, int value)
        {
            switch (type)
            {
                case "SalesInfo_CustomerId":
                    aSalesView.LoyaltyPoint = _customerManager
                        .GetById(value).LoyaltyPoint;
                    break;
                case "Category_Id":
                    aSalesView.ProductSelectListItems = _productManager
                        .GetAllByCategory(value)
                        .Select(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.Name }
                        ).ToList();
                    break;
                case "Product_Id":
                    List<string> productSalesInfo = _salesManager.SalesView(value, DateTime.MinValue, DateTime.MaxValue);
                    aSalesView.Product.ReorderLevel = Convert.ToInt32(productSalesInfo[0]);
                    aSalesView.AvailableQuantity = Convert.ToInt32(productSalesInfo[1]);
                    aSalesView.SalesProductInfo.MRP = Convert.ToDouble(productSalesInfo[2]);
                    break;
            }
            return Json(aSalesView);
        }
    }
}