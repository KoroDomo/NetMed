using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos;
using NetMed.Application.Dtos.Doctors;
using NetMed.Domain.Entities;
using NetMed.Web.Models;
using NetMed.WebApi.Models;

namespace NetMed.Web.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorsServices _doctorsServices;

        //public DoctorController(IDoctorsServices doctorsServices)
        //{
        //    _doctorsServices = doctorsServices;
        //}


        // GET: DoctorControllerWeb
        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261/api/");

                var response = await client.GetAsync("Doctors/GetDoctors");
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var doctorsResponse = await response.Content.ReadFromJsonAsync<OperationResultList<DoctorModel>>();
                    return View(doctorsResponse.data);
                }
            }
            return View();
        }

        // GET: DoctorControllerWeb/Details/5
     
        public async Task<IActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261/api/");
                var response = await client.GetAsync($"Doctors/GetDoctorsById?={id}");
                
                string apiResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine(apiResponse);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var result = await response.Content.ReadFromJsonAsync<OperationResult<DoctorModel>>();
                        return View(result.data);
                    }
                    catch 
                    {
                        Console.WriteLine($"Deserialization error");
                        ViewBag.Message = "Error deserializing response.";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = "Error obteniendo Doctor";
                    return View();
                }
            }
        }



        // GET: DoctorControllerWeb/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DoctorControllerWeb/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddDoctorsDto addDoctorDto)
        {
            try
            {
                _ = await _doctorsServices.Add(addDoctorDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        ////        // GET: DoctorControllerWeb/Edit/5
        //public async Task<IActionResult> Edit(int id)
        //{
           
        //        var result = await _doctorsServices.GetById(id);
        //        if (result.Success) 
        //    {
        //            DoctorsDto doctorsDto = (DoctorsDto doctorsDto)result.data;
        //            return View(doctorsDto);
        //        }
        // return  View();
        //}

        // POST: DoctorControllerWeb/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit(int id, DoctorModel doctorModel)
        //{
        //    OperationResult? operationResult = null;
        //    try
        //    {
        //        using (var client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri("http://localhost:5261");
        //            var response = await client.PutAsJsonAsync($"api/Doctors/SaveDoctor/{id}", operationResult);
        //            if (response.IsSuccessStatusCode)
        //            {
        //                operationResult = await response.Content.ReadFromJsonAsync<OperationResult?>();
        //                if (operationResult != null)
        //                {
        //                    return RedirectToAction(nameof(Index));
        //                }
        //            }
        //        }
        //        return View(operationResult);
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //        // GET: DoctorControllerWeb/Delete/5
        //        public ActionResult Delete(int id)
        //        {
        //            DoctorModel? doctorModel = null;
        //            try
        //            {
        //                using (var client = new HttpClient())
        //                {
        //                    client.BaseAddress = new Uri("http://localhost:5261");
        //                    var response = client.DeleteAsync($"api/Doctors/Delete/{id}");
        //                    if (response.IsCompleted)
        //                    {
        //                        return RedirectToAction(nameof(Index));
        //                    }
        //                }
        //                return View(doctorModel);
        //            }
        //            catch
        //            {

        //                return View();
        //            }
        //        }

        //        // POST: DoctorControllerWeb/Delete/5
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public async Task<IActionResult> Delete(int id, DoctorModel doctorModel)
        //        {
        //            OperationResult? operationResult = null;

        //            try
        //            {
        //                using (var client = new HttpClient())
        //                {
        //                    client.BaseAddress = new Uri("http://localhost:5261");
        //                    var response = await client.DeleteAsync($"api/Doctors/Delete/{id}");
        //                    if (response.IsSuccessStatusCode)
        //                    {
        //                        operationResult = await response.Content.ReadFromJsonAsync<OperationResult?>();
        //                        if (operationResult != null && operationResult.Success)
        //                        {
        //                            return RedirectToAction(nameof(Index));
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                ViewBag.Message = ex.Message;
        //                return View();


        //            }
        //            return View(doctorModel);
        //        }
        //    }
    }
}
