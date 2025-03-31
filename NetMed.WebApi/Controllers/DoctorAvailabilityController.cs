using NetMed.WebApi.Models.OperationsResult;
using Microsoft.AspNetCore.Mvc;
using NetMed.WebApi.Models.DoctorAvailability;

namespace NetMed.WebApi.Controllers
{
    public class DoctorAvailabilityController : Controller
    {
        // GET: DoctorAvailabilityController
        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5135/api/");

                var response = await client.GetAsync("DoctorAvailability/GetDoctorAvailability");

                if (response.IsSuccessStatusCode)
                {
                    var datos = await response.Content.ReadFromJsonAsync<OperationResultTypeList<DoctorAvailabilityModel>>();
                    return View(datos.data);
                }
                else
                {
                    ViewBag.Message = "Error obteniendo las Citas";
                    return View();
                }
            }
        }

        // GET: DoctorAvailabilityController/Details/5
        public async Task<IActionResult> Details(int id)
        {

            using (var client = new HttpClient())
            {

            };
            return View();
        }

        // GET: DoctorAvailabilityController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DoctorAvailabilityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DoctorAvailabilityController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DoctorAvailabilityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DoctorAvailabilityController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DoctorAvailabilityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
