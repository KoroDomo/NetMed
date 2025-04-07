using Microsoft.AspNetCore.Mvc;
using WebApplicationRefactor.Models.Patients;
using WebApplicationRefactor.ServicesApi.Interface;

namespace WebApplicationRefactor.Controllers
{
    public class PatientsWebController : Controller
    {
        private readonly IPatientsService _patientsService;

        public PatientsWebController(IPatientsService patientsService)
        {
            _patientsService = patientsService;
        }

        // GET: PatientsWebController
        public async Task<IActionResult> Index()
        {
            var result = await _patientsService.GetAllData();
            if (result.Success)
            {
                return View(result.data);
            }
            ViewBag.Message = result.Message;
            return View("Error");
        }

        // GET: PatientsWebController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = await _patientsService.GetById(id);
            if (result.Success)
            {
                return View(result.data);
            }
            ViewBag.Message = result.Message;
            return View("Error");
        }

        // GET: PatientsWebController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PatientsWebController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientsApiModel patient)
        {
            var result = await _patientsService.Add(patient);
            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Message = result.Message;
            return View(patient);
        }

        // GET: PatientsWebController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _patientsService.GetById(id);
            if (result.Success)
            {
                return View(result.data);
            }
            ViewBag.Message = result.Message;
            return View("Error");
        }

        // POST: PatientsWebController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PatientsApiModel patient)
        {
            var result = await _patientsService.Update(patient);
            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Message = result.Message;
            return View(patient);
        }

        // GET: PatientsWebController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _patientsService.GetById(id);
            if (result.Success)
            {
                return View(result.data);
            }
            ViewBag.Message = result.Message;
            return View("Error");
        }

        // POST: PatientsWebController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _patientsService.Delete(new PatientsApiModel { Id = id });
            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Message = result.Message;
            return View();
        }
    }
}