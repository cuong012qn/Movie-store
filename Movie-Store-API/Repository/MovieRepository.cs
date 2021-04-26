﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_Store_API.ViewModels;
using Movie_Store_API.Repository.Interface;
using Movie_Store_Data.Data;
using Movie_Store_Data.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Movie_Store_API.Extensions;
using Microsoft.AspNetCore.Hosting;

namespace Movie_Store_API.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDBContext _context;
        private readonly IWebHostEnvironment _env;

        public MovieRepository(MovieDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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

        public async Task<MovieResponse> GetMovieByIDAsync(int id)
        {
            var findMovie = await _context.Movies
                .Include(x => x.Producer)
                .SingleOrDefaultAsync(x => x.ID.Equals(id));

            if (findMovie != null)
            {
                var findDirectors = await _context.MovieDirectors
                    .Where(x => x.IDMovie.Equals(findMovie.ID))
                    .Select(x => x.Director).ToListAsync();

                return new MovieResponse
                {
                    ID = findMovie.ID,
                    Description = findMovie.Description,
                    Producer = findMovie.Producer,
                    ReleaseDate = findMovie.ReleaseDate,
                    Title = findMovie.Title,
                    ImagePath = Path.Combine("Static", findMovie.Image),
                    Directors = findDirectors
                };
            }
            return null;
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

        public async Task<MovieResponse> UpdateMovieAsync(MovieRequest movieRequest)
        {
            var findMovie = await _context.Movies
                .Include(x=> x.Producer)
                .SingleOrDefaultAsync(x => x.ID.Equals(movieRequest.ID));

            if (findMovie != null)
            {
                //Check equals name
                if (!movieRequest.UploadImage.Name.Equals(findMovie.Image))
                {
                    var filehelper = new FileHelpers("Static", _env);
                    filehelper.DeleteImage(findMovie.Image);
                    await filehelper.UploadImage(movieRequest.UploadImage);

                    findMovie.Image = movieRequest.UploadImage.FileName;
                }

                findMovie.Description = movieRequest.Description;
                findMovie.Title = movieRequest.Title;
                findMovie.ReleaseDate = movieRequest.ReleaseDate;
                findMovie.IDProducer = movieRequest.IDProducer;

                _context.Movies.Update(findMovie);

                await SaveChangesAsync();

                var findDirector = await _context
                    .MovieDirectors
                    .Where(x => x.IDMovie.Equals(movieRequest.ID))
                    .Select(x => x.Director)
                    .ToListAsync();

                return new MovieResponse
                {
                    ID = movieRequest.ID,
                    Description = movieRequest.Description,
                    ImagePath = Path.Combine("Static", movieRequest.UploadImage.FileName),
                    Title = movieRequest.Title,
                    ReleaseDate = movieRequest.ReleaseDate,
                    Directors = findDirector,
                    Producer = findMovie.Producer
                };
            }

            return null;
        }
    }
}