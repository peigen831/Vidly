using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using System.Linq;
using Vidly.Models;
using Vidly.ViewModels;
using System;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieViewModel
                {
                    Movie= movie,
                    Genre = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(c => c.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
            }
            _context.SaveChanges();
            return  RedirectToAction ("Index", "Movies");
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.Single(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieViewModel
            {
                Movie = movie,
                Genre = _context.Genres.ToList()              
            };
            return View("MovieForm", viewModel);
        }

        public ActionResult New()
        {
            var genre = _context.Genres.ToList();
            var viewModel = new MovieViewModel()
            {
                Genre = genre
            };
            return View("MovieForm", viewModel);
        }

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
        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content($"{year}/{month}");
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(c => c.Genre).SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }

        public ActionResult Index(int? pageIndex, string sortBy)
        {
            var movies = _context.Movies.Include(c => c.Genre).ToList();
            return View(movies);
        }



        //return new EmptyResult();
        //return RedirectToAction("Index", "Home",  new{ page = 1, sortBy = "name"});

        //if (!pageIndex.HasValue)
        //    pageIndex = 1;
        //if (String.IsNullOrWhiteSpace(sortBy))
        //    sortBy = "Name";
        //return Content(String.Format($"pageIndex={pageIndex}&sortBy={sortBy}"));
    }
}