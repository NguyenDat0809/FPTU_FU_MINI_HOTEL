using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using HotelManagement_BusinessObject.Models;
using HotelManagement_DAO;
using HotelManagement_Repositories.Interfaces;

namespace HotelManagement_Repositories.Implements
{
    public class RoleRepository : IRoleRepository
    {
        public async Task<bool> CreateRole(Role role)
        {
            return await GenericDAO<Role>.Instance.InsertAsync(role);
        }

        public bool DeleteRole(Role role)
        {
            return GenericDAO<Role>.Instance.Delete(role);
        }

        public async Task<Role?> GetRoleById(int id)
        {
            return await GenericDAO<Role>.Instance.SingleOrDefaultAsync(x => x.RoleId == id);
        }

        public async Task<Role?> GetRoleByName(string name)
        {
            return await GenericDAO<Role>.Instance.SingleOrDefaultAsync(x => x.RoleName == name);
        }

        public async Task<IEnumerable<Role>> GetRoleContainedName(string name)
        {
            return await GenericDAO<Role>.Instance.GetListAsync(x => x.RoleName.Contains(name));
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await GenericDAO<Role>.Instance.GetListAsync();
        }

        public bool UpdateRole(Role role)
        {
            return GenericDAO<Role>.Instance.Update(role);
        }
    }
}
