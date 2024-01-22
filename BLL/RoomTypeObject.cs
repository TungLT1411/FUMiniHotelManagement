using DAL.Interfaces;
using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RoomTypeObject
    {
        private readonly IRoomTypeRepository roomTypeRepository;
        public RoomTypeObject()
        {
            roomTypeRepository = new RoomTypeRepository();
        }

        public async Task<ICollection<RoomType>> GetRoomTypeList()
        {
            var ListCustomer = await roomTypeRepository.GetList();
            return ListCustomer;
        }
    }
}
