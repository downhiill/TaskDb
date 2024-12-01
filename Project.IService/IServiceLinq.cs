using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.IService
{
    public interface IServiceLinq
    {
        List<UserModel> SearchUsersByNamePattern(string namePattern);
        void Delete(int id);
        void EditWages(int userId, decimal wages);
        List<UserModel> GetAllUsersOrderBy();
        List<UserModel> GetAllUsersOrderByDescending();
        List<UserModel> GetAllUsersThenBy();
        void UserChangeRole(int userId, List<EnumTypeRoleModel> roles);
        List<ShortUserProfessionRole> GetUserProfessionRole(string nameProfession, EnumTypeRoleModel role);
        List<IGrouping<int, UserModel>> GetAllUsersGroupedByAge();
        List<UserModel> GetActiveAndOldUsers();
        List<UserModel> GetActiveAndOldUsersIntersect();
        bool AnyActiveUserOver30();
        bool AllUsersOver18();
        int CountUsersOver18();
        int GetMinAge();
        int GetMaxAge();
        double GetAverageAge();
        decimal GetTotalWages();
        decimal GetTotalWagesNoTracking();
        decimal GetTotalWagesNoTrackingUsingTrackingBehavior();
        void TrackChangesForEntity(int userId);
        IEnumerable<UserModel> GetAllUsersIEnumerable();
        IQueryable<UserModel> GetAllUsersIQueryable();
    }
}
