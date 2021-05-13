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
            return Ok(await _directorRepository.GetDirectorsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDirectorByID(int id)
        {
            return StatusCode(StatusCodes.Status200OK, await _directorRepository.GetDirectorByIDAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddDirector([FromForm] DirectorRequest directorRequest)
        {
            try
            {
                var response = await _directorRepository.AddDirectorAsync(directorRequest);
                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Server error! Try again!" });
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
                return StatusCode(StatusCodes.Status204NoContent, new { message = "Resource updated successfull" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Server error! try again!" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveDirector(int id)
        {
            try
            {
                await _directorRepository.RemoveDirectorAsync(id);
                await _directorRepository.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent, new { message = "Resource updated successfull" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Server error! try again!" });
            }
        }
    }
}
