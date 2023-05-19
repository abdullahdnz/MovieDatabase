using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieDbSystem.Data;
using MovieDbSystem.Data.Services;
using MovieDbSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDbSystem.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _service;

        public MovieController(IMovieService _service)
        {
            this._service = _service;
        }
        public async Task<IActionResult> Index()
        {
            var value = await _service.GetAllAsync();
            return View(value);
        }
        public async Task<IActionResult> Filter(string searchString)
        {
            var value = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var result = value.Where(x => x.Name.ToLower().Contains(searchString) || x.Description.ToLower().Contains(searchString)).ToList();
                return View("Index", result);
            }
            return View("Index", value);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var value = await _service.GetMovieByIdAsync(id);
            return View(value);
        }

        public async Task<IActionResult> Create()
        {
            var value = await _service.GetNewMovieDropdownValues();

            ViewBag.Directors = new SelectList(value.Directors, "ID", "FullName");
            ViewBag.Actors = new SelectList(value.Actors, "ID", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var value = await _service.GetNewMovieDropdownValues();

                ViewBag.Directors = new SelectList(value.Directors, "ID", "FullName");
                ViewBag.Actors = new SelectList(value.Actors, "ID", "FullName");

                return View(movie);
            }
            await _service.AddMovieAsync(movie);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var value = await _service.GetMovieByIdAsync(id);
            if (value == null)
            {
                return View("NotFound");
            }

            var query = new NewMovieVM()
            {
                ID = value.ID,
                Name = value.Name,
                Description = value.Description,
                StartDate = value.StartDate,
                ImageURL = value.ImageURL,
                MovieCategory = value.MovieCategory,
                DirectorID = value.DirectorID,
                ActorIDs = value.Actor_Movies.Select(n => n.ActorID).ToList(),
            };

            var movieData = await _service.GetNewMovieDropdownValues();

            ViewBag.Directors = new SelectList(movieData.Directors, "ID", "FullName");
            ViewBag.Actors = new SelectList(movieData.Actors, "ID", "FullName");

            return View(query);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM movie)
        {
            if (id != movie.ID)
            {
                return View("NotFound");
            }

            if (!ModelState.IsValid)
            {
                var value = await _service.GetNewMovieDropdownValues();

                ViewBag.Directors = new SelectList(value.Directors, "ID", "FullName");
                ViewBag.Actors = new SelectList(value.Actors, "ID", "FullName");

                return View(movie);
            }
            await _service.UpdateMovieAsync(movie);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var value = await _service.GetByIdAsync(id);

            if (value == null)
            {
                return View("NotFound");
            }

            return View(value);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var value = await _service.GetByIdAsync(id);

            if (value == null)
            {
                return View("NotFound");
            }

            await _service.DeleteMovieAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
