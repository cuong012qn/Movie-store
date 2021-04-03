using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_store.Models;

namespace Movie_store.Repository.Interface
{
   public interface IMovieRepository
    {
        Task<List<Movie>> GetAll();

        void Add(Movie movie);

        void Remove(Movie movie);

        Task<Movie> FindByID(int id);

        Task UpdateAsync(int id, Movie movie);

        Task SaveAsync();
    }
}
