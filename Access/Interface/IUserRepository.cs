using Bussiness.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access.Interface
{
    public interface IUserRepository
    {
        Task<Users> GetUser(string username, string password);
        Task<Users> AddUser(string username, string password);
    }
}
