using HotelManagement_BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement_Repositories.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetRooms();
        Task<IEnumerable<Room>> GetRoomByName(string name);

        Task<Room?> GetRoomById(string id);
        Task<bool> CreateRoom(Room room);
        bool UpdateRoom(Room room);
        bool DeleteRoom(Room room);
    }
}
