using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movie_store.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Movie_store.Data;
using Microsoft.EntityFrameworkCore;
using Movie_store.Repository.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using Movie_store.Extensions;
using Microsoft.AspNetCore.Hosting;

namespace Movie_store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieRepository _movieRepository;
        private readonly IProducerRepository _producerRepository;
        private readonly IWebHostEnvironment _hosting;

        public HomeController(
            ILogger<HomeController> logger,
            IMovieRepository movieRepository,
            IProducerRepository producerRepository,
            IWebHostEnvironment hosting)
        {
            _logger = logger;
            _movieRepository = movieRepository;
            _producerRepository = producerRepository;
            _hosting = hosting;
        }

        public async Task<IActionResult> Details(int id)
        {
            ViewData["Directors"] = await _movieRepository.GetDirectors(id);
            return View(await _movieRepository.FindByID(id));
        }

        public async Task<IActionResult> Index()
        {
            return View(await _movieRepository.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.IDProducer = new SelectList(await _producerRepository.GetAll(), "ID", "FullName");
            return View(await _movieRepository.FindByID(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Movie movie)
        {
            if (movie == null) return BadRequest();

            try
            {
                var findMovie = await _movieRepository.FindByID(id);

                if (movie.UploadImage != null)
                {
                    UploadFileHelper.Instance.Delete(findMovie.Image, _hosting);
                    await UploadFileHelper.Instance.Upload(movie.UploadImage, _hosting);
                }
                else movie.Image = findMovie.Image;

                await _movieRepository.UpdateAsync(id, movie);
                await _movieRepository.SaveAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View(movie);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _movieRepository.FindByID(id));
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            try
            {
                _movieRepository.Remove(await _movieRepository.FindByID(id));
                await _movieRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
