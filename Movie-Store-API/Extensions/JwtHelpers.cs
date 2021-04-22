using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_Store_API.Models;

namespace Movie_Store_API.Extensions
{
    public class JwtHelpers
    {
        private readonly string _secretkey = string.Empty;

        public JwtHelpers(string secretkey)
        {
            _secretkey = secretkey;
        }

        public string CreateToken(User user, double exp = 20)
        {
            //check null secretkey


            return string.Empty;
        }

        public bool IsValidToken(string token, string user)
        {
            //Check null secretkey

            return true;
        }
    }
}
