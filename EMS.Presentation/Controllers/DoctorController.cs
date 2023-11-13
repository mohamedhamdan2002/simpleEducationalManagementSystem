﻿using EMS.Services.Contracts;
using EMS.Service.ViewModels.Doctor;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Presentation.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IServiceManager _service;
        public DoctorController(IServiceManager service)
        {
            _service = service;
        }
        // GET: DoctorsController
        public async Task<IActionResult> Index()
        {
            var models = await _service.DoctorService.GetDoctorsAsync(trackChanges: false);
            return View(models);
        }

        // GET: DoctorsController/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var model = await _service.DoctorService.GetDoctorAsync(id, trackChanges: false);
            return View(model);
        }

        // GET: DoctorsController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DoctorsController/Create
        [HttpPost]
        public async Task<ActionResult> Create(DoctorForCreationViewModel doctorForCreation)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    await _service.DoctorService.CreateDoctorAsync(doctorForCreation);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: DoctorsController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await _service.DoctorService.GetDoctorAsync(id, trackChanges: false);
            var modelToUpdate = new DoctorForUpdateViewModel
            {
                FirstName = model.FullName.Split(' ')[0],
                LastName = model.FullName.Split(' ')[1],
                NationalID = model.NationalID,
            };
            return View(modelToUpdate);
        }

        // POST: DoctorsController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, DoctorForUpdateViewModel doctorForUpdate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.DoctorService.UpdateDoctorAsync(id, doctorForUpdate, trackChanges: true);
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.TryAddModelError(string.Empty, ex.Message);
                return View(ModelState);
            }
        }

        // GET: DoctorsController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = await _service.DoctorService.GetDoctorAsync(id, trackChanges: false);
            return View(model);
        }

        // POST: DoctorsController/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, DoctorViewModel model)
        {
            try
            {
                await _service.DoctorService.DeleteDoctorAsync(id, trackChanges: false);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
