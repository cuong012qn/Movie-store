using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_Store_Data.Models;

namespace Movie_Store_API.Repository.Interface
{
    public interface IMovieDirectorRepository
    {
        Task<List<Movie>> GetMoviesAsync(int idDirector);

        Task<List<Director>> GetDirectorsAsync(int idMovie);

        Task<MovieDirector> FindByID(int idMovie, int idDirector);

        Task AddMovieDirector(int idMovie, int idDirector);

        Task RemoveDirector(int idMovie, int idDirector);

        Task SaveChangesAsync();
    }
}
