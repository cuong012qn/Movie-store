using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_Store_API.Models;

namespace Movie_Store_API.Services.Interface
{
    public interface IUserSevice
    {
        Task<string> AuthencationUserAsync(string username, string password, string token);

        Task<User> GetUserByIDAsync(int id);

        Task AddUserAsync(User user);

        void RemoveUser(User user);

        void UpdateUser(User user);

        Task SaveChangesAsync();
    }
}
