using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movie_store.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_store.Models;
using Microsoft.EntityFrameworkCore;
using Movie_store.Repository.Interface;
using Movie_store.Extensions;
using Microsoft.AspNetCore.Hosting;

namespace Movie_store.Controllers
{
    public class DirectorController : Controller
    {
        private readonly ILogger<DirectorController> _logger;
        private readonly IDirectorRepository _directorRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IWebHostEnvironment _hosting;
        private readonly IMovieDirectorRepository _movieDirectorRepository;

        public DirectorController(
            ILogger<DirectorController> logger,
            IDirectorRepository directorRepository,
            IMovieRepository movieRepository,
            IMovieDirectorRepository movieDirectorRepository,
            IWebHostEnvironment hosting
            )
        {
            _logger = logger;
            _directorRepository = directorRepository;
            _movieRepository = movieRepository;
            _movieDirectorRepository = movieDirectorRepository;
            _hosting = hosting;
        }

        public async Task<IActionResult> List(int? pageNumber)
        {
            var directors = await _directorRepository.GetAll();
            return View(PaginatedList<Director>
                .Create(directors.AsQueryable(), pageNumber ?? 1, 10));
        }

        // GET: DirectorController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DirectorController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            ViewData["Movies"] = await _directorRepository.GetMovies(id);
            return View(await _directorRepository.FindByID(id));
        }

        // GET: DirectorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DirectorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Director director)
        {
            if (director == null) return BadRequest();

            try
            {
                director.Image = director.UploadImage.FileName;
                await UploadFileHelper.Instance.Upload(director.UploadImage, _hosting);
                _directorRepository.Add(director);
                await _directorRepository.SaveAsync();
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        // GET: DirectorController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _directorRepository.FindByID(id));
        }

        // POST: DirectorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Director director)
        {
            if (director == null) return BadRequest();


            try
            {
                var FindDirector = await _directorRepository.FindByID(id);

                if (director.UploadImage != null)
                {
                    UploadFileHelper.Instance.Delete(FindDirector.Image, _hosting);
                    await UploadFileHelper.Instance.Upload(director.UploadImage, _hosting);
                }
                else director.Image = FindDirector.Image;

                await _directorRepository.UpdateAsync(id, director);
                await _directorRepository.SaveAsync();
                return RedirectToAction(nameof(List));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View(director);
            }
        }

        // GET: DirectorController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _directorRepository.FindByID(id));
        }

        // POST: DirectorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Director director)
        {
            try
            {
                _directorRepository.Remove(director);
                await _directorRepository.SaveAsync();
                return RedirectToAction(nameof(List));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View(director);
            }
        }
    }
}
