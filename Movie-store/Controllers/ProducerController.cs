using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movie_store.Models;
using Movie_store.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_store.Controllers
{
    public class ProducerController : Controller
    {
        private readonly ILogger<ProducerController> _logger;
        private readonly IMovieRepository _movieRepository;
        private readonly IProducerRepository _producerRepository;
        private readonly IWebHostEnvironment _hosting;

        public ProducerController(
            ILogger<ProducerController> logger,
            IMovieRepository movieRepository,
            IProducerRepository producerRepository,
            IWebHostEnvironment hosting)
        {
            _logger = logger;
            _movieRepository = movieRepository;
            _producerRepository = producerRepository;
            _hosting = hosting;
        }


        // GET: ProducerController
        public async Task<ActionResult> Index()
        {
            return View(await _producerRepository.GetAll());
        }

        // GET: ProducerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProducerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProducerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Producer producer)
        {
            try
            {
                _producerRepository.Add(producer);
                await _producerRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        // GET: ProducerController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _producerRepository.FindByID(id));
        }

        // POST: ProducerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Producer producer)
        {
            try
            {
                await _producerRepository.UpdateAsync(id, producer);
                await _producerRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        // GET: ProducerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProducerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Producer producer)
        {
            try
            {
                _producerRepository.Remove(producer);
                await _producerRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }
    }
}
