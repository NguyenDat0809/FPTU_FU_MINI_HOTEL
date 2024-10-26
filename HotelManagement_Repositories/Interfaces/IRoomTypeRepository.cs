using HotelManagement_BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement_Repositories.Interfaces
{
    public interface IRoomTypeRepository
    {
        Task<IEnumerable<RoomType>> GetRoomTypes();
        Task<RoomType?> GetRoomTypeById(string id);
        Task<IEnumerable<RoomType>> GetRoomTypeContainedName(string name);
        Task<RoomType?> GetRoomTypeByName(string name);
        Task<bool> CreateRoomType(RoomType roomType);
        bool UpdateRoomType(RoomType roomType);
        bool DeleteRoomType(RoomType roomType);
    }
}
