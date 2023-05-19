using Microsoft.AspNetCore.Mvc;
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
    public class DirectorController : Controller
    {
        private readonly IDirectorService _service;

        public DirectorController(IDirectorService _service)
        {
            this._service = _service;
        }
        public async Task<IActionResult> Index()
        {
            var value = await _service.GetAllAsync();
            return View(value);
        }

        public async Task<IActionResult> Details(int id)
        {
            var value = await _service.GetByIdAsync(id);

            if (value == null)
            {
                return View("NotFound");
            }

            return View(value);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName, ProfilePictureURL, Bio")] Director director)
        {
            if (!ModelState.IsValid)
            {
                return View(director);
            }

            await _service.AddAsync(director);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var value = await _service.GetByIdAsync(id);

            if (value == null)
            {
                return View("NotFound");
            }

            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("ID, FullName, ProfilePictureURL, Bio")] Director director)
        {
            if (!ModelState.IsValid)
            {
                return View(director);
            }

            if (id == director.ID)
            {
                await _service.UpdateAsync(id, director);
                return RedirectToAction(nameof(Index));
            }

            return View(director);
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

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
