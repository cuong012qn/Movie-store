using System;
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

        public DirectorRepository(
            MovieDBContext movieDBContext,
            IWebHostEnvironment env)
        {
            _movieDBContext = movieDBContext;
            _env = env;
        }

        public async Task AddDirectorAsync(DirectorRequest directorRequest)
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
                }

            }
        }

        public async Task<DirectorResponse> GetDirectorByIDAsync(int id)
        {
            var findDirector = await _movieDBContext.Directors
                .Include(x => x.MovieDirectors)
                .ThenInclude(x => x.Movie)
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
                        ReleaseDate = x.Movie.ReleaseDate.ToString("dd-MM-yyyy"),
                        Title = x.Movie.Title,
                        ImagePath = Path.Combine("Static", x.Movie.Image)
                    }).ToList();

                return new DirectorResponse
                {
                    ID = findDirector.ID,
                    BirthDate = findDirector.BirthDate.ToString("dd-MM-yyyy"),
                    FullName = findDirector.FullName,
                    Gender = findDirector.Gender,
                    PlaceofBirth = findDirector.PlaceofBirth,
                    Image = Path.Combine("Static", findDirector.Image),
                    Movies = listMovie
                };
            }
            return null;
        }

        /// <summary>
        /// Get all director
        /// </summary>
        /// <returns>DirectorResponse</returns>
        public async Task<List<DirectorResponse>> GetDirectorsAsync()
        {
            return await _movieDBContext
                .Directors
                .Include(x => x.MovieDirectors)
                //Cast from Director to DirectorResponse
                .Select(x => new DirectorResponse
                {
                    ID = x.ID,
                    BirthDate = x.BirthDate.ToString("dd-MM-yyyy"),
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

        /// <summary>
        /// Update product
        /// if update success return DirectorResonpse
        /// else return null
        /// </summary>
        /// <param name="idDirector">ID of Director</param>
        /// <param name="directorRequest">Director request</param>
        /// <returns>Director response</returns>
        public async Task UpdateProducerAsync(
            int idDirector,
            DirectorRequest directorRequest)
        {
            //Find director
            var findDirector = await _movieDBContext
                .Directors.SingleOrDefaultAsync(x => x.ID.Equals(idDirector));

            //If found director
            //MakeChange
            if (findDirector != null)
            {
                //make changed image
                if (directorRequest.UploadImage != null)
                {
                    var filehelper = new FileHelpers("Static", _env);
                    filehelper.DeleteImage(findDirector.Image);
                    await filehelper.UploadImage(directorRequest.UploadImage);

                    findDirector.Image = directorRequest.UploadImage.FileName;
                }

                //Make changed
                findDirector.BirthDate = directorRequest.BirthDate;
                findDirector.FullName = directorRequest.FullName;
                findDirector.Gender = directorRequest.Gender;
                findDirector.PlaceofBirth = findDirector.PlaceofBirth;

                await SaveChangesAsync();
            }
        }
    }
}
