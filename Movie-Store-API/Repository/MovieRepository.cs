using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_Store_API.ViewModels;
using Movie_Store_API.Repository.Interface;
using Movie_Store_Data.Data;
using Movie_Store_Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Movie_Store_API.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDBContext _context;

        public MovieRepository(MovieDBContext context)
        {
            _context = context;
        }

        public async Task AddMovieAsync(MovieRequest movieRequest)
        {
            Movie movie = new Movie()
            {
                ID = movieRequest.ID,
                Description = movieRequest.Description,
                Image = movieRequest.Image,
                IDProducer = movieRequest.IDProducer,
                Title = movieRequest.Title,
                ReleaseDate = movieRequest.ReleaseDate,
            };
            await _context.Movies.AddAsync(movie);
        }

        public void DeleteMovie(MovieRequest movieRequest)
        {
            throw new NotImplementedException();
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

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateMovie(MovieRequest movieRequest)
        {
            throw new NotImplementedException();
        }
    }
}
