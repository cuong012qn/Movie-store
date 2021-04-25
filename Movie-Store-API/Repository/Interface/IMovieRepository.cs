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

        Task<MovieResponse> UpdateMovieAsync(MovieRequest movieRequest);

        Task DeleteMovie(int idMovie);

        Task<MovieResponse> AddMovieAsync(MovieRequest movieRequest);

        Task SaveChangesAsync();
    }
}
