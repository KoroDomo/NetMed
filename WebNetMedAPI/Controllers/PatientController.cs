using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetMed.Web.Models;
using WebNetMedAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NetMed.Web.Controllers
{
    public class PatientController : Controller
    {
        // GET: PatientController
        public async Task<IActionResult> Index()
        {
            List<PatientModel> patients = new List<PatientModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261");
                var response = await client.GetAsync("api/Patients/GetPatients");
                if (response.IsSuccessStatusCode)
                {
                    var patientsResponse = await response.Content.ReadFromJsonAsync<OperationResultList<PatientModel>>();
                    if (patientsResponse != null)
                    {
                        patients = patientsResponse.data;
                    }
                }
            }
            return View(patients);
        }

        // GET: PatientController/Details/5
        public async Task<IActionResult> Details(int id)
        {
     
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261");
                var response = await client.GetAsync($"api/Patients/GetPatientsById/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OperationResult<PatientModel>>();
                    if (result == null)
                    {
                        return NotFound();
                    }
                    return View(result.data);
                }
            }
            return View();
        }

        // GET: PatientController/Create
        public async Task<IActionResult>Create()
        {
            return View();
        }

        // POST: PatientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientModel patientModel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5261");
                    var response = await client.PostAsJsonAsync("api/Patients/SavePatients", patientModel);
                    if (response.IsSuccessStatusCode)
                    {
                        var res = await response.Content.ReadFromJsonAsync<OperationResult<PatientModel>>();
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
        public async Task<IActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261");
                var response = await client.GetAsync($"api/Patients/GetPatientsById/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();

                    var datos = await response.Content.ReadFromJsonAsync<OperationResult<PatientModel>>();
                    if (datos == null)
                    {
                        return NotFound();
                    }
                        return View(datos.data); 
                }
            }
            return View();
        }
        
        

        // POST: PatientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Edit(int id, PatientModel patientModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261");
                var response = await client.PutAsJsonAsync($"/api/Patients/UpdatePatients/{id}", patientModel);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var datos = await response.Content.ReadFromJsonAsync<OperationResult<PatientModel>>();
                    if (datos != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }


                }
            }
            return View();

        }

        // GET: PatientController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            using (var client  = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261");
                var response = await client.GetAsync($"api/Patients/GetPatientsById/{id}");
                if(response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();

                    var dato = await response.Content.ReadFromJsonAsync<OperationResult<PatientModel>>();
                    if (dato == null)
                    {
                        return NotFound();
                    }
                    return View(dato.data);
                }
            }
            return View();
        }

        // POST: PatientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id, PatientModel patientModel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5261");
                    var response = await client.DeleteAsync($"api/Patients/DeletePatient/{Id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var dat = await response.Content.ReadFromJsonAsync<OperationResult<PatientModel>>();
                        if (dat != null)
                        {
                            return RedirectToAction(nameof(Index));
                        }


                        return View(dat);
                          
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
            return View(patientModel);
        }
    }
}
