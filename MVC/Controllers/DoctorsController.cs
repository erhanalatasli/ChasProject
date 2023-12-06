#nullable disable
using Business.Models;
using Business.Services;
using Core.Results;
using Core.Results.Bases;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Settings;

//Generated from Custom Template.
namespace MVC.Controllers
{
	public class DoctorsController : Controller
	{
		// Add service injections here
		private readonly IDoctorService _doctorService;
		private readonly IHospitalService _hospitalService;
		private readonly IClinicService _clinicService;
		private readonly IGenderService _genderService;

		public DoctorsController(IDoctorService doctorService, IHospitalService hospitalService, IClinicService clinicService, IGenderService genderService)
		{
			_doctorService = doctorService;
			_hospitalService = hospitalService;
			_clinicService = clinicService;
			_genderService = genderService;
		}

		// GET: Doctors
		[Authorize(Roles = "Admin, Doctor, Nurse")]
		public IActionResult Index()
		{
			List<DoctorModel> doctorList = _doctorService.Query().ToList();
			return View(doctorList);
		}

        [Authorize(Roles = "Admin")]
        public IActionResult IndexJson()
		{
			var doctorList = _doctorService.Query().ToList();
			return Json(doctorList);
		}

        // GET: Doctors/Details/5
        [Authorize(Roles = "Admin, Doctor, Nurse")]
        public IActionResult Details(int id)
		{
			DoctorModel doctor = _doctorService.Query().SingleOrDefault(d => d.Id == id);
			if (doctor == null)
			{
				return View("_Error", "Doctor not found!");
			}
			return View(doctor);
		}

        // GET: Doctors/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
		{
			// Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
			ViewData["HospitalId"] = new SelectList(_hospitalService.Query().ToList(), "Id", "Name");
			ViewData["ClinicId"] = new SelectList(_clinicService.Query().ToList(), "Id", "Name");
			ViewData["GenderId"] = new SelectList(_genderService.Query().ToList(), "Id", "Name");
			return View();
		}

		// POST: Doctors/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(DoctorModel doctor, IFormFile image)
		{
			if (ModelState.IsValid)
			{
				Result result;

				result = UpdateImage(doctor, image);
				if (result.IsSuccessful)
				{
					result = _doctorService.Add(doctor);
					if (result.IsSuccessful)
					{
						TempData["Message"] = result.Message;
						return RedirectToAction(nameof(Index));
					}
				}
				ModelState.AddModelError("", result.Message);
			}
			// Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
			ViewData["HospitalId"] = new SelectList(_hospitalService.Query().ToList(), "Id", "Name");
			ViewData["ClinicId"] = new SelectList(_clinicService.Query().ToList(), "Id", "Name");
			ViewData["GenderId"] = new SelectList(_genderService.Query().ToList(), "Id", "Name");
			return View(doctor);
		}

		private Result UpdateImage(DoctorModel doctor, IFormFile image)
		{
			if (image is not null && image.Length > 0)
			{
				#region Dosya uzantı ve boyut validasyonları
				string fileName = image.FileName;
				string extension = Path.GetExtension(fileName);

				if (!AppSettings.AcceptedImageExtensions.Split(',').Any(e => e.ToLower().Trim() == extension.ToLower()))
				{
					return new ErrorResult("Image can't be uploaded because image extension is not in \"" + AppSettings.AcceptedImageExtensions + "\"!");
				}

				double acceptedFileLength = AppSettings.AcceptedImageLength;
				double acceptedFileLengthInBytes = acceptedFileLength * Math.Pow(1024, 2);

				if (image.Length > acceptedFileLengthInBytes)
				{
					return new ErrorResult("Image can't be uploaded because image file length is greater than " + acceptedFileLength.ToString("N1") + " MB!");
				}
				#endregion

				#region Model içerisindeki Image ve ImageExtension özellikleri güncellenmesi
				using (MemoryStream memoryStream = new MemoryStream())
				{
					image.CopyTo(memoryStream);
					doctor.Image = memoryStream.ToArray();
					doctor.ImageExtension = extension;
				}
				#endregion
			}

			return new SuccessResult();
		}

		// GET: Doctors/Edit/5
		[Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
		{
			DoctorModel doctor = _doctorService.Query().SingleOrDefault(d => d.Id == id);
			if (doctor == null)
			{
				return View("_Error", "Doctor not found!");
			}
			// Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
			ViewData["HospitalId"] = new SelectList(_hospitalService.Query().ToList(), "Id", "Name", doctor.HospitalId);
			ViewData["ClinicId"] = new SelectList(_clinicService.Query().ToList(), "Id", "Name", doctor.ClinicId);
			ViewData["GenderId"] = new SelectList(_genderService.Query().ToList(), "Id", "Name", doctor.GenderId);
			return View(doctor);
		}

		// POST: Doctors/Edit
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(DoctorModel doctor, IFormFile image)
		{
			if (ModelState.IsValid)
			{
				Result result;

				result = UpdateImage(doctor, image);
				if (result.IsSuccessful)
				{
					result = _doctorService.Update(doctor);
					if (result.IsSuccessful)
					{
						TempData["Message"] = result.Message;
						return RedirectToAction(nameof(Index));
					}
				}
				ModelState.AddModelError("", result.Message);
			}
			// Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
			ViewData["HospitalId"] = new SelectList(_hospitalService.Query().ToList(), "Id", "Name", doctor.HospitalId);
			ViewData["ClinicId"] = new SelectList(_clinicService.Query().ToList(), "Id", "Name", doctor.ClinicId);
			ViewData["GenderId"] = new SelectList(_genderService.Query().ToList(), "Id", "Name", doctor.GenderId);
			return View(doctor);
		}

        // GET: Doctors/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
		{
			DoctorModel doctor = _doctorService.Query().SingleOrDefault(d => d.Id == id);
			if (doctor == null)
			{
				return View("_Error", "Doctor not found!");
			}
            //return View(doctor);

            Result result = _doctorService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
