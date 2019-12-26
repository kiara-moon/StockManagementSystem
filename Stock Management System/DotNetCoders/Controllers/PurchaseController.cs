using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DotNetCoders.Manager.Manager;
using DotNetCoders.Model.Model;
using DotNetCoders.Models;

namespace DotNetCoders.Controllers
{
    public class PurchaseController : Controller
    {
        PurchaseManager _PurchaseManager = new PurchaseManager();
        SupplierManager _supplierManager = new SupplierManager();
        CategoryManager _categoryManager = new CategoryManager();
        ProductManager _productManager = new ProductManager();
        // GET: Purchase
        private PurchaseView aPurchaseView = new PurchaseView();
        [HttpGet]
        public ActionResult Add()
        {
            PurchaseInitialize();
            ViewBag.Message = null;
            return View(aPurchaseView);
        }

        private void PurchaseInitialize()
        {
            aPurchaseView.CategorySelectListItems = _categoryManager
                .GetAll()
                .Select(c => new SelectListItem() {Value = c.Id.ToString(), Text = c.Name}
                ).ToList();

            aPurchaseView.SupplierSelectListItems = _supplierManager
                .GetAll()
                .Select(s => new SelectListItem() {Value = s.Id.ToString(), Text = s.Name}
                ).ToList();
        }

        [HttpPost]
        public ActionResult Add(PurchaseView purchaseView)
        {
            
                PurchaseInfo aPurchaseInfo = purchaseView.PurchaseInfo;
                ViewBag.Message = _PurchaseManager.Insert(aPurchaseInfo) ? "Purchase is saved" : "Purchase is not Saved";

            PurchaseInitialize();

            return View(aPurchaseView);
        }

        [HttpGet]
        public ActionResult Show()
        {
            PurchaseView aPurchaseView = new PurchaseView();
            aPurchaseView.PurchaseInfos = _PurchaseManager.Show();
            ViewBag.product = null;
            return View(aPurchaseView);
        }
        [HttpPost]
        public ActionResult Show(string search)
        {
            PurchaseView aPurchaseView = new PurchaseView();
            aPurchaseView.PurchaseInfos = _PurchaseManager.Search(search);
            ViewBag.product = null;
            return View(aPurchaseView);
        }
        public JsonResult Details(int id)
        {
            aPurchaseView.PurchaseProductInfos = _PurchaseManager.Details(id);
            //PurchaseView aPurchaseView = new PurchaseView();
            //aPurchaseView.PurchaseInfos = _PurchaseManager.Show();
            //List<PurchaseProductInfo> query = new List<PurchaseProductInfo>();
            //query = 
            foreach (var product in aPurchaseView.PurchaseProductInfos)
            {
                var name = _productManager.GetById(product.ProductId);
                aPurchaseView.ProductName.Add(name.Name);
                aPurchaseView.ProductCode.Add(name.Code);
                var totalPrice = product.Quantity * product.UnitPrice;
                aPurchaseView.TotalPrices.Add(totalPrice.ToString());
            }
            var x = aPurchaseView;
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBillNo(PurchaseInfo purchaseInfo)
        {
            return Json(!_PurchaseManager.Show().Any(x => x.InvoiceNo == purchaseInfo.InvoiceNo && x.Id != purchaseInfo.Id), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult AjaxMethod(string type, int value)
        {
            switch (type)
            {
                case "Category_Id":
                    aPurchaseView.ProductSelectListItems = _productManager
                        .GetAllByCategory(value)
                        .Select(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.Name }
                        ).ToList();
                    break;
                case "Product_Id":
                    List<string> productPurchaseInfo = _PurchaseManager.PurchaseView(value);
                    aPurchaseView.Product.Code = productPurchaseInfo[0];
                    aPurchaseView.AvailableQuantity = Convert.ToInt32(productPurchaseInfo[1]);
                    aPurchaseView.PreviousUnitPrice = Convert.ToDouble(productPurchaseInfo[2]);
                    aPurchaseView.PreviousMRP = Convert.ToDouble(productPurchaseInfo[3]);
                    break;
            }
            return Json(aPurchaseView);
        }
    }
}