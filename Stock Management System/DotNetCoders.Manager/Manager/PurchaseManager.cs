using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCoders.Model;
using DotNetCoders.Model.Model;
using DotNetCoders.Repository.Repository;

namespace DotNetCoders.Manager.Manager
{
    
    public class PurchaseManager
    {
        PurchaseRepository _purchaseRepository = new PurchaseRepository();

        public bool Insert(PurchaseInfo purchaseInfo)
        {
            return _purchaseRepository.Insert(purchaseInfo);
        }

        public List<PurchaseInfo> Show()
        {
            return _purchaseRepository.Show();
        }

        public List<PurchaseInfo> Search(string search)
        {
            return _purchaseRepository.Search(search);
        }
        public List<string> PurchaseView(int productId)
        {
            return _purchaseRepository.PurchaseView(productId);
        }

        public List<PurchaseProductInfo> Details(int id)
        {
            return _purchaseRepository.Details(id);
        }

        public List<PurchaseReportView> PurchaseReportViews(DateTime startDate, DateTime endDate)
        {
            return _purchaseRepository.PurchaseReportViews(startDate, endDate);
        }
    }
}
