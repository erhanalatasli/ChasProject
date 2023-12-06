#nullable disable
using Business.Models;
using Business.Services;
using Core.Results.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class DistrictsController : Controller
    {
        // Add service injections here
        private readonly IDistrictService _districtService;
        private readonly ICityService _cityService;

        public DistrictsController(IDistrictService districtService, ICityService cityService)
        {
            _districtService = districtService;
            _cityService = cityService;
        }

        // GET: Districts
        [Authorize(Roles = "Admin, Doctor, Nurse, Patient")]
        public IActionResult Index()
        {
            List<DistrictModel> districtList = _districtService.Query().OrderBy(d => d.CityDisplay).ToList();
            return View(districtList);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult IndexJson()
        {
            var districtList = _districtService.Query().ToList();
            return Json(districtList);
        }

        // GET: Districts/Details/5
        [Authorize(Roles = "Admin, Doctor, Nurse, Patient")]
        public IActionResult Details(int id)
        {
            DistrictModel district = _districtService.Query().SingleOrDefault(d => d.Id == id);
            if (district == null)
            {
                return View("_Error", "District not found");
            }
            return View(district);
        }

        // GET: Districts/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["CityId"] = new SelectList(_cityService.Query().ToList(), "Id", "Name");
            return View();
        }

        // POST: Districts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(DistrictModel district)
        {
            if (ModelState.IsValid)
            {
                Result result = _districtService.Add(district);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["CityId"] = new SelectList(_cityService.Query().ToList(), "Id", "Name", district.CityId);
            return View(district);
        }

        // GET: Districts/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            DistrictModel district = _districtService.Query().SingleOrDefault(d => d.Id == id);
            if (district == null)
            {
                return View("_Error", "District not found");
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["CityId"] = new SelectList(_cityService.Query().ToList(), "Id", "Name", district.CityId);
            return View(district);
        }

        // POST: Districts/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(DistrictModel district)
        {
            if (ModelState.IsValid)
            {
                Result result = _districtService.Update(district);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["CityId"] = new SelectList(_cityService.Query().ToList(), "Id", "Name", district.CityId);
            return View(district);
        }

        // GET: Districts/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            DistrictModel district = _districtService.Query().SingleOrDefault(d => d.Id == id);
            if (district == null)
            {
                return View("_Error", "District not found");
            }
            //return View(district);

            Result result = _districtService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
