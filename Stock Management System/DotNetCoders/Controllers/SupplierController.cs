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
    public class SupplierController : Controller
    {
        SupplierManager _supplierManager = new SupplierManager();
        [HttpGet]
        public ActionResult Add(int id)
        {
            SupplierView supplierView = new SupplierView();
            ViewBag.ActionName = "Show Supplier";
            if (id != 0)
            {
                Supplier supplier = _supplierManager.GetById(id);
                supplierView = Mapper.Map<SupplierView>(supplier);
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
            return View(supplierView);
        }
        [HttpPost]
        public ActionResult Add(SupplierView supplierView)
        {
            string message = null;
            ViewBag.ActionName = "Add";
            ViewBag.ButtonName = "Save";
            ViewBag.Head = "Add";
            Supplier supplier = Mapper.Map<Supplier>(supplierView);

            if (supplierView.Id != 0)
            {
                ViewBag.ActionName = "Show";
                if (_supplierManager.Update(supplier))
                {
                    message = "Supplier is updated successfully";
                }
            }
            else
            {

                message = _supplierManager.Add(supplier) ? "Supplier is saved successfully" : "Supplier is not saved";
            }

            ViewBag.Message = message;
            ViewBag.Save = "save";
            ModelState.Clear();
            return View(new SupplierView());

        }
        [HttpGet]
        public ActionResult Show()
        {
            SupplierView supplierView = new SupplierView { Suppliers = _supplierManager.GetAll() };
            ViewBag.Show = "Show";
            ViewBag.ActionName = "Add";
            return View(supplierView);
        }
        public ActionResult Show(int id, string search)
        {
            SupplierView supplierView;
            ViewBag.Show = "Show";
            ViewBag.ActionName = "Add";
            if (search == null)
            {
                ViewBag.Message = _supplierManager.Delete(id) ? "Deleted" : "Not deleted";
                supplierView = new SupplierView { Suppliers = _supplierManager.GetAll() };
            }
            else
            {
                supplierView = new SupplierView { Suppliers = _supplierManager.SearchSuppliers(search) };
            }
            return View(supplierView);
        }
        public JsonResult GetSupplierCode(SupplierView supplier)
        {
            return Json(!_supplierManager.GetAll().Any(x => x.Code == supplier.Code && x.Id != supplier.Id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSupplierContact(SupplierView supplier)
        {
            return Json(!_supplierManager.GetAll().Any(x => x.Contact == supplier.Contact && x.Id != supplier.Id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSupplierEmail(SupplierView supplier)
        {
            return Json(!_supplierManager.GetAll().Any(x => x.Email == supplier.Email && x.Id != supplier.Id), JsonRequestBehavior.AllowGet);
        }
    }
}