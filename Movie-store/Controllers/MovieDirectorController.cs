using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movie_store.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_store.Controllers
{
    public class MovieDirectorController : Controller
    {
        private readonly ILogger<MovieDirectorController> _logger;
        private readonly IMovieRepository _movieRepository;
        private readonly IProducerRepository _producerRepository;
        private readonly IDirectorRepository _directorRepository;
        private readonly IMovieDirectorRepository _movieDirectorRepository;
        private readonly IWebHostEnvironment _hosting;

        public MovieDirectorController(
            ILogger<MovieDirectorController> logger,
            IMovieRepository movieRepository,
            IProducerRepository producerRepository,
            IDirectorRepository directorRepository,
            IMovieDirectorRepository movieDirectorRepository,
            IWebHostEnvironment hosting)
        {
            _logger = logger;
            _movieRepository = movieRepository;
            _producerRepository = producerRepository;
            _directorRepository = directorRepository;
            _movieDirectorRepository = movieDirectorRepository;
            _hosting = hosting;
        }

        public async Task<IActionResult> AddDirector(int IDDirector, int IDMovie)
        {
            _movieDirectorRepository.Add(IDMovie, IDDirector);
            await _movieDirectorRepository.SaveAsync();
            return RedirectToAction("Index", "Home");
        }

        // GET: MovieDirectorController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MovieDirectorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MovieDirectorController/Create
        public async Task<IActionResult> Create(int IDMovie)
        {
            ViewData["IDMovie"] = IDMovie;
            return View(await _directorRepository.GetDirectorsDistinct(IDMovie));
        }


        // POST: MovieDirectorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int IDDirector, int IDMovie)
        {
            try
            {
                _movieDirectorRepository.Remove(IDMovie, IDDirector);
                await _movieDirectorRepository.SaveAsync();
                return RedirectToAction("Index","Home");
            }
            catch
            {
                return View();
            }
        }
    }
}
