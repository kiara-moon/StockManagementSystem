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
    public class CustomerController : Controller
    {
        CustomerManager _customerManager = new CustomerManager();
        // GET: Customer
        
        [HttpGet]
        public ActionResult Add(int id)
        {
            CustomerView customerView = new CustomerView();
            ViewBag.ActionName = "Show Customer";
            if (id != 0)
            {
                Customer customer = _customerManager.GetById(id);
                customerView = Mapper.Map<CustomerView>(customer);
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
            return View(customerView);
        }
        [HttpPost]
        public ActionResult Add(CustomerView customerView)
        {
            string message = null;
            ViewBag.ActionName = "Add";
            ViewBag.ButtonName = "Save";
            ViewBag.Head = "Add";
            Customer customer = Mapper.Map<Customer>(customerView);

            if (customerView.Id != 0)
            {
                ViewBag.ActionName = "Show";
                if (_customerManager.Update(customer))
                {
					message = "Customer is updated successfully";
                }
            }
            else
            {

                message = _customerManager.Add(customer) ? "Customer is saved successfully" : "Customer is not saved";
            }

            ViewBag.Message = message;
            ViewBag.Save = "save";
            ModelState.Clear();
            return View(new CustomerView());
           
        }
        [HttpGet]
        public ActionResult Show()
        {
            CustomerView customerView = new CustomerView {Customers = _customerManager.GetAll()};
             ViewBag.Show = "Show";
            ViewBag.ActionName = "Add";			
			return View(customerView);			
        }
        public ActionResult Show(int id, string search)
        {
            CustomerView customerView;
            ViewBag.Show = "Show";
            ViewBag.ActionName = "Add";
            if (search == null)
            {
                ViewBag.Message = _customerManager.Delete(id) ? "Deleted" : "Not deleted";
                customerView = new CustomerView { Customers = _customerManager.GetAll() };
            }
            else
            {
                customerView = new CustomerView { Customers = _customerManager.SearchCustomers(search) };
            }
            return View(customerView);
        }
        public JsonResult GetCustomerCode(CustomerView customer)
        {
            return Json(!_customerManager.GetAll().Any(x => x.Code == customer.Code && x.Id != customer.Id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCustomerContact(CustomerView customer)
        {
            return Json(!_customerManager.GetAll().Any(x => x.Contact == customer.Contact && x.Id != customer.Id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCustomerEmail(CustomerView customer)
        {
            return Json(!_customerManager.GetAll().Any(x => x.Email == customer.Email && x.Id != customer.Id), JsonRequestBehavior.AllowGet);
        }
    }
}