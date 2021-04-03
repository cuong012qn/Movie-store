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
            return await _context.Directors
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ID.Equals(id));
        }

        public async Task<IEnumerable<Director>> GetAll()
        {
            return await _context.Directors.ToListAsync();
        }

        public async Task<List<Director>> GetDirectorsDistinct(int IDMovie)
        {
            var director = await GetAll();
            var moviedirectors = await _context.MovieDirectors
                .Where(x => x.IDMovie.Equals(IDMovie))
                .Select(x => x.Director).ToListAsync();

            return director.Except(moviedirectors).ToList();

        }

        public async Task<List<Movie>> GetMovies(int IDDirector)
        {
            return await _context.MovieDirectors
                .Include(x => x.Movie)
                .ThenInclude(x => x.Producer)
                .Where(x => x.IDDirector.Equals(IDDirector))
                .Select(x => x.Movie)
                .ToListAsync();
        }

        public void Remove(Director director)
        {
            var findMovieDirector = _context.MovieDirectors.Where(x => x.IDDirector.Equals(director.ID));

            _context.MovieDirectors.RemoveRange(findMovieDirector);

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
