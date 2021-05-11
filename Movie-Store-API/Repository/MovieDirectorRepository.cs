using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_Store_API.Repository.Interface;
using Movie_Store_Data.Models;
using Movie_Store_Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Movie_Store_API.Repository
{
    public class MovieDirectorRepository : IMovieDirectorRepository
    {
        private readonly MovieDBContext _context;

        public MovieDirectorRepository(MovieDBContext movieDBContext)
        {
            _context = movieDBContext;
        }

        public async Task AddMovieDirector(int idMovie, int idDirector)
        {
            var find = await FindByID(idMovie, idDirector);
            if (find == null)
                _context.MovieDirectors.Add(new MovieDirector
                {
                    IDDirector = idDirector,
                    IDMovie = idMovie
                });
        }

        public async Task<MovieDirector> FindByID(int idMovie, int idDirector)
        {
            return await _context.MovieDirectors
                .SingleOrDefaultAsync(x => x.IDDirector.Equals(idDirector) && x.IDMovie.Equals(idMovie));
        }

        public async Task<List<Director>> GetDirectorsAsync(int idMovie)
        {
            return await _context.MovieDirectors
                .Where(x => x.IDMovie.Equals(idMovie))
                .Select(x => x.Director)
                .ToListAsync();
        }

        public async Task<List<Movie>> GetMoviesAsync(int idDirector)
        {
            return await _context.MovieDirectors
                .Where(x => x.IDDirector.Equals(idDirector))
                .Select(x => x.Movie)
                .ToListAsync();
        }

        public async Task RemoveDirector(int idMovie, int idDirector)
        {
            var find = await FindByID(idMovie, idDirector);

            if (find != null)
                _context.MovieDirectors.Remove(find);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
