using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_store.Models;
using Movie_store.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Movie_store.Data;

namespace Movie_store.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDBContext _context;

        public MovieRepository(MovieDBContext context)
        {
            _context = context;
        }

        public void Add(Movie movie)
        {
            _context.Movies.Add(movie);
        }

        public async Task<Movie> FindByID(int id)
        {
            return await _context.Movies
                .Include(x => x.MovieDirectors)
                .Include(x => x.Producer)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ID.Equals(id));
        }

        public async Task<List<Movie>> GetAll()
        {
            return await _context.Movies
                .Include(x => x.Producer)
                .Include(x => x.MovieDirectors)
                .ToListAsync();
        }

        public async Task<List<Director>> GetDirectors(int idMovie)
        {
            return await _context.MovieDirectors
                .Where(x => x.IDMovie.Equals(idMovie))
                .Select(x => x.Director).ToListAsync();
        }

        public void Remove(Movie movie)
        {
            var findMovieDirector = _context.MovieDirectors.Where(x => x.IDMovie.Equals(movie.ID));
            _context.MovieDirectors.RemoveRange(findMovieDirector);

            _context.Movies.Remove(movie);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Movie movie)
        {
            Movie findMovie = await FindByID(id);
            //Found movie
            if (findMovie != null)
            {
                _context.Movies.Update(movie);
            }
        }
    }
}
