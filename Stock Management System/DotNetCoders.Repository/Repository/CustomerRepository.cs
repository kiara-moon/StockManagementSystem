using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCoders.DatabaseContext.DatabaseContext;
using DotNetCoders.Model.Model;

namespace DotNetCoders.Repository.Repository
{
    public class CustomerRepository
    {
        ProjectDbContext _dbContext = new ProjectDbContext();
        public bool Add(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            //dbContext.SaveChanges();

            return _dbContext.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            Customer aCustomer = _dbContext.Customers.FirstOrDefault(c => c.Id == id);
            _dbContext.Customers.Remove(aCustomer);

            return _dbContext.SaveChanges() > 0;
        }
        public bool Update(Customer customer)
        {
            Customer aCustomer = _dbContext.Customers.FirstOrDefault(c => c.Id == customer.Id);
            if (aCustomer != null)
            {
                aCustomer.Id = customer.Id;
                aCustomer.Code = customer.Code;
                aCustomer.Name = customer.Name;
                aCustomer.Address = customer.Address;
                aCustomer.Contact = customer.Contact;
                aCustomer.Email = customer.Email;
                aCustomer.LoyaltyPoint = customer.LoyaltyPoint;
            }


            return _dbContext.SaveChanges() > 0;
        }
        public List<Customer> GetAll()
        {
            return _dbContext.Customers.ToList();
        }
        public Customer GetById(int id)
        {
            return _dbContext.Customers.First(c => c.Id == id);
        }

        public List<Customer> SearchCustomers(string search)
        {
            var customers = _dbContext.Customers.Where(c => c.Name.Contains(search) || c.Contact.Contains(search) || c.Email.Contains(search)).ToList();
            return customers;
        }
    }
}
