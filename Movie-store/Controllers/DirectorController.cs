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

namespace Movie_store.Controllers
{
    public class DirectorController : Controller
    {
        private readonly ILogger<DirectorController> _logger;
        private readonly MovieDBContext _context;

        public DirectorController(ILogger<DirectorController> logger, MovieDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> List(int IDMovie)
        {
            List<Director> directors = await _context.MovieDirectors
                .Include(director => director.Director)
                .Where(x => IDMovie.Equals(IDMovie))
                .Select(director => director.Director)
                .ToListAsync();
            return View(directors);
        }

        // GET: DirectorController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DirectorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DirectorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DirectorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DirectorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DirectorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DirectorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DirectorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
