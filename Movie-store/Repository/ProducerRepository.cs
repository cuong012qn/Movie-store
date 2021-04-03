using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movie_store.Data;
using Movie_store.Models;
using Movie_store.Repository.Interface;

namespace Movie_store.Repository
{
    public class ProducerRepository : IProducerRepository
    {
        private readonly MovieDBContext _context;

        public ProducerRepository(MovieDBContext context)
        {
            _context = context;
        }

        public void Add(Producer producer)
        {
            _context.Producers.Add(producer);
        }

        public async Task<Producer> FindByID(int id)
        {
            return await _context.Producers.FindAsync(id);
        }

        public async Task<List<Producer>> GetAll()
        {
            return await _context.Producers.ToListAsync();
        }

        public void Remove(Producer producer)
        {
            _context.Producers.Remove(producer);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Producer producer)
        {
            var findProducer = await FindByID(id);
            if (findProducer != null)
            {
                _context.Producers.Update(producer);
            }
        }
    }
}
