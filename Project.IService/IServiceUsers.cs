using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.IService
{
    public interface IServiceUsers
    {
        int Add(UserModel user);
        void EditName(int userId, string name);
        void EditAge(int userId, int age);
        void Delete(int userId);
        List<UserModel> GetAllUsers();
        List<UserModel> SearchUsersMoreAge(int age);
        List<UserModel> SearchUsers(string term);
    }
}
