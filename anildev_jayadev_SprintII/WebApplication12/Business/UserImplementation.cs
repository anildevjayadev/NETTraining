using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Repository.Models;

namespace Business
{
    public class UserImplementation:IUser
    {
        private readonly AppDbContext _context;
        public UserImplementation(AppDbContext context)
        {
            _context = context;
        }
        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.UserID == id);
        }
        public User GetUserByEmail(string useremail)
        {
            return _context.Users.FirstOrDefault(u => u.UserEmail == useremail);
        }

        public User AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }       
        public User UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return user;
        }
    }
}
