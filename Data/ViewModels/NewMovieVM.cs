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
    public class NewMovieVM
    {
        public int ID { get; set; }

        [Display(Name = "Movie name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Movie description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Movie image")]
        [Required(ErrorMessage = "Image URL is required")]
        public string ImageURL { get; set; }

        [Display(Name = "Movie start date")]
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Movie category is required")]
        public MovieCategory MovieCategory { get; set; }

        [Display(Name = "Select actor(s)/actress(es)")]
        [Required(ErrorMessage = "Movie actor(s)/actress(es) is required")]
        //Relationships
        public List<int> ActorIDs { get; set; }

        //Director
        [Display(Name = "Select a director")]
        [Required(ErrorMessage = "Movie director is required")]
        public int DirectorID { get; set; }
    }
}
