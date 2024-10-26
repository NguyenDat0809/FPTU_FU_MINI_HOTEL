using HotelManagement_BusinessObject.Models;
using HotelManagement_Repositories.Implements;
using HotelManagement_Repositories.Interfaces;
using HotelManagement_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement_Services.Implements
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        public RoomTypeService()
        {
            _roomTypeRepository = new RoomTypeRepository();
        }
        public async Task<bool> CreateRoomType(RoomType roomType)
        {
            
            return await _roomTypeRepository.CreateRoomType(roomType);
        }

        public async Task<bool> DeleteRoomType(RoomType roomType)
        {
            var roomTypeDelete = await GetRoomTypeById(roomType.RoomTypeId);
            return _roomTypeRepository.DeleteRoomType(roomTypeDelete);
        }

        public async Task<RoomType?> GetRoomTypeById(string id)
        {
            return await _roomTypeRepository.GetRoomTypeById(id);
        }

        public async Task<RoomType?> GetRoomTypeByname(string name)
        {
            return await _roomTypeRepository.GetRoomTypeByName(name);
        }

        public async Task<IEnumerable<RoomType>> GetRoomTypes()
        {
            return await _roomTypeRepository.GetRoomTypes();
        }

        public async Task<bool> UpdateRoomType(RoomType roomType)
        {
            var roomTypeUpdate = await _roomTypeRepository.GetRoomTypeById(roomType.RoomTypeId);
            return  _roomTypeRepository.UpdateRoomType(roomType);
        }
    }
}
