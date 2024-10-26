using HotelManagement_BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement_Services.Interfaces
{
    public interface IRoomTypeService
    {
        Task<IEnumerable<RoomType>> GetRoomTypes();
        Task<RoomType?> GetRoomTypeById(string id);
        Task<RoomType?> GetRoomTypeByname(string name);

        Task<bool> CreateRoomType(RoomType room);
        Task<bool> UpdateRoomType(RoomType room);
        Task<bool> DeleteRoomType(RoomType room);
    }
}
