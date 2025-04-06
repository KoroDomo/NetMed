
using Microsoft.AspNetCore.Mvc;
using NetMed.WebApi.Models.DoctorAvailability;
using WebApiApplication.Application.Interfaces;

namespace WebApiApplication.Controllers
{
    public class DoctorAvailabilityController : Controller
    {
        private readonly IDoctorAvailabilityService _doctorAvailabilityService;

        public DoctorAvailabilityController(IDoctorAvailabilityService doctorAvailabilityService)
        {
            _doctorAvailabilityService = doctorAvailabilityService;
        }
        public async Task<IActionResult> Index()
        {
            var doctor = await _doctorAvailabilityService.GetAllAsync();
            return View(doctor);
        }
        public async Task<IActionResult> Details(int id)
        {

           var doctor = await _doctorAvailabilityService.GetByIdAsync(id);
            return doctor == null ? View("Error") : View(doctor);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorAvailabilityModelSave doctorAvailabilityModel)
        {
            if (await _doctorAvailabilityService.SaveAsync(doctorAvailabilityModel))
                return RedirectToAction(nameof(Index));
            ViewBag.Message = "Error guardando la cita";
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var doctor = await _doctorAvailabilityService.GetByIdAsync(id);
            return doctor == null ? View("Error") : View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DoctorAvailabilityModelUpdate doctorAvailabilityModel)
        {
            if (await _doctorAvailabilityService.UpdateAsync(doctorAvailabilityModel))
                return RedirectToAction(nameof(Index));
            ViewBag.Message = "Error editando la cita";
            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _doctorAvailabilityService.GetByIdAsync(id);
            return doctor == null ? View("Error") : View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DoctorAvailabilityModelRemove doctorAvailabilityModel)
        {
            if (await _doctorAvailabilityService.RemoveAsync(doctorAvailabilityModel))
                return RedirectToAction(nameof(Index));
            ViewBag.Message = "Error eliminando la cita";
            return View();
        }
    }
}
