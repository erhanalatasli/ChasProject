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
    public class AppointmentsController : Controller
    {
        // Add service injections here
        private readonly IAppointmentService _appointmentService;
        private readonly IUserService _userService;
        private readonly IHospitalService _hospitalService;
        private readonly IClinicService _clinicService;
        private readonly IDoctorService _doctorService;

        public AppointmentsController(IAppointmentService appointmentService, IUserService userService, IHospitalService hospitalService, IClinicService clinicService, IDoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _userService = userService;
            _hospitalService = hospitalService;
            _clinicService = clinicService;
            _doctorService = doctorService;
        }

        // GET: Appointments
        [Authorize(Roles = "Admin, Doctor, Nurse, Patient")]

        public IActionResult Index()
        {
            List<AppointmentModel> appointmentList = _appointmentService.Query().ToList();
            return View(appointmentList);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult IndexJson()
        {
            var appointmentList = _appointmentService.Query().ToList();
            return Json(appointmentList);
        }

        // GET: Appointments/Details/5
        [Authorize(Roles = "Admin, Doctor, Nurse")]
        public IActionResult Details(int id)
        {
            AppointmentModel appointment = _appointmentService.Query().SingleOrDefault(a => a.Id == id);
            if (appointment == null)
            {
                return View("_Error", "Appointment not found");
            }
            return View(appointment);
        }

        [Authorize]
        public IActionResult GetDoctorsByClinic(int clinicId)
		{
			var doctors = _doctorService.GetListByClinic(clinicId);
			return Json(doctors);
		}

        // GET: Appointments/Create
        [Authorize(Roles = "Admin, Doctor, Nurse, Patient")]
        public IActionResult Create()
        {
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            var loggedInUserName = User.Identity.Name;
            var user = _userService.GetUserByUserName(loggedInUserName);

            ViewData["ClinicId"] = new SelectList(_clinicService.Query().ToList(), "Id", "Name");
            ViewData["DoctorId"] = new SelectList(_doctorService.Query().ToList(), "Id", "NameSurnameAppointment");
            ViewData["HospitalId"] = new SelectList(_hospitalService.Query().ToList(), "Id", "Name");
            ViewData["UserId"] = new SelectList(new List<UserModel> { user }, "Id", "NameSurname");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Doctor, Nurse, Patient")]
        public IActionResult Create(AppointmentModel appointment)
        {
            if (ModelState.IsValid)
            {
                Result result = _appointmentService.Add(appointment);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            var loggedInUserName = User.Identity.Name;
            var user = _userService.GetUserByUserName(loggedInUserName);

            ViewData["ClinicId"] = new SelectList(_clinicService.Query().ToList(), "Id", "Name");
            ViewData["DoctorId"] = new SelectList(_doctorService.Query().ToList(), "Id", "NameSurnameAppointment");
            ViewData["HospitalId"] = new SelectList(_hospitalService.Query().ToList(), "Id", "Name");
            ViewData["UserId"] = new SelectList(new List<UserModel> { user }, "Id", "NameSurname");
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        [Authorize(Roles = "Admin, Nurse")]
        public IActionResult Edit(int id)
        {
            AppointmentModel appointment = _appointmentService.Query().SingleOrDefault(a => a.Id == id);
            if (appointment == null)
            {
                return View("_Error", "Appointment not found");
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items

            ViewData["ClinicId"] = new SelectList(_clinicService.Query().ToList(), "Id", "Name", appointment.ClinicId);
            ViewData["DoctorId"] = new SelectList(_doctorService.Query().ToList(), "Id", "NameSurnameAppointment", appointment.DoctorId);
            ViewData["HospitalId"] = new SelectList(_hospitalService.Query().ToList(), "Id", "Name", appointment.HospitalId);
			ViewData["UserId"] = new SelectList(_userService.Query().ToList(), "Id", "NameSurname", appointment.UserId);
			return View(appointment);
        }

        // POST: Appointments/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Nurse")]
        public IActionResult Edit(AppointmentModel appointment)
        {
            if (ModelState.IsValid)
            {
                Result result = _appointmentService.Update(appointment);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            
            ViewData["ClinicId"] = new SelectList(_clinicService.Query().ToList(), "Id", "Name", appointment.ClinicId);
            ViewData["DoctorId"] = new SelectList(_doctorService.Query().ToList(), "Id", "NameSurnameAppointment", appointment.DoctorId);
            ViewData["HospitalId"] = new SelectList(_hospitalService.Query().ToList(), "Id", "Name", appointment.HospitalId);
            ViewData["UserId"] = new SelectList(_userService.Query().ToList(), "Id", "NameSurname", appointment.UserId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        [Authorize(Roles = "Admin, Nurse")]
        public IActionResult Delete(int id)
        {
            AppointmentModel appointment = _appointmentService.Query().SingleOrDefault(a => a.Id == id);
            if (appointment == null)
            {
                return View("_Error", "Appointment not found!");
            }
            //return View(appointment);

            Result result = _appointmentService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
