using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetMed.Web.Models.Appointments;

namespace NetMed.Web.Controllers
{
    public class AppointmentsController : Controller
    {
        // GET: AppointmentsController
        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                List<AppointmentsModel> appointments = new List<AppointmentsModel>();
                client.BaseAddress = new Uri("http://localhost:5135/api/");
                
                var response = await client.GetAsync("Appointments/GetAppointment");

                if (response.IsSuccessStatusCode)  
                {
                    var data = await response.Content.ReadFromJsonAsync<AppointmentsModel>();
                }
            }
            return View();
        }

        // GET: AppointmentsController/Details/5
        public ActionResult Details(int id)
        {
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

        // GET: AppointmentsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AppointmentsController/Edit/5
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

        // GET: AppointmentsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AppointmentsController/Delete/5
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
