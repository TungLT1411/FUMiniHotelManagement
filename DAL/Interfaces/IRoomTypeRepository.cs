using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRoomTypeRepository
    {
        Task<ICollection<RoomType>> GetList();
        Task<RoomType> GetById(int id);
        Task<bool> Delete(int id);
        Task<bool> Add(RoomType type);
        Task<bool> Update(RoomType type);
    }
}
