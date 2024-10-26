using HotelManagement_BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement_Services.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetRooms();
        Task<Room?> GetRoomById(string id);
        Task<IEnumerable<Room>> GetRoomsByName(string name);
        Task<bool> CreateRoom(Room room);
        Task<bool> UpdateRoom(Room room);
        Task<bool> DeleteRoom(Room room);
    }
}
