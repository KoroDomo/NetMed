
using Microsoft.AspNetCore.Mvc;
using NetMed.Application.Dtos.Appointments;
using NetMed.Application.Interfaces;


namespace NetMed.WebServices.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentsService _appointmentService;

        public AppointmentsController(IAppointmentsService appointmentService)
        {
            _appointmentService = appointmentService;      
        }
        // GET: AppointmentsController
        public async Task<IActionResult> Index()
        {
            var result = await _appointmentService.GetAll();

            if (result.success)
            {              
                return View(result.data);               
            }
            return View();
        }

        // GET: AppointmentsController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = await _appointmentService.GetById(id);
            if (result.success)
            {
                return View(result.data);
            }
            return View();
        }

        // GET: AppointmentsController/Create
        public ActionResult Create()
        {
            return View();        
        }

        // POST: AppointmentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveAppointmentsDto saveAppointmentsDto)
        {
            try
            {
                var result = await _appointmentService.Save(saveAppointmentsDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppointmentsController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _appointmentService.GetById(id);
            if (result.success)
            {
                return View(result.data);
            }
            return View();
        }

        // POST: AppointmentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateAppointmentsDto updateAppointmentsDto)
        {
            try
            { 
                await _appointmentService.Update(updateAppointmentsDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppointmentsController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _appointmentService.GetById(id);
            if (result.success)
            {
                return View(result.data);
            }
            return View();
        }

        // POST: AppointmentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RemoveAppointmentsDto removeAppointmentsDto)
        {
            try
            {
                await _appointmentService.Remove(removeAppointmentsDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
