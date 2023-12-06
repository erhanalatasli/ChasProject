using Business.Models.Report;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Areas.Report.Models;

namespace MVC.Areas.Report.Controllers
{
	[Area("Report")]
	[Authorize(Roles = "Admin")]
	public class HomeController : Controller
	{
		private readonly IReportService _reportService;
		private readonly IUserService _userService;
		private readonly IHospitalService _hospitalService;
		private readonly IClinicService _clinicService;
		private readonly IDoctorService _doctorService;

        public HomeController(IReportService reportService, IUserService userService, IHospitalService hospitalService, IClinicService clinicService, IDoctorService doctorService)
        {
            _reportService = reportService;
            _userService = userService;
            _hospitalService = hospitalService;
            _clinicService = clinicService;
            _doctorService = doctorService;
        }

        // GET: HomeController
        public ActionResult Index()
		{
            var model = _reportService.GetList();
            var viewModel = new HomeIndexViewModel()
            {
                Report = model,
                UserSelectList = new SelectList(_userService.Query().ToList(), "Id", "NameSurname"),
                HospitalSelectList = new SelectList(_hospitalService.Query().ToList(), "Id", "Name"),
                ClinicSelectList = new SelectList(_clinicService.Query().ToList(), "Id", "Name"),
                DoctorSelectList = new SelectList(_doctorService.Query().ToList(), "Id", "NameSurnameAppointment")
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(FilterModel filter)
		{
            var model = _reportService.GetList(filter);
            return PartialView("_Report", model);
        }
	}
}
