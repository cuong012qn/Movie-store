using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_Store_Data.Models;
using Movie_Store_API.ViewModels;

namespace Movie_Store_API.Repository.Interface
{
    public interface IMovieRepository
    {
        Task<List<MovieResponse>> GetMoviesAsync();

        Task<MovieResponse> GetMovieByIDAsync(int id);

        void UpdateMovie(MovieRequest movieRequest);

        void DeleteMovie(MovieRequest movieRequest);

        Task AddMovieAsync(MovieRequest movieRequest);

        Task SaveChangesAsync();
    }
}
