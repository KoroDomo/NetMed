using Microsoft.AspNetCore.Mvc;
using WebApplicationRefactor.Application.Contracts;
using WebApplicationRefactor.Models.Doctors;

namespace WebApplicationRefactor.Controllers
{
    public class DoctorWebController : Controller
    {
        private readonly IDoctorServices _doctorServices;

        public DoctorWebController(IDoctorServices doctorServices)
        {
            _doctorServices = doctorServices;
        }

        // GET: DoctorWbController
        public async Task<IActionResult> Index()
        {
            var result = await _doctorServices.GetAllData();
            if (result.Success)
            {
                return View(result.data);
            }
            ViewBag.Message = result.message;
            return View("Error");
        }

        // GET: DoctorWbController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = await _doctorServices.GetById(id);
            if (result.Success)
            {
                return View(result.data);
            }
            ViewBag.Message = result.message;
            return View("Error");
        }

        // GET: DoctorWbController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DoctorWbController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorsApiModel doctor)
        {
            var result = await _doctorServices.Add(doctor);
            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Message = result.message;
            return View(doctor);
        }

        // GET: DoctorWbController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _doctorServices.GetById(id);
            if (result.Success)
            {
                return View(result.data);
            }
            ViewBag.Message = result.message;
            return View("Error");
        }

        // POST: DoctorWbController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DoctorsApiModel doctor)
        {
            var result = await _doctorServices.Update(doctor);
            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Message = result.message;
            return View(doctor);
        }

        // GET: DoctorWbController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _doctorServices.GetById(id);
            if (result.Success)
            {
                return View(result.data);
            }
            ViewBag.Message = result.message;
            return View("Error");
        }

        // POST: DoctorWbController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _doctorServices.Delete(new DoctorsApiModel { Id = id });
            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Message = result.message;
            return View();
        }
    }
}
