using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly FUMiniHotelManagementContext _context;
        public RoomTypeRepository()
        {
            _context = new FUMiniHotelManagementContext();
        }

        public async Task<ICollection<RoomType>> GetList()
        {
            return await _context.RoomTypes.ToListAsync();
        }
       public async Task<RoomType> GetById(int id)
        {
            return await _context.RoomTypes.FirstOrDefaultAsync(a => a.RoomTypeId == id);
        }
        public async Task<bool> Add(RoomType RoomType)
        {
            _context.RoomTypes.Add(RoomType);
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
        public async Task<bool> Update(RoomType RoomType)
        {
            _context.RoomTypes.Update(RoomType);
            return await _context.SaveChangesAsync() > 0 ? true : false;

        }
        public async Task<bool> Delete(int id)
        {
            var _exisitngRoomType = await _context.RoomTypes.FirstOrDefaultAsync(a => a.RoomTypeId == id);

            if (_exisitngRoomType != null)
            {
                _context.RoomTypes.Remove(_exisitngRoomType);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }
    }
}
