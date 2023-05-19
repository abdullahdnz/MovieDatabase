using MovieDbSystem.Data.Repositories;
using MovieDbSystem.Data.ViewModels;
using MovieDbSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDbSystem.Data.Services
{
    public interface IMovieService : IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropdownVM> GetNewMovieDropdownValues();
        Task AddMovieAsync(NewMovieVM data);
        Task UpdateMovieAsync(NewMovieVM data);
        Task DeleteMovieAsync(int id);
    }
}
