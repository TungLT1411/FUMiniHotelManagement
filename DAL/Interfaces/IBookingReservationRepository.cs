using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IBookingReservationRepository
    {
        public Task<ICollection<BookingReservation>> GetList();
        Task<BookingReservation> GetById(int id);
        Task<bool> Delete(int id);
        Task<bool> Add(BookingReservation reservation);
        Task<bool> Update(BookingReservation reservation);
    }

}
