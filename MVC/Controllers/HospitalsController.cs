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
    public class HospitalsController : Controller
    {
        // Add service injections here
        private readonly IHospitalService _hospitalService;
        private readonly ICityService _cityService;
        private readonly IDistrictService _districtService;

        public HospitalsController(IHospitalService hospitalService, ICityService cityService, IDistrictService districtService)
        {
            _hospitalService = hospitalService;
            _cityService = cityService;
            _districtService = districtService;
        }

        // GET: Hospitals
        [Authorize(Roles = "Admin, Doctor, Nurse, Patient")]
        public IActionResult Index()
        {
            List<HospitalModel> hospitalList = _hospitalService.Query().ToList();
            return View(hospitalList);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult IndexJson()
        {
            var hospitalList = _hospitalService.Query().ToList();
            return Json(hospitalList);
        }

        // GET: Hospitals/Details/5
        [Authorize(Roles = "Admin, Doctor, Nurse")]
        public IActionResult Details(int id)
        {
            HospitalModel hospital = _hospitalService.Query().SingleOrDefault(h => h.Id == id);
            if (hospital == null)
            {
                return View("_Error", "Hospital not found");
            }
            return View(hospital);
        }

        [Authorize]
		public IActionResult GetDistrictsByCity(int cityId)
		{
			var districts = _districtService.GetListByCity(cityId);
			return Json(districts);
		}

        // GET: Hospitals/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["CityId"] = new SelectList(_cityService.Query().ToList(), "Id", "Name");
            ViewData["DistrictId"] = new SelectList(_districtService.Query().ToList(), "Id", "Name");

            return View();
        }

        // POST: Hospitals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(HospitalModel hospital)
        {
            if (ModelState.IsValid)
            {
                Result result = _hospitalService.Add(hospital);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["CityId"] = new SelectList(_cityService.Query().ToList(), "Id", "Name");
            ViewData["DistrictId"] = new SelectList(_districtService.Query().ToList(), "Id", "Name");

            return View(hospital);
        }

        // GET: Hospitals/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            HospitalModel hospital = _hospitalService.Query().SingleOrDefault(h => h.Id == id);
            if (hospital == null)
            {
                return View("_Error", "Hospital not found!");
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["CityId"] = new SelectList(_cityService.Query().ToList(), "Id", "Name");
            ViewData["DistrictId"] = new SelectList(_districtService.Query().ToList(), "Id", "Name");
            return View(hospital);
        }

        // POST: Hospitals/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public IActionResult Edit(HospitalModel hospital)
        {
            if (ModelState.IsValid)
            {
                Result result = _hospitalService.Update(hospital);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["CityId"] = new SelectList(_cityService.Query().ToList(), "Id", "Name");
            ViewData["DistrictId"] = new SelectList(_districtService.Query().ToList(), "Id", "Name");
            return View(hospital);
        }

        // GET: Hospitals/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            HospitalModel hospital = _hospitalService.Query().SingleOrDefault(h => h.Id == id);
            if (hospital == null)
            {
                return View("_Error", "Hospital not found!");
            }
            //return View(hospital);

            Result result = _hospitalService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
