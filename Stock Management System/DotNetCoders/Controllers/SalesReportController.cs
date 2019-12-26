using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetCoders.Manager.Manager;
using DotNetCoders.Models;

namespace DotNetCoders.Controllers
{
    public class SalesReportController : Controller
    {
        // GET: SalesReport
        SalesManager _salesManager = new SalesManager();
        [HttpGet]
        public ActionResult Index()
        {
            var salesReportViews = _salesManager.SalesReportViews(DateTime.MinValue, DateTime.MaxValue);
            ViewBag.Message = null;
            ViewBag.Report = salesReportViews;
            return View();
        }
        [HttpPost]
        public ActionResult Index(DateTime startDate, DateTime endDate)
        {
            var salesReportViews = _salesManager.SalesReportViews(startDate, endDate);
            if (salesReportViews.Count < 1)
            {
                ViewBag.Message = "No Data Found";
            }

            ViewBag.Report = salesReportViews;
            return View();
        }
    }
}