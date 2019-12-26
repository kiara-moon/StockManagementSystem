using DotNetCoders.DatabaseContext.DatabaseContext;
using DotNetCoders.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoders.Repository.Repository
{
    public class StockRepository
    {
        ProjectDbContext _dbContext = new ProjectDbContext();
        public List<Stock> StockReports(int? produtId,DateTime sdate, DateTime eDate)
        {
            List<Stock> stocks = new List<Stock>();
            var allProduct = (dynamic)null;
            if (produtId != null)
            {
               allProduct = _dbContext.Products.Include(x => x.Category).Where(x=>x.Id==produtId).ToList();
            }
            else
            {
                allProduct = _dbContext.Products.Include(x => x.Category).ToList();
            }
          
            foreach (var product in allProduct)
            {
                int productId = product.Id;

                Stock stock = new Stock();
                stock.Code = product.Code;
                stock.Product = product.Name;
                stock.Category = product.Category.Name;
                stock.ReorderLevel = product.ReorderLevel;
                stock.OpeningBalance = GetTotalPurchaseProductByIdAndDate(productId, sdate) -
                                       GetTotalSaleProductByIdAndDate(productId, sdate);

                stock.In = GetTotalPurchaseProductByIdAndStartAndEndDate(productId, sdate, eDate);

                stock.Out = GetTotalSaleProductByIdAndStartAndEndDate(productId, sdate, eDate);

                stock.ClosingBalance = stock.OpeningBalance + stock.In - stock.Out;

                stocks.Add(stock);
            }

            return stocks;
        }

        public int GetTotalPurchaseProductByIdAndDate(int productId, DateTime startDate)
        {
            int total = 0;

            total = _dbContext.PurchaseProductInfos.Where(x => x.Product.Id == productId && x.PurchaseInfo.Date < startDate).Select(c => c.Quantity).DefaultIfEmpty(0).Sum();

            return total;
        }
        public int GetTotalSaleProductByIdAndDate(int productId, DateTime startDate)
        {
            int total = 0;

            total = _dbContext.SalesProductInfos.Where(x => x.Product.Id == productId && x.SalesInfo.Date < startDate).Select(c => c.Quantity).DefaultIfEmpty(0).Sum();

            return total;
        }
        public int GetTotalPurchaseProductByIdAndStartAndEndDate(int productId, DateTime startDate, DateTime endDate)
        {
            int total = 0;

            total = _dbContext.PurchaseProductInfos.Where(x => x.Product.Id == productId && x.PurchaseInfo.Date >= startDate && x.PurchaseInfo.Date <= endDate).Select(c => c.Quantity).DefaultIfEmpty(0).Sum();

            return total;

        }
        public int GetTotalSaleProductByIdAndStartAndEndDate(int productId, DateTime startDate, DateTime endDate)
        {
            int total = 0;

            total = _dbContext.SalesProductInfos.Where(x => x.Product.Id == productId && x.SalesInfo.Date >= startDate && x.SalesInfo.Date <= endDate).Select(c => c.Quantity).DefaultIfEmpty(0).Sum();

            return total;
        }

    }
}
