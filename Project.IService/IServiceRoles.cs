using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.IService
{
    public interface IServiceRoles
    {
        void UserAddRole(int userId,EnumTypeRoleModel role);
        void UserChangeRole(int userId, List<EnumTypeRoleModel> roles);
        void UserRemoveRole(int userId, EnumTypeRoleModel role);
        List<ShortUserRole> GetUsers(EnumTypeRoleModel role);
        List<RoleModel> GetRolesUser(int userId);
        List<ShortUserRoles> GetAllUsers();
    }
}
