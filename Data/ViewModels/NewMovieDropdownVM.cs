using MovieDbSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDbSystem.Data.ViewModels
{
    public class NewMovieDropdownVM
    {
        public NewMovieDropdownVM()
        {
            Directors = new List<Director>();
            Actors = new List<Actor>();
        }

        public List<Director> Directors { get; set; }
        public List<Actor> Actors { get; set; }
    }
}
