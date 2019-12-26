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
    public class ProductController : Controller
    {
        ProductManager _productManager = new ProductManager();
        CategoryManager _categoryManager = new CategoryManager();
        [HttpGet]
        public ActionResult Add(int id)
        {
            ProductView productView = new ProductView();
            ViewBag.ActionName = "Show Product";
            
            if (id != 0)
            {
                Product product = _productManager.GetById(id);
                productView = Mapper.Map<ProductView>(product);
                ViewBag.Head = "Update";
                ViewBag.ButtonName = "Update";
                ViewBag.Message = null;
            }
            else
            {
                ViewBag.Message = null;
                ViewBag.Head = "Add";
                ViewBag.ButtonName = "Save";
            }
            CategoryListInitialize(productView);
            return View(productView);
        }

        private void CategoryListInitialize(ProductView productView)
        {
            productView.CategorySelectListItems = _categoryManager.GetAll().Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
        }

        [HttpPost]
        public ActionResult Add(ProductView productView)
        {
            string message = null;
            ViewBag.ActionName = "Add";
            ViewBag.ButtonName = "Save";
            ViewBag.Head = "Add";
            Product product = Mapper.Map<Product>(productView);

            if (productView.Id != 0)
            {
                ViewBag.ActionName = "Show";
                if (_productManager.Update(product))
                {
                    message = "Product is updated successfully";
                }
            }
            else
            {

                message = _productManager.Add(product) ? "Product is saved successfully" : "Product is not saved";
            }
            CategoryListInitialize(productView);
            ViewBag.Message = message;
            ViewBag.Save = "save";
            ModelState.Clear();
            return View(new ProductView());
        }
        [HttpGet]
        public ActionResult Show()
        {
            ProductView productView = new ProductView { Products = _productManager.GetAll() };
            ViewBag.Show = "Show";
            ViewBag.ActionName = "Add";
            return View(productView);
        }
        [HttpPost]
        public ActionResult Show(int id, string search)
        {
            ProductView productView;
            ViewBag.Show = "Show";
            ViewBag.ActionName = "Add";
            if (search == null)
            {
                ViewBag.Message = _productManager.Delete(id) ? "Deleted" : "Not deleted";
                productView = new ProductView { Products = _productManager.GetAll() };
            }
            else
            {
                productView = new ProductView { Products = _productManager.SearchProducts(search) };
            }
            return View(productView);
        }

        public JsonResult GetProductCode(ProductView product)
        {
            return Json(!_productManager.GetAll().Any(x => x.Code == product.Code && x.Id != product.Id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductName(ProductView product)
        {
            return Json(!_productManager.GetAll().Any(x => x.Name == product.Name && x.Id != product.Id), JsonRequestBehavior.AllowGet);
        }
	}
}
