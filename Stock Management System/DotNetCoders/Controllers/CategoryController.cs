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
    public class CategoryController : Controller
    {
        CategoryManager _categoryManager = new CategoryManager();

        // GET: Category
        [HttpGet]
        public ActionResult Add(int id)
        {
            CategoryView categoryView = new CategoryView();
            ViewBag.ActionName = "Show Category";
            if (id != 0)
            {
                Category category = _categoryManager.GetById(id);
                categoryView = Mapper.Map<CategoryView>(category);
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
            
            return View(categoryView);
        }
        [HttpPost]
        public ActionResult Add(CategoryView categoryView)
        {
            string message = null;
            ViewBag.ActionName = "Add";
            ViewBag.ButtonName = "Save";
            ViewBag.Head = "Add";
            Category category = Mapper.Map<Category>(categoryView);

            if (categoryView.Id != 0)
            {
                ViewBag.ActionName = "Show";
                if (_categoryManager.Update(category))
                {
                    message = "Category is updated successfully";
                }
            }
            else
            {
                
                message = _categoryManager.Add(category) ? "Category is saved successfully" : "Category is not saved";
            }
            
            ViewBag.Message = message;
            ViewBag.Save = "save";
            ModelState.Clear();
            return View(new CategoryView());
        }
        [HttpGet]
        public ActionResult Show()
        {
            CategoryView categoryView = new CategoryView { Categories = _categoryManager.GetAll() };
            ViewBag.Show = "Show";
            ViewBag.ActionName = "Add";
            return View(categoryView);
        }
        [HttpPost]
        public ActionResult Show(int id, string search)
        {
            CategoryView categoryView;
            ViewBag.Show = "Show";
            ViewBag.ActionName = "Add";
            if (search == null)
            {
                ViewBag.Message = _categoryManager.Delete(id) ? "Deleted" : "Not deleted";
                categoryView = new CategoryView { Categories = _categoryManager.GetAll() };
            }
            else
            {
                categoryView = new CategoryView { Categories = _categoryManager.SearchCategories(search) };
            }
            return View(categoryView);
        }

        public JsonResult GetCategoryCode(CategoryView category)
        {   
            return Json(!_categoryManager.GetAll().Any(x => x.Code == category.Code && x.Id != category.Id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCategoryName(CategoryView category)
        {
            return Json(!_categoryManager.GetAll().Any(x => x.Name == category.Name && x.Id != category.Id), JsonRequestBehavior.AllowGet);

        }
    }
}