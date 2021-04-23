using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_Store_Data.Data;
using Movie_Store_API.Repository.Interface;

namespace Movie_Store_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("allmovies")]
        public async Task<IActionResult> GetMovies()
        {
            return Ok(await _movieRepository.GetMoviesAsync());
        }
    }
}
