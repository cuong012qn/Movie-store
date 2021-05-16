using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_Store_FE.ViewModels;

namespace Movie_Store_FE.ApiClient.Interface
{
    interface IDirectorApiClient
    {
        Task<DirectorResponse> GetDirectors();

        Task<DirectorResponse> GetDirector(int id);

        Task AddDirector(DirectorRequest director);
    }
}
