using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_Store_API.Models;
using Movie_Store_API.Services.Interface;
using Movie_Store_API.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Movie_Store_API.Extensions;

namespace Movie_Store_API.Services
{
    public class UserService : IUserSevice
    {
        private readonly MovieDBContext _context;
        private readonly IConfiguration _configuration;
        private string secretkey = string.Empty;

        public UserService(MovieDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

            secretkey = _configuration.GetValue<string>("Secretkey");
        }

        public async Task AddUserAsync(User user)
        {
            user.ID = Guid.NewGuid().ToString();
            await _context.Users.AddAsync(user);
        }

        public async Task<string> AuthencationUserAsync(string username, string password, string token)
        {
            var findUser = await _context.Users.SingleOrDefaultAsync(x => x.Username.Equals(username) && x.Password.Equals(password));

            if (findUser != null)
            {
                return new JwtHelpers(secretkey).CreateToken(findUser);
            }
            return string.Empty;
        }

        public async Task<User> GetUserByIDAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public void RemoveUser(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateUser(User user)
        {
            _context.Update(user);
        }
    }
}
