using System;
using DotNetCoders.DatabaseContext.DatabaseContext;
using DotNetCoders.Model.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using DotNetCoders.Model;

namespace DotNetCoders.Repository.Repository
{
    public class PurchaseRepository
    {
        ProjectDbContext _dbContext = new ProjectDbContext();
        public bool Insert(PurchaseInfo purchaseInfo)
        {
            
            _dbContext.PurchaseInfos.Add(purchaseInfo);
            return _dbContext.SaveChanges() > 0;
        }
        public List<PurchaseInfo> Show()
        {
            return _dbContext.PurchaseInfos.Include(c => c.Supplier).OrderByDescending(purrchase => purrchase.Date).ToList();
        }
        public List<PurchaseInfo> Search(string search)
        {
            return _dbContext.PurchaseInfos.Include(c => c.Supplier).Where(c => c.Code.Contains(search) || c.Supplier.Name.Contains(search)).ToList();
        }
        ProductRepository _productRepository = new ProductRepository();
        public List<string> PurchaseView(int productId)
        {
            List<string> productInfo = new List<string>();
            var purchaseProduct = _dbContext.PurchaseProductInfos.Where(c => c.ProductId == productId).Where(c => c.PurchaseInfo.Date < DateTime.Today).ToList();
            var salesProduct = _dbContext.SalesProductInfos.Where(c => c.ProductId == productId).Where(c => c.SalesInfo.Date < DateTime.Today).ToList();
            string code = _productRepository.GetAll().Where(c => c.Id == productId).Select(c => c.Code).FirstOrDefault();
            productInfo.Add(code);
            int stockIn = 0;
            double mrp = 0;
            double unitPrice = 0;
            int stockOut = 0;
            int availableProduct = stockIn - stockOut;
            if (purchaseProduct.Count < 1 && salesProduct.Count < 1)
            {

                productInfo.Add(availableProduct.ToString());
                productInfo.Add("0");
                productInfo.Add("0");
                return productInfo;
            }

            stockIn = purchaseProduct.Sum(purchase => purchase.Quantity);
            stockOut = salesProduct.Sum(sales => sales.Quantity);
            mrp = purchaseProduct[purchaseProduct.Count - 1].MRP;
            unitPrice = purchaseProduct[purchaseProduct.Count - 1].UnitPrice;
            availableProduct = stockIn - stockOut;
            productInfo.Add(availableProduct.ToString());
            productInfo.Add(unitPrice.ToString());
            productInfo.Add(mrp.ToString());
            return productInfo;
        }
        public int PurchaseView(int productId, DateTime startDate, DateTime endDate)
        {
            var purchaseProduct = _dbContext.PurchaseProductInfos.Where(c => c.ProductId == productId).Where(c => c.PurchaseInfo.Date < DateTime.Today).ToList();
            var salesProduct = _dbContext.SalesProductInfos.Where(c => c.ProductId == productId).Where(c => c.SalesInfo.Date < DateTime.Today).ToList();
            int stockIn = 0;
            int stockOut = 0;
            int availableProduct;
            if (purchaseProduct.Count < 1 && salesProduct.Count < 1)
            {

                availableProduct = 0;
                return availableProduct;
            }

            stockIn = purchaseProduct.Sum(purchase => purchase.Quantity);
            stockOut = salesProduct.Sum(sales => sales.Quantity);
            availableProduct = stockIn - stockOut;
            return availableProduct;
        }

        public List<PurchaseProductInfo> Details(int id)
        {
            var query = _dbContext.PurchaseProductInfos.Where(c => c.PurchaseInfoId == id).ToList();

            return query;
        }
        public List<PurchaseReportView> PurchaseReportViews(DateTime startDate, DateTime endDate)
        {
            List<PurchaseReportView> aList = new List<PurchaseReportView>();
            var purchaseProductList = _dbContext.PurchaseProductInfos
                .Include(c => c.PurchaseInfo)
                .Include(c => c.Product)
                .Include(c => c.Product.Category)
                .Where(c => c.PurchaseInfo.Date >= startDate && c.PurchaseInfo.Date <= endDate)
                .ToList();
            int index = 0;
            foreach (var purchaseProduct in purchaseProductList)
            {
                int flag = 1;
                if (aList.Count < 1)
                {
                    AddPurchaseProductReport(purchaseProduct, aList, startDate, endDate);
                }
                for (int j = 0; j < aList.Count; j++)
                {
                    if (aList[j].Product == purchaseProduct.Product.Name)
                    {
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
                    AddPurchaseProductReport(purchaseProduct, aList, startDate, endDate);
                }
                index++;
            }
            return aList;
        }

        private void AddPurchaseProductReport(PurchaseProductInfo purchaseProduct, List<PurchaseReportView> aList, DateTime startDate, DateTime endDate)
        {
            int productAvailable = PurchaseView(purchaseProduct.ProductId, startDate, endDate);
            aList.Add(new PurchaseReportView()
            {
                Product = purchaseProduct.Product.Name,
                Code = purchaseProduct.Product.Code,
                Category = purchaseProduct.Product.Category.Name,
                AvailableQuantity = productAvailable,
                CostPrice = purchaseProduct.UnitPrice * productAvailable,
                MRP = purchaseProduct.MRP * productAvailable,
                Profit = ((purchaseProduct.MRP * productAvailable) - (purchaseProduct.UnitPrice * productAvailable))
            });
        }
    }
}