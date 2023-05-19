using MovieDbSystem.Data;
using MovieDbSystem.Data.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDbSystem.Models
{
    public class Movie : IEntityBase
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public DateTime StartDate { get; set; }
        public MovieCategory MovieCategory { get; set; }

        //Relationships
        public List<Actor_Movie> Actor_Movies { get; set; }

        //Director
        public int DirectorID { get; set; }
        [ForeignKey("DirectorID")]
        public Director Director { get; set; }
    }
}
