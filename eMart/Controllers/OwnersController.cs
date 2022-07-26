using eMart.Data;
using eMart.Data.Services;
using eMart.Data.Static;
using eMart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMart.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class OwnersController : Controller
    {
        private readonly IOwnersService _service;

        public OwnersController(IOwnersService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var AllOwners =await _service.GetAllAsync();
            return View(AllOwners);
        }

        //Get: Owners/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name")]ProductOwner owner)
        {
            if(!ModelState.IsValid)
            {
                return View(owner);
            }
            await _service.AddAsync(owner); 
            return RedirectToAction(nameof(Index));
        }

        //Get: Actors/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var ownerDetails =await _service.GetByIdAsync(id);

            if (ownerDetails == null) return View("NotFound");
            return View(ownerDetails);
        }

        //Get: Owners/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var ownerDetails = await _service.GetByIdAsync(id);

            if (ownerDetails == null) return View("NotFound");
            return View(ownerDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,Logo,Name")] ProductOwner owner)
        {
            if (!ModelState.IsValid)
            {
                return View(owner);
            }
            await _service.UpdateAsync(id,owner);
            return RedirectToAction(nameof(Index));
        }

        //Get: Owners/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var ownerDetails = await _service.GetByIdAsync(id);

            if (ownerDetails == null) return View("NotFound");
            return View(ownerDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ownerDetails = await _service.GetByIdAsync(id);
            if (ownerDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
