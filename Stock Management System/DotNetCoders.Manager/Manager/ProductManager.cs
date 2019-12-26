using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCoders.Repository.Repository;
using DotNetCoders.Model.Model;

namespace DotNetCoders.Manager.Manager
{
    public class ProductManager
    {
        ProductRepository _productRepository = new ProductRepository();

        public bool Add(Product product)
        {
            return _productRepository.Add(product);
        }
        public bool Delete(int id)
        {
            return _productRepository.Delete(id);
        }
        public bool Update(Product product)
        {
            return _productRepository.Update(product);
        }
        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }
        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public List<Product> SearchProducts(string search)
        {
            return _productRepository.SearchProducts(search);
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _productRepository.GetAllByCategory(categoryId);
        }
    }
}
