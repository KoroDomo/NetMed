
using Microsoft.AspNetCore.Mvc;
using NetMed.WebApi.ConfiUrl;
using NetMed.WebApi.Models.Appointments;
using NetMed.WebApi.Models.OperationsResult;
using System.Text;
using System.Text.Json;

namespace NetMed.WebApi.Controllers
{
    public class AppointmentsController : Controller
    {
        public async Task<IActionResult> Index()
        {            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiConfig.GetBaseUrl());
                var response = await client.GetAsync("Appointments/GetAppointment");

                if (response.IsSuccessStatusCode)
                {
                    var datos = await response.Content.ReadFromJsonAsync<OperationResultTypeList<AppointmentsModel>>();
                    return View(datos.data);                    
                }
                else
                {
                    ViewBag.Message = "Error obteniendo las Citas";
                    return View();
                }
            }            
        }
        public async Task<IActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiConfig.GetBaseUrl());
                var response = await client.GetAsync($"Appointments/GetAppointmentById?id={id}");

                string apiResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OperationResult<AppointmentsModel>>();
                    return View(result.data);
                }
                else
                {
                    ViewBag.Message = "Error obteniendo la Cita";
                    return View();
                }
            }  
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentsModelSave appointmentsModel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiConfig.GetBaseUrl());

                    var response = await client.PostAsJsonAsync($"Appointments/SaveAppointement",appointmentsModel);

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        await response.Content.ReadFromJsonAsync<OperationResult<AppointmentsModel>>();
                    }
                    else
                    {
                        ViewBag.Message = "Error Guardando las Cita";
                        return View();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiConfig.GetBaseUrl());
                var response = await client.GetAsync($"Appointments/GetAppointmentById?id={id}");

                string apiResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OperationResult<AppointmentsModel>>();
                    return View(result.data);
                }
                else
                {
                    ViewBag.Message = "Error obteniendo la Cita";
                    return View();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppointmentsModelUpdate appointmentsModel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiConfig.GetBaseUrl());

                    var response = await client.PutAsJsonAsync($"Appointments/UpdateAppointment", appointmentsModel);

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        await response.Content.ReadFromJsonAsync<OperationResult<AppointmentsModel>>();
                    }
                    else
                    {
                        ViewBag.Message = "Error editando la Cita";
                        return View();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiConfig.GetBaseUrl());
                var response = await client.GetAsync($"Appointments/GetAppointmentById?id={id}");

                string apiResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OperationResult<AppointmentsModel>>();
                    return View(result.data);
                }
                else
                {
                    ViewBag.Message = "Error obteniendo la Cita";
                    return View();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Delete(int id,AppointmentsModelRemove appointmentsModel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiConfig.GetBaseUrl());

                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(client.BaseAddress, "Appointments/RemoveAppointment"),
                        Content = new StringContent(JsonSerializer.Serialize(appointmentsModel), Encoding.UTF8, "application/json")
                    };

                    var response = await client.SendAsync(request);                 

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        await response.Content.ReadFromJsonAsync<OperationResult<AppointmentsModelRemove>>();
                    }
                    else
                    {
                        ViewBag.Message = "Error Eliminando la Cita";
                        return View();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
