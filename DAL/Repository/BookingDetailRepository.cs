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
    public class BookingDetailRepository : IBookingDetailRepository
    {
        private readonly FUMiniHotelManagementContext _context;
        public BookingDetailRepository(FUMiniHotelManagementContext context)
        {
            _context = context;
        }

        async Task<ICollection<BookingDetail>> IBookingDetailRepository.GetList()
        {
            return await _context.BookingDetails.ToListAsync();
        }
        async Task<BookingDetail> IBookingDetailRepository.GetById(int id)
        {
            return await _context.BookingDetails.FirstOrDefaultAsync(a => a.RoomId == id);
        }
        async Task<bool> IBookingDetailRepository.Add(BookingDetail BookingDetail)
        {
            _context.BookingDetails.Add(BookingDetail);
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
        async Task<bool> IBookingDetailRepository.Update(BookingDetail BookingDetail)
        {
            _context.BookingDetails.Update(BookingDetail);
            return await _context.SaveChangesAsync() > 0 ? true : false;

        }
        async Task<bool> IBookingDetailRepository.Delete(int id)
        {
            var _exisitngBookingDetail = await _context.BookingDetails.FirstOrDefaultAsync(a => a.RoomId == id);

            if (_exisitngBookingDetail != null)
            {
                _context.BookingDetails.Remove(_exisitngBookingDetail);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }
    }
}
