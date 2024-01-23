using DAL.Interfaces;
using DAL.Models;
using DAL.ModelsDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class RoomInformationRepository : IRoomInformationRepository
    {
        private readonly FUMiniHotelManagementContext _context;
        public RoomInformationRepository()
        {
            _context = new FUMiniHotelManagementContext();
        }



        public async Task<ICollection<RoomInformationDTO>> GetListWithType()
        {
            return await _context.RoomInformations.Include(e => e.RoomType).Select(info => new RoomInformationDTO
            {
                RoomId = info.RoomId,
                RoomNumber = info.RoomNumber,
                RoomDetailDescription = info.RoomDetailDescription,
                RoomMaxCapacity = info.RoomMaxCapacity,
                RoomTypeId = info.RoomTypeId,
                RoomStatus = info.RoomStatus,
                RoomPricePerDay = info.RoomPricePerDay,
                RoomTypeName = info.RoomType != null ? info.RoomType.RoomTypeName : null,
                TypeDescription = info.RoomType != null ? info.RoomType.TypeDescription : null,
                TypeNote = info.RoomType != null ? info.RoomType.TypeNote : null
            }).ToListAsync();
        }

        public async Task<ICollection<RoomInformationDTO>> GetListRoomAvailabel(DateTime startDate, DateTime endDate, int roomTypeId)
        {
            return await _context.RoomInformations.Include(e => e.RoomType).Include(e=>e.BookingDetails).Where(room => room.RoomTypeId == roomTypeId
                               || room.BookingDetails.All(booking =>
                                   endDate <= booking.StartDate || startDate >= booking.EndDate))
                .Select(info => new RoomInformationDTO
                {
                    RoomId = info.RoomId,
                    RoomNumber = info.RoomNumber,
                    RoomDetailDescription = info.RoomDetailDescription,
                    RoomMaxCapacity = info.RoomMaxCapacity,
                    RoomTypeId = info.RoomTypeId,
                    RoomStatus = info.RoomStatus,
                    RoomPricePerDay = info.RoomPricePerDay,
                    RoomTypeName = info.RoomType != null ? info.RoomType.RoomTypeName : null,
                    TypeDescription = info.RoomType != null ? info.RoomType.TypeDescription : null,
                    TypeNote = info.RoomType != null ? info.RoomType.TypeNote : null
                }).ToListAsync();
        }

        public async Task<ICollection<RoomInformationDTO>> SearchRoom(string keySearch)
        {
            return await _context.RoomInformations.Include(e => e.RoomType).Select(info => new RoomInformationDTO
            {
                RoomId = info.RoomId,
                RoomNumber = info.RoomNumber,
                RoomDetailDescription = info.RoomDetailDescription,
                RoomMaxCapacity = info.RoomMaxCapacity,
                RoomTypeId = info.RoomTypeId,
                RoomStatus = info.RoomStatus,
                RoomPricePerDay = info.RoomPricePerDay,
                RoomTypeName = info.RoomType != null ? info.RoomType.RoomTypeName : null,
                TypeDescription = info.RoomType != null ? info.RoomType.TypeDescription : null,
                TypeNote = info.RoomType != null ? info.RoomType.TypeNote : null
            }).Where(e=>e.RoomNumber.Contains(keySearch)|| e.RoomMaxCapacity.ToString() == keySearch || 
                        e.RoomTypeName.Contains(keySearch))
            .ToListAsync();
        }

        public async Task<ICollection<RoomInformation>> GetList()
        {
            return await _context.RoomInformations.ToListAsync();
        }
        public async Task<RoomInformation> GetById(int id)
        {
            return await _context.RoomInformations.FirstOrDefaultAsync(a => a.RoomId == id);
        }

        public async Task<bool>  Add(RoomInformation RoomInformation)
        {
            _context.RoomInformations.Add(RoomInformation);
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
        public async Task<bool>  Update(RoomInformation RoomInformation)
        {

            try
            {
                var existingRoom = await _context.RoomInformations.FirstOrDefaultAsync(e => e.RoomId == RoomInformation.RoomId);
                if (existingRoom != null)
                {
                    existingRoom.RoomMaxCapacity = RoomInformation.RoomMaxCapacity;
                    existingRoom.RoomNumber = RoomInformation.RoomNumber;
                    existingRoom.RoomDetailDescription = RoomInformation.RoomDetailDescription;
                    existingRoom.RoomPricePerDay = RoomInformation.RoomPricePerDay;
                    existingRoom.RoomTypeId = RoomInformation.RoomTypeId;
                    existingRoom.RoomStatus = RoomInformation.RoomStatus;

                    _context.RoomInformations.Update(existingRoom);
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


/*            _context.RoomInformations.Update(RoomInformation);
            return await _context.SaveChangesAsync() > 0 ? true : false;*/

        }
        public async Task<bool> Delete(int id)
        {
            var _exisitngRoomInformation = await _context.RoomInformations.FirstOrDefaultAsync(a => a.RoomId == id);

            if (_exisitngRoomInformation != null)
            {
                _context.RoomInformations.Remove(_exisitngRoomInformation);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }
    }
}
