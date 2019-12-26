using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCoders.DatabaseContext.DatabaseContext;
using DotNetCoders.Model.Model;

namespace DotNetCoders.Repository.Repository
{
   public  class CategoryRepository
    {
        ProjectDbContext _dbContext = new ProjectDbContext();
        public bool Add(Category category)
        {
            _dbContext.Categories.Add(category);


            return _dbContext.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            Category aCategory = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            _dbContext.Categories.Remove(aCategory);

            return _dbContext.SaveChanges() > 0;
        }
        
        public bool Update(Category category)
        {
            Category aCategory = _dbContext.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (aCategory != null)
            {
                aCategory.Id = category.Id;
                aCategory.Code = category.Code;
                aCategory.Name = category.Name;

            }


            return _dbContext.SaveChanges() > 0;
        }
        public List<Category> GetAll()
        {
            return _dbContext.Categories.ToList();
        }
        public Category GetById(int id)
        {
            return _dbContext.Categories.First(c => c.Id == id);
        }

        public List<Category> SearchCategories(string search)
        {
            var categories = _dbContext.Categories.Where(c => c.Name.Contains(search)|| c.Code.Contains(search)).ToList();
            return categories;
        }

        //public JsonResult IsProductNameExist(string ProductName, int? Id)
        //{
        //    var validateName = db.Products.FirstOrDefault
        //                        (x => x.ProductName == ProductName && x.Id != Id);
        //    if (validateName != null)
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(true, JsonRequestBehavior.AllowGet);
        //    }
        
   }
}
