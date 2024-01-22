using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly FUMiniHotelManagementContext _context;
        public CustomerRepository()
        {
            _context = new FUMiniHotelManagementContext();
        }

        public async Task<ICollection<Customer>> GetList()
        {
            return await _context.Customers.ToListAsync();
        }
        public async Task<Customer> GetById(int id)
        {
            return await _context.Customers.FirstOrDefaultAsync(a => a.CustomerId == id);
        }
        public async Task<ICollection<Customer>> SearchCustomer(string search)
        {
            return await _context.Customers.Where(c=>c.CustomerFullName.Contains(search) ||
                                                     c.EmailAddress.Contains(search) ||
                                                     c.Telephone.Contains(search) ||
                                                     c.CustomerBirthday.ToString().Contains(search))
                .ToListAsync();
        }
        public async Task<bool> Add(Customer Customer)
        {
            _context.Customers.Add(Customer);
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
        public async Task<bool> Update(Customer Customer)
        {
            try
            {
                var existingCustomer = await _context.Customers.FirstOrDefaultAsync( e=> e.CustomerId == Customer.CustomerId);
                if (existingCustomer != null)
                {
                    existingCustomer.EmailAddress = Customer.EmailAddress;
                    existingCustomer.Telephone = Customer.Telephone;
                    existingCustomer.CustomerBirthday = Customer.CustomerBirthday;
                    existingCustomer.CustomerFullName = Customer.CustomerFullName;
                    existingCustomer.CustomerStatus = Customer.CustomerStatus;
                    existingCustomer.Password = Customer.Password;

                    _context.Customers.Update(existingCustomer);
                   return await _context.SaveChangesAsync() > 0 ? true : false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
           
        }
        public async Task<bool> Delete(int id)
        {
            var _exisitngCustomer = await _context.Customers.FirstOrDefaultAsync(a => a.CustomerId == id);

            if (_exisitngCustomer != null)
            {
                _context.Customers.Remove(_exisitngCustomer);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<Customer> Login(string email, string password)
        {
            return await _context.Customers.FirstOrDefaultAsync(u => u.EmailAddress == email && u.Password == password);  
        }


    }
}