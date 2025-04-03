using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos;
using NetMed.Domain.Entities;
using NetMed.Web.Models;
using WebNetMedAPI.Models;

namespace NetMed.Web.Controllers
{
    public class DoctorController : Controller

    {


        // GET: DoctorController
        public async Task<IActionResult> Index()
        {
            List<DoctorModel> doctors = new List<DoctorModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261");

                var response = await client.GetAsync("api/Doctors/GetDoctors");

                if (response.IsSuccessStatusCode)
                {
                    var operationResult = await response.Content.ReadFromJsonAsync<OperationResultList<DoctorModel>>();
                    if (operationResult != null && operationResult.Success)
                    {
                        doctors = operationResult.data;
                    }
                }
            }
            return View(doctors);
        }

        // GET: DoctorController/Details/5
        public async Task<IActionResult> Details(int id)
        {
      

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261");
                var response = await client.GetAsync($"api/Doctors/GetDoctorsById/{id}");

                if (response.IsSuccessStatusCode)
                {
                   var result = await response.Content.ReadFromJsonAsync<OperationResult<DoctorModel>>();
                    return View(result.data);
               
                }
            }
            return View();
        }

        // GET: DoctorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DoctorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorModel doctor)
        {

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5261");
                    var response = await client.PostAsJsonAsync("api/Doctors/SaveDoctor", doctor);
                    if (response.IsSuccessStatusCode)
                    {
                      var  data = await response.Content.ReadFromJsonAsync<OperationResult<DoctorModel>>();
                        if (data != null)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                return View(doctor);
            }
            catch
            {
                return View();
            }
        }

        // GET: DoctorController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
  
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261");
                var response = await client.GetAsync($"api/Doctors/GetDoctorsById/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    var dat = await response.Content.ReadFromJsonAsync<OperationResult<DoctorModel>>();
                    if (dat == null)
                    {
                        return NotFound();
                    }
                    return View(dat.data);
                }
            }
            return View();
        }

       // POST: DoctorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, DoctorModel doctorModel)
        {



            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261");
                var response = await client.PutAsJsonAsync($"/api/Doctors/UpdateDoctor/{Id}", doctorModel);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var data = await response.Content.ReadFromJsonAsync<OperationResult<DoctorModel>>();
                    if (data != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                }
            }            
                return View();
            
        }

        // GET: DoctorController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261");
                var response = await client.GetAsync($"api/Doctors/GetDoctorsById/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    var dat = await response.Content.ReadFromJsonAsync<OperationResult<DoctorModel>>();
                    if (dat == null)
                    {
                        return NotFound();
                    }
                    return View(dat.data);
                }
            }
            return View();
        }

        // POST: DoctorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id, DoctorModel doctorModel)
        {
           

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5261");
                    var response = await client.DeleteAsync($"api/Doctors/Delete/{Id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadFromJsonAsync<OperationResult<DoctorModel>>();
                        if (data != null)
                        {
                            // Optionally, you can check the success status of the operation
                            if (data.Success)
                            {
                                
                                return RedirectToAction(nameof(Index));
                            }
                            else
                            {
                                
                                ViewBag.Message = data.Message;
                            }
                        }
                     return View(data);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();


            }
            return View(doctorModel);
        }
    }
}

