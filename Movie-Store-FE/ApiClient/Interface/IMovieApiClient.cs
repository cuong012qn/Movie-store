using Movie_Store_FE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Store_FE.ApiClient.Interface
{
    public interface IMovieApiClient
    {
        Task<MovieResponse> GetMovies();

        Task<MovieResponse> GetMovie(int id);

        Task AddMovie(MovieRequest movie);
    }
}
