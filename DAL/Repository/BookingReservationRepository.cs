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
    public class BookingReservationRepository : IBookingReservationRepository
    {
        private readonly FUMiniHotelManagementContext _context;
        public BookingReservationRepository()
        {
            _context = new FUMiniHotelManagementContext();
        }

        public async Task<ICollection<BookingReservationDTO>> GetList()
        {
            return await _context.BookingReservations.Include(e=>e.Customer).Include(p=>p.BookingDetails).Select(order => new BookingReservationDTO
            {
                BookingReservationId = order.BookingReservationId,
                BookingDate = order.BookingDate,
                BookingStatus = order.BookingStatus,
                CustomerName = order.Customer.CustomerFullName,
                Telephone  = order.Customer.Telephone,
                TotalPrice = order.TotalPrice
            }).ToArrayAsync();
        }
        public async Task<BookingReservation> GetById(int id)
        {
            return await _context.BookingReservations.FirstOrDefaultAsync(a => a.BookingReservationId == id);
        }
        public async Task<bool> Add(BookingReservation BookingReservation)
        {
            _context.BookingReservations.Add(BookingReservation);
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
        public async Task<bool> Update(BookingReservation BookingReservation)
        {
            _context.BookingReservations.Update(BookingReservation);
            return await _context.SaveChangesAsync() > 0 ? true : false;

        }
        public async Task<bool> Delete(int id)
        {
            var _exisitngBookingReservation = await _context.BookingReservations.FirstOrDefaultAsync(a => a.BookingReservationId == id);

            if (_exisitngBookingReservation != null)
            {
                _context.BookingReservations.Remove(_exisitngBookingReservation);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }
    }
}
