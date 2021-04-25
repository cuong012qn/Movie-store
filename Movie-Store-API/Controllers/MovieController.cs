using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_Store_Data.Data;
using Movie_Store_API.Repository.Interface;
using Microsoft.AspNetCore.Hosting;
using Movie_Store_API.Extensions;
using Movie_Store_API.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Movie_Store_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IWebHostEnvironment _env;

        public MovieController(IMovieRepository movieRepository,
            IWebHostEnvironment env)
        {
            _movieRepository = movieRepository;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateMovie([FromForm] MovieRequest movieRequest)
        {

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> RemoveMovie(int id)
        {
            try
            {
                await _movieRepository.DeleteMovie(id);
                await _movieRepository.SaveChangesAsync();

                return StatusCode(200, new { sucess = true });
            }
            catch
            {
                return StatusCode(500, new { sucess = false, message = "Server error! Try again!" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            return Ok(await _movieRepository.GetMoviesAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie([FromForm] MovieRequest request)
        {
            var saveImage = await new FileHelpers("Static", _env).UploadImage(request.UploadImage);

            if (saveImage)
            {
                var responseMovie = await _movieRepository.AddMovieAsync(request);
                //await _movieRepository.SaveChangesAsync();

                return Ok(new
                {
                    sucess = true,
                    message = "Added successful!",
                    response = responseMovie
                });
            }
            return StatusCode(500,
                new { success = false, message = "Server error! Try again!" });
        }
    }
}
