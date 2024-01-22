using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IBookingDetailRepository 
    {
        Task<ICollection<BookingDetail>> GetList();
        Task<BookingDetail> GetById(int id);
        Task<bool> Delete(int id);
        Task<bool> Add(BookingDetail bookingDetail);
        Task<bool> Update(BookingDetail bookingDetail);
    }
}
