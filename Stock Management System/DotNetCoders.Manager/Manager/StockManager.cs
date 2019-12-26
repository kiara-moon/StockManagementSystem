using DotNetCoders.Model.Model;
using DotNetCoders.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoders.Manager.Manager
{
   public class StockManager
    {
        private StockRepository _stockRepository = new StockRepository();
        public List<Stock> StockReports(int? productId,DateTime sdate, DateTime eDate)
        {
            return _stockRepository.StockReports(productId,sdate, eDate);
        }
    }
}
