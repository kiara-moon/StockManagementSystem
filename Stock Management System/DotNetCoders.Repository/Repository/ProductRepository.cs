using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCoders.DatabaseContext.DatabaseContext;
using DotNetCoders.Model.Model;
using System.Data.Entity;

namespace DotNetCoders.Repository.Repository
{
    public class ProductRepository
    {
        ProjectDbContext _dbContext = new ProjectDbContext();


        public bool Add(Product product)
        {
            _dbContext.Products.Add(product);
            //dbContext.SaveChanges();

            return _dbContext.SaveChanges() > 0;
        }



        public bool Delete(int id)
        {
            Product aProduct = _dbContext.Products.FirstOrDefault(c => c.Id == id);
            _dbContext.Products.Remove(aProduct);

            return _dbContext.SaveChanges() > 0;
        }



        public bool Update(Product product)
        {
            Product aProduct = _dbContext.Products.FirstOrDefault(c => c.Id == product.Id);
            if (aProduct != null)
            {
                aProduct.Id = product.Id;
                aProduct.Code = product.Code;
                aProduct.Name = product.Name;
                aProduct.ReorderLevel = product.ReorderLevel;
                aProduct.Description = product.Description;
                
            }


            return _dbContext.SaveChanges() > 0;
        }



        public List<Product> GetAll()
        {
            return _dbContext.Products.Include(c=> c.Category).ToList();
        }
        public List<Product> GetAllByCategory(int categoryId)
        {
            var products = _dbContext.Products
                .Include(c => c.Category)
                .Where(c=>c.CategoryId == categoryId)
                .ToList();

            

            return products;

        }

        public Product GetById(int id)
        {
            return _dbContext.Products.First(c => c.Id == id);
        }

        public List<Product> SearchProducts(string search)
        {
            var products = _dbContext.Products.Include(c => c.Category).Where(c => c.Name.Contains(search) || c.Code.Contains(search) || c.Category.Name.Contains(search)).ToList();
            return products;
        }
    }
}
