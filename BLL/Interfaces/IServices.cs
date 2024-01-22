using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IServices<T> where T : class
    {
        IList<T> GetAll();
        Task<T> Find(int? id);
        Task<int> Create(T item);
        Task Delete(int? id);
        Task Update(int id, T item);
    }
}
