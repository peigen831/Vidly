using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieViewModel
    {
        public Movie Movie { get; set; }
        public IEnumerable<Genre> Genre{ get; set; }
        public string Title{
            get
            {
                if(Movie == null || Movie.Id == 0)
                {
                    return "New movie";
                }
                return "Edit Movie";
            }
        }
    }
}