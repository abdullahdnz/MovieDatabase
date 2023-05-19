using Microsoft.AspNetCore.Mvc;
using MovieDbSystem.Data;
using MovieDbSystem.Data.Services;
using MovieDbSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDbSystem.Controllers
{
    public class ActorController : Controller
    {
        private readonly IActorService _service;

        public ActorController(IActorService _service)
        {
            this._service = _service;
        }

        public async Task<IActionResult> Index()
        {
            var value = await _service.GetAllAsync();
            return View(value);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName, ProfilePictureURL, Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }

            await _service.AddAsync(actor);
            return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("ID, FullName, ProfilePictureURL, Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }

            await _service.UpdateAsync(id, actor);
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

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
