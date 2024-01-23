using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ModelsDTOs
{
    public class BookingReservationDTO
    {
        public int BookingReservationId { get; set; }
        public string? CustomerName { get; set; }
        public DateTime? BookingDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? Telephone { get; set; }
        public byte? BookingStatus { get; set; }
    }
}
