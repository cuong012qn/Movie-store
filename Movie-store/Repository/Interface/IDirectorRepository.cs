using Movie_store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_store.Repository.Interface
{
    interface IDirectorRepository
    {
        Task<List<Director>> GetAll();

        void Add(Director director);

        void Remove(Director director);

        Task<Director> FindByID(int id);

        Task UpdateAsync(int id, Director director);

        Task SaveAsync();
    }
}
