using NetMed.WebApi.Models.OperationsResult;
using Microsoft.AspNetCore.Mvc;
using NetMed.WebApi.Models.DoctorAvailability;
using System.Text;
using System.Text.Json;

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
                    ViewBag.Message = "Error obteniendo las disponibilidades de los doctores";
                    return View();
                }
            }
        }
        // GET: DoctorAvailabilityController/Details/5
        public async Task<IActionResult> Details(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5135/api/");

                var response = await client.GetAsync($"DoctorAvailability/GetAvailabilityById?id={id}");

                string apiResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var datos = await response.Content.ReadFromJsonAsync<OperationResult<DoctorAvailabilityModel>>();
                    return View(datos.data);
                }
                else
                {
                    ViewBag.Message = "Error al obtener los datos del doctor";
                    return View();
                }
            }           
        }

        // GET: DoctorAvailabilityController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DoctorAvailabilityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorAvailabilityModel doctorAvailabilityModel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5135/api/");

                    var response = await client.PostAsJsonAsync($"DoctorAvailability/SaveDoctorAvailability", doctorAvailabilityModel);

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        await response.Content.ReadFromJsonAsync<OperationResult<DoctorAvailabilityModelSave>>();
                    }
                    else
                    {
                        ViewBag.Message = "Error Guardando la disponibilidad";
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

        // GET: DoctorAvailabilityController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5135/api/");

                var response = await client.GetAsync($"DoctorAvailability/GetAvailabilityById?id={id}");

                string apiResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var datos = await response.Content.ReadFromJsonAsync<OperationResult<DoctorAvailabilityModel>>();
                    return View(datos.data);
                }
                else
                {
                    ViewBag.Message = "Error obteniendo los datos del doctor";
                    return View();
                }
            }
        }

        // POST: DoctorAvailabilityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DoctorAvailabilityModelUpdate doctorAvailabilityModel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5135/api/");
                    var response = await client.PutAsJsonAsync("DoctorAvailability/UpdateDoctorAvailability", doctorAvailabilityModel);

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        await response.Content.ReadFromJsonAsync<OperationResult<DoctorAvailabilityModel>>();
                    }
                    else
                    {
                        ViewBag.Message = "Error editando la disponibilidad del doctor";
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

        // GET: DoctorAvailabilityController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5135/api/");

                var response = await client.GetAsync($"DoctorAvailability/GetAvailabilityById?id={id}");

                string apiResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var datos = await response.Content.ReadFromJsonAsync<OperationResult<DoctorAvailabilityModel>>();
                    return View(datos.data);
                }
                else
                {
                    ViewBag.Message = "Error obteniendo los datos del doctor";
                    return View();
                }

            }
        }

        // POST: DoctorAvailabilityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DoctorAvailabilityModelRemove doctorAvailabilityModel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5135/api/");

                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(client.BaseAddress, "DoctorAvailability/RemoveDoctorAvailability"),
                        Content = new StringContent(JsonSerializer.Serialize(doctorAvailabilityModel), Encoding.UTF8, "application/json")
                    };

                    var response = await client.SendAsync(request);

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        await response.Content.ReadFromJsonAsync<OperationResult<DoctorAvailabilityModel>>();
                    }
                    else
                    {
                        ViewBag.Message = "Error Eliminando la disponibilidad del doctor";
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
