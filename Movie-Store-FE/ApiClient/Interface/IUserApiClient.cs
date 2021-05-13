using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_Store_FE.Models;
using Movie_Store_FE.ViewModels;

namespace Movie_Store_FE.ApiClient.Interface
{
    public interface IUserApiClient
    {
        Task<UserResponse> Authencate(User user);
    }
}
