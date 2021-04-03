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

        public void Add(int IDMovie, int IDDirector)
        {
            _context.MovieDirectors.Add(new MovieDirector()
            {
                IDMovie = IDMovie,
                IDDirector = IDDirector
            });
        }

        public async Task<MovieDirector> FindByID(int id)
        {
            return null;
        }

        public async Task<List<MovieDirector>> GetAll()
        {
            return await _context.MovieDirectors
                .Include(x => x.Movie)
                .Include(x => x.Director)
                .ToListAsync();
        }

        public void Remove(int IDMovie, int IDDirector)
        {
            var findMovieDir = _context
                .MovieDirectors
                .SingleOrDefault(x => x.IDMovie.Equals(IDMovie) && x.IDDirector.Equals(IDDirector));

            if (findMovieDir != null) _context.MovieDirectors.Remove(findMovieDir);
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
