using Movie_store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_store.Repository.Interface
{
    interface IMovieDirectorRepository
    {
        Task<List<MovieDirector>> GetAll();

        void Add(MovieDirector movieDirector);

        void Remove(MovieDirector movieDirector);

        Task<MovieDirector> FindByID(int id);

        Task UpdateAsync(int id, MovieDirector movieDirector);

        Task SaveAsync();
    }
}
