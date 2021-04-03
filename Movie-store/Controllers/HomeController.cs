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

namespace Movie_store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MovieDBContext _context;
        private readonly IMovieRepository _movieRepository;

        public HomeController(ILogger<HomeController> logger, IMovieRepository movieRepository)
        {
            _logger = logger;
            _movieRepository = movieRepository;
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _movieRepository.FindByID(id));
        }

        public async Task<IActionResult> Index()
        {
            return View(await _movieRepository.GetAll());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
