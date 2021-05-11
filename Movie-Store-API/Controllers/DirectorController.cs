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
            _logger.LogInformation("GetDirectors");
            return Ok(await _directorRepository.GetDirectorsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDirectorByID(int id)
        {
            var getDirector = await _directorRepository.GetDirectorByIDAsync(id);
            if (getDirector != null)
            {
                _logger.LogInformation("GetDirectorByID found obj with id {0}", id);
                return Ok(getDirector);
            }
            _logger.LogInformation("GetDirectorByID not found obj with id {0}", id);
            return Ok(new { success = false, message = "Not found!" });
        }

        [HttpPost]
        public async Task<IActionResult> AddDirector([FromForm] DirectorRequest directorRequest)
        {
            var response = await _directorRepository.AddDirectorAsync(directorRequest);
            if (response == null) return StatusCode(StatusCodes.Status500InternalServerError,
                new
                {
                    success = false,
                    message = "Server error! Try again!"
                });


            return Ok(new
            {
                success = true,
                message = "Added sucessful",
                response
            });
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateDirector(
            int id,
            DirectorRequest directorRequest)
        {
            await _directorRepository.UpdateProducerAsync(id, directorRequest);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveDirector(int id)
        {
            try
            {
                await _directorRepository.RemoveDirectorAsync(id);
                await _directorRepository.SaveChangesAsync();
                return Ok(new
                {
                    success = true,
                    message = "Remove successfully!"
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        success = false,
                        message = "Server error! The id of director can not delete right now, please try again!"
                    });
            }
        }
    }
}
