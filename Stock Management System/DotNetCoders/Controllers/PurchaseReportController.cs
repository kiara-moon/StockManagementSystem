using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetCoders.Manager.Manager;
using DotNetCoders.Model;

namespace DotNetCoders.Controllers
{
    public class PurchaseReportController : Controller
    {
        // GET: PurchaseReport
        PurchaseManager _purchaseManager = new PurchaseManager();
        [HttpGet]
        public ActionResult Index()
        {
            List<PurchaseReportView> aPurchaseReportView = new List<PurchaseReportView>();
            aPurchaseReportView = _purchaseManager.PurchaseReportViews(DateTime.MinValue, DateTime.MaxValue);
            ViewBag.Message = null;
            ViewBag.Report = aPurchaseReportView;
            return View();
        }
        [HttpPost]
        public ActionResult Index(DateTime startDate, DateTime endDate)
        {
            List<PurchaseReportView> aPurchaseReportView = new List<PurchaseReportView>();
            aPurchaseReportView = _purchaseManager.PurchaseReportViews(startDate, endDate);
            if (aPurchaseReportView.Count < 1)
            {
                ViewBag.Message = "No Data Found";
            }

            ViewBag.Report = aPurchaseReportView;
            return View();
        }
    }
}