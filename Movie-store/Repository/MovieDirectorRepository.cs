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
    public class MovieDirectorRepository : IMovieDirectorRepository
    {
        private readonly MovieDBContext _context;
        public MovieDirectorRepository(MovieDBContext context)
        {
            _context = context;
        }

        public void Add(MovieDirector movieDirector)
        {
            _context.MovieDirectors.Add(movieDirector);
        }

        public async Task<MovieDirector> FindByID(int id)
        {
            return null;
        }

        public Task<List<MovieDirector>> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(MovieDirector movieDirector)
        {
            _context.MovieDirectors.Remove(movieDirector);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(int id, MovieDirector movieDirector)
        {
            throw new NotImplementedException();
        }
    }
}
