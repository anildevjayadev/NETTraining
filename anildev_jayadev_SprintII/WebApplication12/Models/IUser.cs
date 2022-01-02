using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Repository
{
    public interface IUser
    {
        List<User> GetAllUsers();
        User GetUserById(int id);
        User GetUserByEmail(string id);
        User AddUser(User user);
        User UpdateUser(User user);
    }
}
