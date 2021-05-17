using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Movie_Store_FE.ApiClient.Interface;
using Movie_Store_FE.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Store_FE.Controllers
{
    [Authorize]
    public class MovieController : Controller
    {
        private readonly IMovieApiClient movieApiClient;
        private readonly IDirectorApiClient directorApiClient;
        private readonly IProducerApiClient producerApiClient;
        private readonly IConfiguration configuration;

        public MovieController(IMovieApiClient movieApiClient,
            IDirectorApiClient directorApiClient,
            IProducerApiClient producerApiClient,
            IConfiguration configuration)
        {
            this.movieApiClient = movieApiClient;
            this.directorApiClient = directorApiClient;
            this.producerApiClient = producerApiClient;
            this.configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> AddDirector(int idMovie)
        {
            ViewData["IDMovie"] = idMovie;
            return View(await GetDirectorsDistinct(idMovie));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDirector(int idMovie, int idDirector)
        {
            await movieApiClient.AddDirector(idMovie, idDirector);
            return RedirectToAction("Index", "Movie");
        }

        public async Task<List<Director>> GetDirectorsDistinct(int IDMovie)
        {
            var director = await directorApiClient.GetDirectors();
            var movie = await movieApiClient.GetMovie(IDMovie);
           
            return director.Directors.Except(movie.Movie.Directors).ToList();
        }

        public async Task<IActionResult> Index()
        {

            var response = await movieApiClient.GetMovies();

            //Unauthorized
            if (response == null)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login", "User");
            }

            //foreach (var item in response.Movies)
            //{
            //    item.ImagePath = Path.Combine(configuration.GetValue<string>("Api"), item.ImagePath);
            //}

            ViewBag.Api = configuration.GetValue<string>("Api");

            return View(response.Movies);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Api = configuration.GetValue<string>("Api");
            var response = await movieApiClient.GetMovie(id);
            if (response != null)
            {
                return View(response.Movie);
            }

            return RedirectToAction("Index", "Movie");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var getProducers = await producerApiClient.GetProducers();
            ViewBag.IDProducer = new SelectList(getProducers.Producers, "ID", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie)
        {
            var request = new MovieRequest
            {
                UploadImage = movie.UploadImage,
                Description = movie.Description,
                IDProducer = movie.IDProducer,
                Title = movie.Title,
                ReleaseDate = DateTime.ParseExact(movie.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture)
            };

            await movieApiClient.AddMovie(request);

            return RedirectToAction("Index", "Movie");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            ViewBag.Api = configuration.GetValue<string>("Api");
            var response = await movieApiClient.GetMovie(id);
            if (response != null)
            {
                return View(response.Movie);
            }

            return RedirectToAction("Index", "Movie");
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteComfirmed(int id)
        {
            await movieApiClient.RemoveMovie(id);
            return RedirectToAction("Index", "Movie");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Api = configuration.GetValue<string>("Api");

            var getProducers = await producerApiClient.GetProducers();
            ViewBag.IDProducer = new SelectList(getProducers.Producers, "ID", "FullName");

            var response = await movieApiClient.GetMovie(id);
            if (response != null)
            {
                return View(response.Movie);
            }

            return RedirectToAction("Index", "Movie");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieRequest movieRequest)
        {
            await movieApiClient.UpdateMovie(id, movieRequest);
            return RedirectToAction("Index", "Movie");
        }
    }
}
