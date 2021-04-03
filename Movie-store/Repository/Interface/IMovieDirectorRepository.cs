using Movie_store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_store.Repository.Interface
{
    public interface IMovieDirectorRepository
    {
        Task<List<MovieDirector>> GetAll();

        void Add(int IDMovie, int IDDirector);

        void Remove(int IDMovie, int IDDirector);

        Task<MovieDirector> FindByID(int id);

        Task UpdateAsync(int id, MovieDirector movieDirector);

        Task SaveAsync();
    }
}
