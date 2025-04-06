
using Microsoft.AspNetCore.Mvc;
using NetMed.WebApi.Models.Appointments;
using WebApiApplication.Application.Interfaces;

namespace WebApiApplication.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentsService _appointmentsService;

        public AppointmentsController(IAppointmentsService appointmentsService)
        {
            _appointmentsService = appointmentsService;
        }
        public async Task<IActionResult> Index()
        {
            var Appointments = await _appointmentsService.GetAllAsync();
            return View(Appointments);
        }
        public async Task<IActionResult> Details(int id)
        {
            var Appointments = await _appointmentsService.GetByIdAsync(id);
            return Appointments == null ? View("Error") : View(Appointments);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentsModelSave appointmentsModel)
        {
            if (await _appointmentsService.SaveAsync(appointmentsModel))
                return RedirectToAction(nameof(Index));
            ViewBag.Message = "Error guardando la cita";
            return View();
        }
            
        
        public async Task<IActionResult> Edit(int id)
        {
            var Appointments = await _appointmentsService.GetByIdAsync(id);
            return Appointments == null ? View("Error") : View(Appointments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppointmentsModelUpdate appointmentsModel)
        {
            if (await _appointmentsService.UpdateAsync(appointmentsModel))
                return RedirectToAction(nameof(Index));
            ViewBag.Message = "Error editando la cita";
            return View();

        }
        public async Task<IActionResult> Delete(int id)
        {

            var Appointments = await _appointmentsService.GetByIdAsync(id);
            return Appointments == null ? View("Error") : View(Appointments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Delete(int id,AppointmentsModelRemove appointmentsModel)
        {
            if (await _appointmentsService.RemoveAsync(appointmentsModel))
                return RedirectToAction(nameof(Index));
            ViewBag.Message = "Error eliminando la cita";
            return View();
        }
    }
}
