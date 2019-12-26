using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCoders.DatabaseContext.DatabaseContext;
using DotNetCoders.Model.Model;

namespace DotNetCoders.Repository.Repository
{
    public class SupplierRepository
    {
        ProjectDbContext _dbContext = new ProjectDbContext();
        public bool Add(Supplier supplier)
        {
            _dbContext.Suppliers.Add(supplier);
            //dbContext.SaveChanges();

            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            Supplier aSupplier = _dbContext.Suppliers.FirstOrDefault(c => c.Id == id);
            _dbContext.Suppliers.Remove(aSupplier);

            return _dbContext.SaveChanges() > 0;
        }

        public bool Update(Supplier supplier)
        {
            Supplier aSupplier = _dbContext.Suppliers.FirstOrDefault(c => c.Id == supplier.Id);
            if (aSupplier != null)
            {
                aSupplier.Id = supplier.Id;
                aSupplier.Code = supplier.Code;
                aSupplier.Name = supplier.Name;
                aSupplier.Address = supplier.Address;
                aSupplier.Contact = supplier.Contact;
                aSupplier.Email = supplier.Email;
                aSupplier.ContactPerson = supplier.ContactPerson;
            }


            return _dbContext.SaveChanges() > 0;
        }

        public List<Supplier> GetAll()
        {
            return _dbContext.Suppliers.ToList();
        }
        public Supplier GetById(int id)
        {
            return _dbContext.Suppliers.First(c => c.Id == id);
        }

        public List<Supplier> SearchSuppliers(string search)
        {
            var suppliers = _dbContext.Suppliers.Where(c => c.Name.Contains(search) || c.Contact.Contains(search) || c.Email.Contains(search)).ToList();
            return suppliers;
        }


    }
}
