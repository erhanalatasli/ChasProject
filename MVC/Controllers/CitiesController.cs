#nullable disable
using Business.Models;
using Business.Services;
using Core.Results.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class CitiesController : Controller
    {
        // Add service injections here
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        // GET: Cities
        [Authorize(Roles = "Admin, Doctor, Nurse, Patient")]
        public IActionResult Index()
        {
            List<CityModel> cityList = _cityService.Query().ToList();
            return View(cityList);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult IndexJson()
        {
            var cityList = _cityService.Query().ToList();
            return Json(cityList);
        }

        // GET: Cities/Details/5
        [Authorize(Roles = "Admin, Doctor, Nurse, Patient")]
        public IActionResult Details(int id)
        {
            CityModel city = _cityService.Query().SingleOrDefault(c => c.Id == id);
            if (city == null)
            {
                return View("_Error", "City not found");
            }
            return View(city);
        }

        // GET: Cities/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(CityModel city)
        {
            if (ModelState.IsValid)
            {
                Result result = _cityService.Add(city);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(city);
        }

        // GET: Cities/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            CityModel city = _cityService.Query().SingleOrDefault(c => c.Id == id);
            if (city == null)
            {
                return View("_Error", "City not found");
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(city);
        }

        // POST: Cities/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(CityModel city)
        {
            if (ModelState.IsValid)
            {
                Result result = _cityService.Update(city);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(city);
        }

        // GET: Cities/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            CityModel city = _cityService.Query().SingleOrDefault(c => c.Id == id);
            if (city is null)
            {
                return View("_Error", "City not found!");
            }

            Result result = _cityService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
