using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetMed.Web.Models;

namespace NetMed.Web.Controllers
{
    public class PatientController : Controller
    {
        // GET: PatientController
        public async Task<ActionResult> Index()
        {
            List<PatientModel> patients = new List<PatientModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261");
                var response = await client.GetAsync("api/Patients/GetPatients");
                if (response.IsSuccessStatusCode)
                {
                    var patientsResponse = await response.Content.ReadFromJsonAsync<List<PatientModel>>();
                    if (patientsResponse != null)
                    {
                        patients = patientsResponse;
                    }
                }
            }
            return View(patients);
        }

        // GET: PatientController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            PatientModel? patientModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261");
                var response = await client.GetAsync($"api/Patients/GetPatientsById/{id}");
                if (response.IsSuccessStatusCode)
                {
                    patientModel = await response.Content.ReadFromJsonAsync<PatientModel>();
                    if (patientModel == null)
                    {
                        return NotFound();
                    }
                }
            }
            return View(patientModel);
        }

        // GET: PatientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PatientModel patientModel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5261");
                    var response = await client.PostAsJsonAsync("api/Patients/SavePatients", patientModel);
                    if (response.IsSuccessStatusCode)
                    {
                        var res = await response.Content.ReadFromJsonAsync<OperationResult<DoctorModel>>();
                        if (res == null)
                        {
                            return NotFound();
                        }
                    }
                    return View(patientModel);
                }
            }
            catch
            {
                return View();
            }
        }




        // GET: PatientController/Edit/5
        public ActionResult Edit(int id)
        {
           PatientModel patientModel = new PatientModel();
            return View(patientModel);
        }

        // POST: PatientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PatientModel patientModel)
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

        // GET: PatientController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PatientController/Delete/5
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
