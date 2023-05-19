using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MovieDbSystem.Data.Repositories;
using MovieDbSystem.Data.ViewModels;
using MovieDbSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDbSystem.Data.Services
{
    public class MovieService : EntityBaseRepository<Movie>, IMovieService
    {
        private readonly ApplicationDbContext _context;
        public MovieService(ApplicationDbContext _context) : base(_context)
        {
            this._context = _context;
        }

        public async Task AddMovieAsync(NewMovieVM data)
        {
            var value = new Movie()
            {
                Name = data.Name,
                Description = data.Description,
                ImageURL = data.ImageURL,
                StartDate = data.StartDate,
                MovieCategory = data.MovieCategory,
                DirectorID = data.DirectorID
            };
            await _context.Movies.AddAsync(value);
            await _context.SaveChangesAsync();

            foreach (var actorId in data.ActorIDs)
            {
                var actor = new Actor_Movie()
                {
                    MovieID = value.ID,
                    ActorID = actorId
                };
                await _context.Actor_Movies.AddAsync(actor);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovieAsync(int id)
        {
            var entity = await _context.Set<Movie>().FirstOrDefaultAsync(x => x.ID == id);
            EntityEntry entry = _context.Entry<Movie>(entity);
            entry.State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var value = await _context.Movies
                .Include(d => d.Director)
                .Include(am => am.Actor_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(x => x.ID == id);

            return value;
        }

        public async Task<NewMovieDropdownVM> GetNewMovieDropdownValues()
        {
            var value = new NewMovieDropdownVM()
            {
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Directors = await _context.Directors.OrderBy(n => n.FullName).ToListAsync()
            };
            return value;
        }

        public async Task UpdateMovieAsync(NewMovieVM data)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.ID == data.ID);

            if (movie != null)
            {
                movie.Name = data.Name;
                movie.Description = data.Description;
                movie.ImageURL = data.ImageURL;
                movie.StartDate = data.StartDate;
                movie.MovieCategory = data.MovieCategory;
                movie.DirectorID = data.DirectorID;
                await _context.SaveChangesAsync();
            }

            //Remove actors/actresses of in movie
            var existingActor = _context.Actor_Movies.Where(x => x.MovieID == data.ID).ToList();
            _context.Actor_Movies.RemoveRange(existingActor);
            await _context.SaveChangesAsync();

            foreach (var actorId in data.ActorIDs)
            {
                var actor = new Actor_Movie()
                {
                    MovieID = data.ID,
                    ActorID = actorId
                };
                await _context.Actor_Movies.AddAsync(actor);
            }
            await _context.SaveChangesAsync();

        }
    }
}
