using HotelManagement_BusinessObject.Models;
using HotelManagement_Repositories.Implements;
using HotelManagement_Repositories.Interfaces;
using HotelManagement_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement_Services.Implements
{
    public class RoleService : IRoleService
    {
        public IRoleRepository _roleRepository { get; set; }
        public RoleService()
        {
            _roleRepository = new RoleRepository();
        }
        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _roleRepository.GetRoles();
        }

        public async Task<Role?> GetRoleById(int id)
        {
            return await _roleRepository.GetRoleById(id);
        }

        public async Task<bool> CreateRole(Role role)
        {
            return await _roleRepository.CreateRole(role);
        }

        public async Task<bool> UpdateRole(Role role)
        {
            var roleUpdate = await GetRoleById(role.RoleId);
            roleUpdate.RoleName = role.RoleName;
            return _roleRepository.UpdateRole(role);
        }

        public async Task<bool> DeleteRole(Role role)
        {
            return _roleRepository.DeleteRole(role);
        }

        public async Task<IEnumerable<Role>> GetRoleContainedName(string name)
        {
            return await _roleRepository.GetRoleContainedName(name);
        }

        public async Task<Role?> GetRoleByName(string name)
        {
            return await _roleRepository.GetRoleByName(name);
        }
    }
}
