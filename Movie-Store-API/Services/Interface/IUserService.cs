using Movie_Store_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Store_API.Services.Interface
{
    public interface IUserService
    {
        Task<string> AuthencationUserAsync(string username, string password, string token);

        Task<User> GetUserByIDAsync(string id);

        Task<User> Login(string username, string password);

        Task AddUserAsync(User user);

        void RemoveUser(User user);

        void UpdateUser(User user);

        Task SaveChangesAsync();
    }
}
