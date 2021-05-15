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
    [Authorize]
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
            try
            {
                var producer = await _producerRepository.GetProducersAsync();

                return StatusCode(StatusCodes.Status200OK,
                    new
                    {
                        message = producer.Count + " producer",
                        success = true,
                        producer
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
        public async Task<IActionResult> GetProducersByID(int id)
        {
            var producer = await _producerRepository.GetProducerByIDAsync(id);

            if (producer == null)
                return StatusCode(StatusCodes.Status200OK,
                    new
                    {
                        message = "Not found!",
                        success = true
                    });


            return StatusCode(StatusCodes.Status200OK,
                new
                {
                    message = "Found producer",
                    success = true,
                    producer
                });
        }

        [HttpPost]
        public async Task<IActionResult> AddProducer([FromForm] ProducerRequest producerRequest)
        {
            try
            {
                await _producerRepository.AddProducerAsync(producerRequest);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveProducer(int id)
        {
            try
            {
                await _producerRepository.RemoveProducerAsync(id);
                await _producerRepository.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent,
                    new
                    {
                        message = "Resource updated successfully",
                        success = true
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


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducer(int id,
            [FromForm] ProducerRequest producerRequest)
        {
            try
            {
                await _producerRepository.UpdateProducerAsync(id, producerRequest);
                return StatusCode(StatusCodes.Status204NoContent,
                    new
                    {
                        message = "Resource updated successfully",
                        success = true
                    });
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
    }
}
