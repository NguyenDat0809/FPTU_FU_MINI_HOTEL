using HotelManagement_BusinessObject.Models;
using HotelManagement_DAO;
using HotelManagement_Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HotelManagement_Repositories.Implements
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        public async Task<bool> CreateRoomType(RoomType roomType)
        {
            return await GenericDAO<RoomType>.Instance.InsertAsync(roomType);
        }

        public bool DeleteRoomType(RoomType roomType)
        {
            return  GenericDAO<RoomType>.Instance.Delete(roomType);

        }

        public async Task<RoomType?> GetRoomTypeById(string id)
        {
            return await GenericDAO<RoomType>.Instance.SingleOrDefaultAsync(r => r.RoomTypeId == id);

        }

        public async Task<RoomType?> GetRoomTypeByName(string name)
        {
            return await GenericDAO<RoomType>.Instance.SingleOrDefaultAsync(r => r.RoomTypeName == name);

        }

        public async Task<IEnumerable<RoomType>> GetRoomTypeContainedName(string name)
        {
            return await GenericDAO<RoomType>.Instance.GetListAsync(r => r.RoomTypeName.Contains(name));
        }

        public async Task<IEnumerable<RoomType>> GetRoomTypes()
        {
            return await GenericDAO<RoomType>.Instance.GetListAsync();
        }

        public bool UpdateRoomType(RoomType roomType)
        {
            return GenericDAO<RoomType>.Instance.Update(roomType);
        }
    }
}
