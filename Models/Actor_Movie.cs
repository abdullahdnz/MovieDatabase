using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDbSystem.Models
{
    public class Actor_Movie
    {
        public int MovieID { get; set; }
        public Movie Movie { get; set; }
        public int ActorID { get; set; }
        public Actor Actor { get; set; }
    }
}
