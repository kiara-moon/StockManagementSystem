using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DotNetCoders.Model.Model;

namespace DotNetCoders.DatabaseContext.DatabaseContext
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext()
        {
            Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseProductInfo> PurchaseProductInfos { get; set; }
        public DbSet<PurchaseInfo> PurchaseInfos { get; set; }
        public DbSet<SalesProductInfo> SalesProductInfos { get; set; }
        public DbSet<SalesInfo> SalesInfos { get; set; }
    }
}
