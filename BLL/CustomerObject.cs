using DAL.Interfaces;
using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CustomerObject
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerObject()
        {
            customerRepository = new CustomerRepository();
        }

        public async Task<Customer> Login(string email, string password)
        {
            return await customerRepository.Login(email, password);

        }

        public async Task<ICollection<Customer>> GetCustomerList()
        {
            var ListCustomer = await customerRepository.GetList();
            return ListCustomer;
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            var Customer = await customerRepository.GetById(id);
            return Customer;
        }

        public async Task<ICollection<Customer>> SearchCustomer(string search)
        {
            var ListCustomer = await customerRepository.SearchCustomer(search);
            return ListCustomer;
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            var isUpdated = await customerRepository.Update(customer);
            return isUpdated;
        }

        public async Task<bool> RemoveCustomer(int id)
        {
            var isDeleted = await customerRepository.Delete(id);
            return isDeleted;
        }
        public async Task<bool> AddCustomer(string email, string password, string fullname, DateTime birthday, string phone, byte status)
        {
            Customer NewCustomer = new Customer
            {
                EmailAddress = email,
                Password = password,
                CustomerBirthday = birthday,
                CustomerFullName = fullname,
                CustomerStatus = status,
                Telephone = phone
            };
            await customerRepository.Add(NewCustomer);
            return true;
        }
    }
}
