using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movie_Store_API.Repository.Interface;
using Movie_Store_API.ViewModels;
using System.Threading.Tasks;

namespace Movie_Store_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DirectorController : ControllerBase
    {
        private readonly IDirectorRepository _directorRepository;
        private readonly ILogger<DirectorController> _logger;

        public DirectorController(IDirectorRepository directorRepository,
            ILogger<DirectorController> logger)
        {
            _directorRepository = directorRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetDirectors()
        {
            try
            {
                var directors = await _directorRepository.GetDirectorsAsync();

                return StatusCode(StatusCodes.Status200OK,
                    new
                    {
                        message = directors.Count + " director",
                        success = true,
                        directors
                    });
            }
            catch
            {
                return StatusCode(500,
                    new
                    {
                        success = false,
                        message = "Server error! Try again!"
                    });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDirectorByID(int id)
        {
            var director = await _directorRepository.GetDirectorByIDAsync(id);

            if (director == null)
                return StatusCode(StatusCodes.Status200OK,
                    new
                    {
                        message = "Not found!",
                        success = true
                    });


            return StatusCode(StatusCodes.Status200OK,
                new
                {
                    message = "Found director",
                    success = true,
                    director
                });
        }

        [HttpPost]
        public async Task<IActionResult> AddDirector([FromForm] DirectorRequest directorRequest)
        {
            try
            {
                await _directorRepository.AddDirectorAsync(directorRequest);
                return StatusCode(StatusCodes.Status201Created, new
                {
                    message = "added successfully",
                    success = true
                });
            }
            catch
            {
                return StatusCode(500,
                    new
                    {
                        message = "Server error! Try again",
                        success = false
                    });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDirector(
            int id,
            DirectorRequest directorRequest)
        {
            try
            {
                await _directorRepository.UpdateProducerAsync(id, directorRequest);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch
            {
                return StatusCode(500,
                    new
                    {
                        success = false,
                        message = "Server error! Try again"
                    });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveDirector(int id)
        {
            try
            {
                await _directorRepository.RemoveDirectorAsync(id);
                await _directorRepository.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch
            {
                return StatusCode(500,
                     new
                     {
                         success = false,
                         message = "Server error! Try again!"
                     });
            }
        }
    }
}
