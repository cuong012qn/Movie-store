using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_Store_API.ViewModels;
using Movie_Store_API.Repository.Interface;
using Movie_Store_Data.Data;
using Movie_Store_Data.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Movie_Store_API.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDBContext _context;

        public MovieRepository(MovieDBContext context)
        {
            _context = context;
        }

        public async Task<MovieResponse> AddMovieAsync(MovieRequest movieRequest)
        {
            Movie movie = new Movie()
            {
                ID = movieRequest.ID,
                Description = movieRequest.Description,
                IDProducer = movieRequest.IDProducer,
                Image = movieRequest.UploadImage.FileName,
                Title = movieRequest.Title,
                ReleaseDate = movieRequest.ReleaseDate,
            };

            await _context.Movies.AddAsync(movie);
            await SaveChangesAsync();

            var findDirector = await _context.MovieDirectors
                .Where(x => x.IDMovie.Equals(movie.ID))
                .Select(director => director.Director).ToListAsync();

            return new MovieResponse
            {
                ID = movie.ID,
                Description = movie.Description,
                Title = movie.Title,
                Directors = findDirector,
                Producer = movie.Producer,
                ReleaseDate = movie.ReleaseDate,
                ImagePath = Path.Combine("Static", movie.Image)
            };
        }

        public async Task DeleteMovie(int idMovie)
        {
            //Delete movie
            var findMovie = await _context.Movies.SingleOrDefaultAsync(x => x.ID.Equals(idMovie));

            if (findMovie != null)
            {
                //Delete many to many (MovieDirectors)
                var findDirector = _context.MovieDirectors.Where(x => x.IDMovie.Equals(idMovie));

                _context.MovieDirectors.RemoveRange(findDirector);

                _context.Movies.Remove(findMovie);
            }
        }

        public Task<MovieResponse> GetMovieByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MovieResponse>> GetMoviesAsync()
        {
            return await _context.Movies
                .Include(x => x.MovieDirectors)
                .Select(x => new MovieResponse
                {
                    ID = x.ID,
                    Description = x.Description,
                    Title = x.Title,
                    ImagePath = x.Image,
                    ReleaseDate = x.ReleaseDate,
                    Producer = x.Producer,
                    Directors = x.MovieDirectors
                    .Where(md => md.IDMovie.Equals(x.ID))
                    .Select(director => director.Director).ToList()
                }).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateMovie(MovieRequest movieRequest)
        {
            throw new NotImplementedException();
        }
    }
}
