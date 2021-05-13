using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_Store_API.Repository.Interface;
using Movie_Store_API.ViewModels;
using System.Threading.Tasks;

namespace Movie_Store_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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

        [HttpPost("adddirector")]
        public async Task<IActionResult> AddDirector(
            [FromForm] int IDMovie,
            [FromForm] int IDDirector)
        {
            try
            {
                await _movieRepository.AddDirectorAsync(IDMovie, IDDirector);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch
            {
                return StatusCode(500,
                    new { message = "Server error! Try again" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieByID(int id)
        {
            return Ok(new { movie = await _movieRepository.GetMovieByIDAsync(id) });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromForm] MovieRequest movieRequest)
        {
            try
            {
                await _movieRepository.UpdateMovieAsync(id, movieRequest);
                return StatusCode(StatusCodes.Status204NoContent, new { message = "Resource updated successfully" });
            }
            catch
            {
                return StatusCode(500,
                    new { success = false, message = "Server error! Try again" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveMovie(int id)
        {
            try
            {
                await _movieRepository.RemoveMovie(id);
                await _movieRepository.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent, new { message = "Resource updated successfully" });
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
            try
            {
                await _movieRepository.AddMovieAsync(request);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch
            {
                return StatusCode(500,
                    new { success = false, message = "Server error! Try again!" });
            }
        }
    }
}
