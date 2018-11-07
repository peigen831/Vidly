using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movie
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "JinWo!" };
            var customers = new List<Customer>
            {
                new Customer{ Name = "Customer 1"},
                new Customer{ Name = "Customer 2"},
            };

            var viewModel = new RandomMovieViewModel { Movie = movie, Customers = customers};
            
            return View(viewModel);
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home",  new{ page = 1, sortBy = "name"});
        }

        public ActionResult Edit(int movieId)
        {
            return Content($"id={movieId}");
        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content($"{year}/{month}");
        }

        public ActionResult Index(int? pageIndex, string sortBy)
        {
            var movies = GetMovies();
            return View(movies);
            //if (!pageIndex.HasValue)
            //    pageIndex = 1;
            //if (String.IsNullOrWhiteSpace(sortBy))
            //    sortBy = "Name";
            //return Content(String.Format($"pageIndex={pageIndex}&sortBy={sortBy}"));
        }

        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie> {
                new Movie{ Id = 1, Name ="Along with Gods" },
                new Movie{ Id = 2, Name ="KongFu Panda" },
            };
        }
    }
}