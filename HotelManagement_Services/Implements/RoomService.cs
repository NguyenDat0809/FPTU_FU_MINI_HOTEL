using HotelManagement_BusinessObject.Models;
using HotelManagement_Repositories.Implements;
using HotelManagement_Repositories.Interfaces;
using HotelManagement_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement_Services.Implements
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        public RoomService()
        {
            _roomRepository = new RoomRepository();
        }
        public async Task<bool> CreateRoom(Room room)
        {
            var accountUpdate = await GetRoomById(room.RoomId);
            return await _roomRepository.CreateRoom(room); 
        }

        public async Task<bool> DeleteRoom(Room room)
        { 

            var accountDelete = await GetRoomById(room.RoomId);
            return _roomRepository.DeleteRoom(accountDelete);
        }

        public async Task<IEnumerable<Room>> GetRooms()
        {
            return await _roomRepository.GetRooms();
        }

        public async Task<Room?> GetRoomById(string id)
        {
            return await _roomRepository.GetRoomById(id);
        }

        public async Task<bool> UpdateRoom(Room role)
        {
            var roomUpdate = await GetRoomById(role.RoomId);
            roomUpdate.RoomName = role.RoomName;
            roomUpdate.RoomTypeId = role.RoomTypeId;
            roomUpdate.Capacity = role.Capacity;
            roomUpdate.Description = role.Description;
            roomUpdate.ImageUrl = role.ImageUrl;
            return _roomRepository.UpdateRoom(roomUpdate);
        }

        public async Task<IEnumerable<Room>> GetRoomsByName(string name)
        {
            return await _roomRepository.GetRoomByName(name);
        }
    }
}
