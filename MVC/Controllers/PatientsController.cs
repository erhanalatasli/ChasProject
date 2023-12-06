#nullable disable
using Business.Models;
using Business.Services;
using Core.Results;
using Core.Results.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Settings;

//Generated from Custom Template.
namespace MVC.Controllers
{
	public class PatientsController : Controller
	{
		// Add service injections here
		private readonly IPatientService _patientService;
		private readonly IDoctorService _doctorService;
		private readonly IGenderService _genderService;
		private readonly ICityService _cityService;
		private readonly IClinicService _clinicService;

		public PatientsController(IPatientService patientService, IDoctorService doctorService, IGenderService genderService, ICityService cityService, IClinicService clinicService)
		{
			_patientService = patientService;
			_doctorService = doctorService;
			_genderService = genderService;
			_cityService = cityService;
			_clinicService = clinicService;
		}

        // GET: Patients
        [Authorize(Roles = "Admin, Doctor, Nurse")]
        public IActionResult Index()
		{
			List<PatientModel> patientList = _patientService.Query().ToList();
			return View(patientList);
		}

        [Authorize(Roles = "Admin")]
        public IActionResult IndexJson()
        {
            var patientList = _patientService.Query().ToList();
            return Json(patientList);
        }

        // GET: Patients/Details/5
        [Authorize(Roles = "Admin, Doctor, Nurse")]
        public IActionResult Details(int id)
		{
			PatientModel patient = _patientService.Query().SingleOrDefault(p => p.Id == id);
			if (patient == null)
			{
				return View("_Error", "Patient not found!");
			}
			return View(patient);
		}

        [Authorize]
        public IActionResult GetDoctorsByClinic(int clinicId)
		{
			var doctors = _doctorService.GetListByClinic(clinicId);
			return Json(doctors);
		}

        // GET: Patients/Create
        [Authorize(Roles = "Admin, Nurse")]
        public IActionResult Create()
		{
			ViewData["DoctorId"] = new MultiSelectList(_doctorService.Query().ToList(), "Id", "NameSurname");
			ViewData["CityId"] = new SelectList(_cityService.Query().ToList(), "Id", "Name");
			ViewData["GenderId"] = new SelectList(_genderService.Query().ToList(), "Id", "Name");
			ViewData["ClinicId"] = new SelectList(_clinicService.Query().ToList(), "Id", "Name");
			return View();
		}


		// POST: Patients/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Nurse")]
        public IActionResult Create(PatientModel patient, IFormFile image)
		{
			if (ModelState.IsValid)
			{
				Result result;

				result = UpdateImage(patient, image);
				if (result.IsSuccessful)
				{
					result = _patientService.Add(patient);
					if (result.IsSuccessful)
					{
                        TempData["Message"] = result.Message;
                        return RedirectToAction(nameof(Index));
                    }					
				}
				ModelState.AddModelError("", result.Message);
			}
			ViewData["DoctorId"] = new MultiSelectList(_doctorService.Query().ToList(), "Id", "NameSurname");
			ViewData["CityId"] = new SelectList(_cityService.Query().ToList(), "Id", "Name");
			ViewData["GenderId"] = new SelectList(_genderService.Query().ToList(), "Id", "Name");
			ViewData["ClinicId"] = new SelectList(_clinicService.Query().ToList(), "Id", "Name");
			return View(patient);
		}

        private Result UpdateImage(PatientModel patient, IFormFile image)
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
                    patient.Image = memoryStream.ToArray();
                    patient.ImageExtension = extension;
                }
                #endregion
            }

            return new SuccessResult();
        }

        // GET: Patients/Edit/5
        [Authorize(Roles = "Admin, Nurse")]
        public IActionResult Edit(int id)
		{
			PatientModel patient = _patientService.Query().SingleOrDefault(p => p.Id == id);
			if (patient == null)
			{
				return View("_Error", "Patient not found!");
			}
			ViewData["DoctorId"] = new MultiSelectList(_doctorService.Query().ToList(), "Id", "NameSurname", patient.DoctorIds);
			ViewData["CityId"] = new SelectList(_cityService.Query().ToList(), "Id", "Name", patient.CityId);
			ViewData["GenderId"] = new SelectList(_genderService.Query().ToList(), "Id", "Name", patient.GenderId);
			ViewData["ClinicId"] = new SelectList(_clinicService.Query().ToList(), "Id", "Name", patient.ClinicId);
			return View(patient);
		}

		// POST: Patients/Edit
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Nurse")]
        public IActionResult Edit(PatientModel patient, IFormFile image)
		{
			if (ModelState.IsValid)
			{
				Result result = UpdateImage(patient, image);

				if (result.IsSuccessful)
				{
					result = _patientService.Update(patient);
					if (result.IsSuccessful)
					{
                        TempData["Message"] = result.Message;
                        return RedirectToAction(nameof(Index));
                    }					
				}
				ModelState.AddModelError("", result.Message);
			}
			ViewData["DoctorId"] = new MultiSelectList(_doctorService.Query().ToList(), "Id", "NameSurname", patient.DoctorIds);
			ViewData["CityId"] = new SelectList(_cityService.Query().ToList(), "Id", "Name", patient.CityId);
			ViewData["GenderId"] = new SelectList(_genderService.Query().ToList(), "Id", "Name", patient.GenderId);
			ViewData["ClinicId"] = new SelectList(_clinicService.Query().ToList(), "Id", "Name", patient.ClinicId);
			return View(patient);
		}

        // GET: Patients/Delete/5
        [Authorize(Roles = "Admin, Nurse")]
        public IActionResult Delete(int id)
		{
			PatientModel patient = _patientService.Query().SingleOrDefault(p => p.Id == id);
			if (patient == null)
			{
				return View("_Error", "Patient not found");
			}
            //return View(patient);

            Result result = _patientService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
