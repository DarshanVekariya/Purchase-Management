using Access.Interface;
using Bussiness.db;
using Bussiness.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _db;

        public UserRepository(MyDbContext db)
        {
            _db = db;
        }

        public async Task<Users> GetUser(string username, string password)
        {
            return await _db.Users.SingleOrDefaultAsync(u => u.Username == username && u.Password == password);

        }
        public async Task<Users> AddUser(string username, string password)
        {
            var user = new Users();
            user.Username = username;
            user.Password = password;
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return user;
        }
    }
}
