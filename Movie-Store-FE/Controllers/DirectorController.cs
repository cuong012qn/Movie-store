using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Movie_Store_FE.ApiClient.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Store_FE.Controllers
{
    public class DirectorController : Controller
    {

        private readonly IConfiguration configuration;
        private readonly IDirectorApiClient directorApiClient;

        public DirectorController(IConfiguration configuration, IDirectorApiClient directorApiClient)
        {
            this.configuration = configuration;
            this.directorApiClient = directorApiClient;
        }

        // GET: DirectorController
        public async Task<IActionResult> Index()
        {
            var director = await directorApiClient.GetDirectors();
            return View(director.Directors);
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
