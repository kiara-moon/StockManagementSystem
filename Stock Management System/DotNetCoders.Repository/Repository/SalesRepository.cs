using System;
using DotNetCoders.DatabaseContext.DatabaseContext;
using DotNetCoders.Model.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DotNetCoders.Model;

namespace DotNetCoders.Repository.Repository
{
    public class SalesRepository
    {
        ProjectDbContext _dbContext = new ProjectDbContext();
        public bool Insert(SalesInfo salesInfo)
        {
            _dbContext.SalesInfos.Add(salesInfo);
            return _dbContext.SaveChanges() > 0;
        }
        public List<SalesInfo> Show()
        {
            return _dbContext.SalesInfos.Include(c => c.Customer).OrderByDescending(sales => sales.Date).ToList();
        }
        public List<SalesInfo> Search(string search)
        {
            return _dbContext.SalesInfos.Include(c => c.Customer).Where(c => c.Code.Contains(search) || c.Customer.Name.Contains(search)).ToList();
        }
        ProductRepository _productRepository = new ProductRepository();
        public List<string> SalesView(int productId, DateTime startDate, DateTime endDate)
        {
            List<string> productInfo = new List<string>();
            var purchaseProduct = _dbContext.PurchaseProductInfos.Where(c => c.ProductId == productId).Where(c => c.PurchaseInfo.Date < endDate).ToList();
            var salesProduct = _dbContext.SalesProductInfos.Where(c => c.ProductId == productId).Where(c => c.SalesInfo.Date < endDate).ToList();
            string reorderLevel = _productRepository.GetAll().Where(c => c.Id == productId).Select(c => c.ReorderLevel.ToString()).FirstOrDefault();
            productInfo.Add(reorderLevel);
            int stockIn = 0;
            double mrp = 0;
            int stockOut = 0;
            int availableProduct = stockIn - stockOut;
            if (purchaseProduct.Count < 1 && salesProduct.Count < 1)
            {
                productInfo.Add(availableProduct.ToString());
                productInfo.Add("0");
                return productInfo;
            }
            stockIn = purchaseProduct.Sum(purchase => purchase.Quantity);
            stockOut = salesProduct.Sum(sales => sales.Quantity);
            mrp = purchaseProduct[purchaseProduct.Count - 1].MRP;
            availableProduct = stockIn - stockOut;
            productInfo.Add(availableProduct.ToString());
            productInfo.Add(mrp.ToString());
            return productInfo;
        }
        public List<SalesProductInfo> Details(int id)
        {
            var query = _dbContext.SalesProductInfos.Where(c => c.SalesInfoId == id).ToList();

            return query;
        }
        public List<SalesReportView> SalesReportViews(DateTime startDate, DateTime endDate)
        {
            List<SalesReportView> aList = new List<SalesReportView>();
            
            var salesProductList = _dbContext.SalesProductInfos
                .Include(c => c.SalesInfo)
                .Include(c => c.Product)
                .Include(c => c.Product.Category)
                .Where(c => c.SalesInfo.Date >= startDate && c.SalesInfo.Date <= endDate)
                .ToList();

            int index = 0;
            foreach (var salesProduct in salesProductList)
            {
                int flag = 1;
                if (aList.Count < 1)
                {
                    AddSalesProductReport(salesProduct, aList, startDate, endDate);
                }
                for (int j = 0; j < aList.Count; j++)
                {

                    if (aList[j].Product == salesProduct.Product.Name)
                    {
                        var costPrice = CostPrice(salesProduct, endDate);
                        aList[j].Product = salesProduct.Product.Name;
                        aList[j].Code = salesProduct.Product.Code;
                        aList[j].Category = salesProduct.Product.Category.Name;
                        aList[j].SoldQuantity += salesProduct.Quantity;
                        aList[j].CostPrice += costPrice.Average()* salesProduct.Quantity;
                        aList[j].SalesPrice += salesProduct.PayableAmount * salesProduct.Quantity;
                        aList[j].Profit += ((salesProduct.PayableAmount * salesProduct.Quantity) -
                                           (costPrice.Average() * salesProduct.Quantity));
                        
                        flag = 0;
                        break;
                    }
                    else
                    {
                        flag++;
                    }
                }
                if (flag > 1)
                {
                    AddSalesProductReport(salesProduct, aList, startDate, endDate);
                }

                index++;
            }
            return aList;
        }
        private void AddSalesProductReport(SalesProductInfo salesProduct, List<SalesReportView> aList, DateTime startDate, DateTime endDate)
        {
            var costPrice = CostPrice(salesProduct, endDate);

            aList.Add(new SalesReportView()
            {
                Product = salesProduct.Product.Name,
                Code = salesProduct.Product.Code,
                Category = salesProduct.Product.Category.Name,
                SoldQuantity = salesProduct.Quantity,
                CostPrice = costPrice.Average() * salesProduct.Quantity,
                SalesPrice = salesProduct.PayableAmount * salesProduct.Quantity,
                Profit = ((salesProduct.PayableAmount * salesProduct.Quantity) - (costPrice.Average() * salesProduct.Quantity))
            });
        }

        private List<double> CostPrice(SalesProductInfo salesProduct, DateTime endDate)
        {
            var purchaseProductList = _dbContext.PurchaseProductInfos
                .Include(c => c.PurchaseInfo)
                .Include(c => c.Product)
                .Include(c => c.Product.Category)
                .Where(c => c.PurchaseInfo.Date <= endDate)
                .ToList();
            List<double> costPrice = new List<double>();
            foreach (var product in purchaseProductList)
            {
                if (product.ProductId == salesProduct.ProductId)
                {
                    costPrice.Add(product.UnitPrice);
                }
            }
            return costPrice;
        }
        
    }
}
