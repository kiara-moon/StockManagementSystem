using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetCoders.Manager.Manager;
using DotNetCoders.Model.Model;
using DotNetCoders.Models;

namespace DotNetCoders.Controllers
{
    public class HomeController : Controller
    {
        ProductManager _productManager = new ProductManager();
        CategoryManager _categoryManager = new CategoryManager();
        SupplierManager _supplierManager = new SupplierManager();
        CustomerManager _customerManager = new CustomerManager();
        // GET: Home
        public ActionResult Index()
        {
            HomeView homeView = new HomeView();
           
            var CategoryNumber= _categoryManager.GetAll().Count();
            var ProductNumber = _productManager.GetAll().ToList().Count();
            var CustomerNumber = _customerManager.GetAll().ToList().Count();
            var SupplierNumber = _supplierManager.GetAll().ToList().Count();

            ViewBag.CatNumber = CategoryNumber;
            ViewBag.ProNumber = ProductNumber;
            ViewBag.CusNumber = CustomerNumber;
            ViewBag.SupNumber = SupplierNumber;

            return View();

            //return View(homeView);
        }
    }
}