using DAL.Models;
using DAL.ModelsDTOs;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRoomInformationRepository
    {
        Task<ICollection<RoomInformationDTO>> GetListWithType();
        Task<ICollection<RoomInformation>> GetList();
        Task<ICollection<RoomInformationDTO>> GetListRoomAvailabel(DateTime startDate, DateTime endDate, int Roomtype);
        Task<ICollection<RoomInformationDTO>> SearchRoom(string keySearch);
        Task<RoomInformation> GetById(int id);
        Task<bool> Delete(int id);
        Task<bool> Add(RoomInformation roomInfo);
        Task<bool> Update(RoomInformation roomInfo);
    }
}
