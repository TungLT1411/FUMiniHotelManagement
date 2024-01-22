using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICustomerRepository
    {
        Task<ICollection<Customer>> GetList();
        Task<Customer> GetById(int id);
        Task<ICollection<Customer>> SearchCustomer(string search);
        Task<bool> Delete(int id);
        Task<bool> Add(Customer customer);
        Task<bool> Update(Customer customer);
        Task<Customer> Login(string email, string password);
    }
}
