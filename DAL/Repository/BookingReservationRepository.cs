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
    public class BookingReservationRepository : IBookingReservationRepository
    {
        private readonly FUMiniHotelManagementContext _context;
        public BookingReservationRepository(FUMiniHotelManagementContext context)
        {
            _context = context;
        }

        async Task<ICollection<BookingReservation>> IBookingReservationRepository.GetList()
        {
            return await _context.BookingReservations.ToListAsync();
        }
        async Task<BookingReservation> IBookingReservationRepository.GetById(int id)
        {
            return await _context.BookingReservations.FirstOrDefaultAsync(a => a.BookingReservationId == id);
        }
        async Task<bool> IBookingReservationRepository.Add(BookingReservation BookingReservation)
        {
            _context.BookingReservations.Add(BookingReservation);
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
        async Task<bool> IBookingReservationRepository.Update(BookingReservation BookingReservation)
        {
            _context.BookingReservations.Update(BookingReservation);
            return await _context.SaveChangesAsync() > 0 ? true : false;

        }
        async Task<bool> IBookingReservationRepository.Delete(int id)
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
