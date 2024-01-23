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
    public class RoomInformationObject
    {
        private readonly IRoomInformationRepository roomInformationRepository;
        public RoomInformationObject()
        {
            roomInformationRepository = new RoomInformationRepository();
        }

        public async Task<ICollection<RoomInformationDTO>> GetRoomListWithType()
        {
             var ListCustomer = await roomInformationRepository.GetListWithType();

             return ListCustomer;
        }
        public async Task<ICollection<RoomInformationDTO>> GetRoomListAvailable(DateTime startDate, DateTime endDate, int roomTypeId)
        {
            var ListCustomer = await roomInformationRepository.GetListRoomAvailabel(startDate,endDate,roomTypeId);

            return ListCustomer;
        }
        public async Task<RoomInformation> GetRoomById(int id)
        {
            var ListCustomer = await roomInformationRepository.GetById(id);
            return ListCustomer;
        }

        public async Task<ICollection<RoomInformation>> GetRoomList()
        {
            var ListRoom = await roomInformationRepository.GetList();
            return ListRoom;
        }
        public async Task<bool> AddRoom(RoomInformation room)
        {
            var ListRoom = await roomInformationRepository.Add(room);
            return ListRoom;
        }
        public async Task<bool> UpdateRoom(RoomInformation room)
        {
            var ListRoom = await roomInformationRepository.Update(room);
            return ListRoom;
        }
        public async Task<bool> RemoveRoom(int id)
        {
            var ListRoom = await roomInformationRepository.Delete(id);
            return ListRoom;
        }

        public async Task<ICollection<RoomInformationDTO>> SearchRoom(string keySerach)
        {
            var ListRoom = await roomInformationRepository.SearchRoom(keySerach);
            return ListRoom;
        }
    }
}

