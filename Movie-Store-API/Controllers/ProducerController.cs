using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_Store_API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Movie_Store_API.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Movie_Store_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IProducerRepository _producerRepository;

        public ProducerController(IProducerRepository producerRepository)
        {
            _producerRepository = producerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducers()
        {
            return StatusCode(StatusCodes.Status200OK, await _producerRepository.GetProducersAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducersByID(int id)
        {
            var findProducer = await _producerRepository.GetProducerByIDAsync(id);

            if (findProducer != null)
            {
                return StatusCode(StatusCodes.Status200OK, new
                {
                    success = true,
                    response = findProducer
                });
            }

            return StatusCode(StatusCodes.Status500InternalServerError,
                new { success = false, message = "Server error!" });
        }

        [HttpPost]
        public async Task<IActionResult> AddProducer([FromForm] ProducerRequest producerRequest)
        {
            try
            {
                var responseProducer = await _producerRepository.AddProducerAsync(producerRequest);
                //await _producerRepository.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created, new
                {
                    success = true,
                    message = "Added successful!",
                    response = responseProducer
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new { success = false, message = "Bad request!" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveProducer(int id)
        {
            await _producerRepository.RemoveProducerAsync(id);
            await _producerRepository.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateProducer(int id,
            [FromForm] ProducerRequest producerRequest)
        {
            try
            {
                var responseProducer = await _producerRepository.UpdateProducerAsync(id, producerRequest);

                //await _producerRepository.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, 
                    new { 
                        success = true, 
                        message = "Updated successful!", 
                        response = responseProducer 
                    });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { success = false, message = "Server error!" });
            }
        }
    }
}
