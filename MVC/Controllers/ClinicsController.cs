#nullable disable
using Business.Models;
using Business.Services;
using Core.Results.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class ClinicsController : Controller
    {
        // Add service injections here
        private readonly IClinicService _clinicService;

        public ClinicsController(IClinicService clinicService)
        {
            _clinicService = clinicService;
        }

        // GET: Clinics
        [Authorize(Roles = "Admin, Doctor, Nurse, Patient")]
        public IActionResult Index()
        {
            List<ClinicModel> clinicList = _clinicService.Query().ToList();
            return View(clinicList);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult IndexJson()
        {
            var clinicList = _clinicService.Query().ToList();
            return Json(clinicList);
        }

        // GET: Clinics/Details/5
        [Authorize(Roles = "Admin, Doctor, Nurse")]
        public IActionResult Details(int id)
        {
            ClinicModel clinic = _clinicService.Query().SingleOrDefault(c => c.Id == id);
            if (clinic == null)
            {
                return View("_Error", "Clinic not found!");
            }
            return View(clinic);
        }

        // GET: Clinics/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View();
        }

        // POST: Clinics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(ClinicModel clinic)
        {
            if (ModelState.IsValid)
            {
                Result result = _clinicService.Add(clinic);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(clinic);
        }

        // GET: Clinics/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            ClinicModel clinic = _clinicService.Query().SingleOrDefault(c => c.Id == id);
            if (clinic == null)
            {
                return View("_Error", "Clinic not found!");
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(clinic);
        }

        // POST: Clinics/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(ClinicModel clinic)
        {
            if (ModelState.IsValid)
            {
                Result result = _clinicService.Update(clinic);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(clinic);
        }

        // GET: Clinics/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            ClinicModel clinic = _clinicService.Query().SingleOrDefault(c => c.Id == id);
            if (clinic is null)
            {
                return View("_Error", "Clinic not found!");
            }

            Result result = _clinicService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
