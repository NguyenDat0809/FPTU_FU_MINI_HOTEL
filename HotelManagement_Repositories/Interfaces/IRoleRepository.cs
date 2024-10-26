using HotelManagement_BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement_Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetRoles();
        Task<Role?> GetRoleByName(string name);
        Task<IEnumerable<Role>> GetRoleContainedName(string name);
        Task<Role?> GetRoleById(int id);
        Task<bool> CreateRole(Role role);
        bool UpdateRole(Role role);
        bool DeleteRole(Role role);
    }
}
