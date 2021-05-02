﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_Store_API.ViewModels;
using Movie_Store_API.Repository.Interface;
using Movie_Store_Data.Data;
using Movie_Store_Data.Models;
using Movie_Store_API.Extensions;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Movie_Store_API.Repository
{
    public class DirectorRepository : IDirectorRepository
    {
        private readonly MovieDBContext _movieDBContext;
        private readonly IWebHostEnvironment _env;
        public DirectorRepository(MovieDBContext movieDBContext,
            IWebHostEnvironment env)
        {
            _movieDBContext = movieDBContext;
            _env = env;
        }

        public async Task<DirectorResponse> AddDirectorAsync(DirectorRequest directorRequest)
        {
            if (directorRequest.UploadImage != null)
            {
                //upload image
                if (await new FileHelpers("Static", _env).UploadImage(directorRequest.UploadImage))
                {
                    var director = new Director
                    {
                        BirthDate = directorRequest.BirthDate,
                        FullName = directorRequest.FullName,
                        Gender = directorRequest.Gender,
                        PlaceofBirth = directorRequest.PlaceofBirth,
                        Image = directorRequest.UploadImage.FileName
                    };

                    _movieDBContext.Directors.Add(director);
                    await SaveChangesAsync();

                    return new DirectorResponse
                    {
                        ID = director.ID,
                        BirthDate = director.BirthDate,
                        FullName = director.FullName,
                        Gender = director.Gender,
                        PlaceofBirth = director.PlaceofBirth,
                        Image = Path.Combine("Static", director.Image),
                        Movies = null
                    };
                }

            }
            return null;
        }

        public async Task<DirectorResponse> GetDirectorByIDAsync(int id)
        {
            var findDirector = await _movieDBContext.Directors
                .Include(x => x.MovieDirectors)
                .ThenInclude(x=> x.Movie)
                .SingleOrDefaultAsync(x => x.ID.Equals(id));
            if (findDirector != null)
            {

                var listMovie = findDirector
                    .MovieDirectors
                    .Where(x => x.IDDirector.Equals(id))
                    .Select(x => new MovieResponse
                    {
                        ID = x.Movie.ID,
                        Description = x.Movie.Description,
                        ReleaseDate = x.Movie.ReleaseDate,
                        Title = x.Movie.Title,
                        ImagePath = Path.Combine("Static", x.Movie.Image)
                    }).ToList();

                return new DirectorResponse
                {
                    ID = findDirector.ID,
                    BirthDate = findDirector.BirthDate,
                    FullName = findDirector.FullName,
                    Gender = findDirector.Gender,
                    PlaceofBirth = findDirector.PlaceofBirth,
                    Image = Path.Combine("Static", findDirector.Image),
                    Movies = listMovie
                };
            }
            return null;
        }

        public async Task<List<DirectorResponse>> GetDirectorsAsync()
        {
            return await _movieDBContext
                .Directors
                .Include(x => x.MovieDirectors)
                .Select(x => new DirectorResponse
                {
                    ID = x.ID,
                    BirthDate = x.BirthDate,
                    FullName = x.FullName,
                    Gender = x.Gender,
                    PlaceofBirth = x.PlaceofBirth,
                    Image = Path.Combine("Static", x.Image)
                })
                .ToListAsync();
        }

        public async Task RemoveDirectorAsync(int idDirector)
        {
            var findDirector = await _movieDBContext
                .Directors
                .SingleOrDefaultAsync(x => x.ID.Equals(idDirector));

            _movieDBContext.Directors.Remove(findDirector);
        }

        public async Task SaveChangesAsync()
        {
            await _movieDBContext.SaveChangesAsync();
        }

        public Task<DirectorResponse> UpdateProducerAsync(
            int idDirector,
            DirectorRequest producerRequest)
        {
            throw new NotImplementedException();
        }
    }
}