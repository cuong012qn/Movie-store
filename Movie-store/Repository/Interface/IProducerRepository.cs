using Movie_store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_store.Repository.Interface
{
    interface IProducerRepository
    {
        Task<List<Producer>> GetAll();

        void Add(Producer producer);

        void Remove(Producer producer);

        Task<Producer> FindByID(int id);

        Task UpdateAsync(int id, Producer producer);

        Task SaveAsync();
    }
}
