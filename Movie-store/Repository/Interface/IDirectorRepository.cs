using Movie_store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_store.Repository.Interface
{
    public interface IDirectorRepository
    {
        Task<IEnumerable<Director>> GetAll();

        Task<List<Movie>> GetMovies(int IDDirector);

        Task<List<Director>> GetDirectorsDistinct(int IDMovie);

        void Add(Director director);

        void Remove(Director director);

        Task<Director> FindByID(int id);

        Task UpdateAsync(int id, Director director);

        Task SaveAsync();
    }
}
