using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using DotNetCoders.Model.Model;
using DotNetCoders.Models;

namespace DotNetCoders
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CustomerView, Customer>();
                cfg.CreateMap<Customer, CustomerView>();
                cfg.CreateMap<CategoryView, Category>();
                cfg.CreateMap<Category, CategoryView>();
                cfg.CreateMap<SupplierView, Supplier>();
                cfg.CreateMap<Supplier, SupplierView>();
                cfg.CreateMap<ProductView, Product>();
                cfg.CreateMap<Product, ProductView>();
                cfg.CreateMap<PurchaseInfo, PurchaseView>();
                cfg.CreateMap<PurchaseView, PurchaseInfo>();
                cfg.CreateMap<SalesView, SalesInfo>();
                cfg.CreateMap<SalesInfo, SalesView>();
            });
        }
    }
}
