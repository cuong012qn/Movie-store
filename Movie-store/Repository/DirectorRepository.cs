using System;
using System.Collections.Generic;
using System.Linq;
using Movie_store.Repository.Interface;
using System.Threading.Tasks;
using Movie_store.Models;
using Movie_store.Data;
using Microsoft.EntityFrameworkCore;

namespace Movie_store.Repository
{
    public class DirectorRepository : IDirectorRepository
    {
        private readonly MovieDBContext _context;

        public DirectorRepository(MovieDBContext context)
        {
            _context = context;
        }

        public void Add(Director director)
        {
            _context.Directors.Add(director);
        }

        public async Task<Director> FindByID(int id)
        {
            return await _context.Directors.FindAsync(id);
        }

        public async Task<List<Director>> GetAll()
        {
            return await _context.Directors.ToListAsync();
        }

        public void Remove(Director director)
        {
            _context.Directors.Remove(director);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Director director)
        {
            var FindDirector = await FindByID(id);
            if (FindDirector != null)
            {
                _context.Directors.Update(director);
            }
        }
    }
}
