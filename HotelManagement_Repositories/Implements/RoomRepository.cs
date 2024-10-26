using HotelManagement_BusinessObject.Models;
using HotelManagement_DAO;
using HotelManagement_Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement_Repositories.Implements
{
    public class RoomRepository : IRoomRepository
    {
        public async Task<bool> CreateRoom(Room room)
        {
            return await GenericDAO<Room>.Instance.InsertAsync(room);
        }

        public bool DeleteRoom(Room room)
        {
            return GenericDAO<Room>.Instance.Delete(room);
        }

        public async Task<Room?> GetRoomById(string id)
        {
            return await GenericDAO<Room>.Instance.SingleOrDefaultAsync(x => x.RoomId == id);
        }

        public async Task<IEnumerable<Room>> GetRoomByName(string name)
        {
            return await GenericDAO<Room>.Instance.GetListAsync(x => x.RoomName.Contains(name));
        }

        public async Task<IEnumerable<Room>> GetRooms()
        {
            return await GenericDAO<Room>.Instance.GetListAsync(
                include: r => r.Include(r => r.RoomType)
                );

        }

        public bool UpdateRoom(Room room)
        {
            return GenericDAO<Room>.Instance.Update(room);

        }
    }
}
