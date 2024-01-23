using DAL.Interfaces;
using DAL.Models;
using DAL.ModelsDTOs;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BookingReservationObject
    {
        private readonly IBookingReservationRepository bookingReservationRepository;
        public BookingReservationObject()
        {
            bookingReservationRepository = new BookingReservationRepository();
        }

        public async Task<ICollection<BookingReservationDTO>> GetOrderList()
        {
            var ListOrder = await bookingReservationRepository.GetList();
            return ListOrder;
        }
    }
}
