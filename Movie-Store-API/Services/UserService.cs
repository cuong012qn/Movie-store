using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Movie_Store_API.Extensions;
using Movie_Store_API.Services.Interface;
using Movie_Store_Data.Data;
using Movie_Store_Data.Models;
using System;
using System.Threading.Tasks;

namespace Movie_Store_API.Services
{
    public class UserService : IUserService
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
            var findUser = await _context.Users.SingleOrDefaultAsync(x => x.Email.Equals(user.Email) || user.Username.Equals(user.Username));
            if (findUser == null)
            {
                user.ID = Guid.NewGuid().ToString();
                await _context.Users.AddAsync(user);
            }
        }

        public async Task<string> AuthencationUserAsync(string username, string password, string token)
        {
            var findUser = await _context.Users.SingleOrDefaultAsync(x => x.Username.Equals(username) && x.Password.Equals(password));

            if (findUser != null)
                return JwtHelpers.GetInstance(secretkey).CreateToken(findUser, 300);
            return string.Empty;
        }

        public async Task<User> GetUserByIDAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> Login(string username, string password)
        {
            return await _context.Users
                .SingleOrDefaultAsync(
                x => x.Username.Equals(username) && x.Password.Equals(password));
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
