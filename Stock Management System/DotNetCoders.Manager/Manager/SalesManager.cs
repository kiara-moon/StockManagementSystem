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
    public class SalesManager
    {
        SalesRepository _salesRepository = new SalesRepository();

        public bool Insert(SalesInfo salesInfo)
        {
            return _salesRepository.Insert(salesInfo);
        }

        public List<SalesInfo> Show()
        {
            return _salesRepository.Show();
        }

        public List<SalesInfo> Search(string search)
        {
            return _salesRepository.Search(search);
        }
        public List<string> SalesView(int productId, DateTime startDate, DateTime endDate)
        {
            return _salesRepository.SalesView(productId, startDate, endDate);
        }

        public List<SalesProductInfo> Details(int id)
        {
            return _salesRepository.Details(id);
        }

        public List<SalesReportView> SalesReportViews(DateTime startDate, DateTime endDate)
        {
            return _salesRepository.SalesReportViews(startDate, endDate);
        }
    }
}
