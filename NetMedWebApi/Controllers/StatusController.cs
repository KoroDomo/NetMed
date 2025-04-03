using Microsoft.AspNetCore.Mvc;
using NetMedWebApi.Models;
using NetMedWebApi.Models.Status;
using System.Text;
using System.Text.Json;

namespace NetMedWebApi.Controllers
{
    public class StatusController : Controller
    {
        // GET: StatusController1
        public async Task<IActionResult> Index()
        {
            List<StatusApiModel> status = new List<StatusApiModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(" http://localhost:5261/api/");
                var reponse = await client.GetAsync("Status/GetAllStatus");

                if (reponse.IsSuccessStatusCode)
                {
                    var data = await reponse.Content.ReadFromJsonAsync<OperationResultList<StatusApiModel>>();
                    return View(data.Data);

                }

            }
            return View();
        }


        // GET: StatusController1/Details/5
        public async Task<IActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261/api/");
                var response = await client.GetAsync($"Status/GetStatusById?ID={id}");

                string apiResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OperationResultSingle<StatusApiModel>>();
                    return View(result.Data);
                }
                else
                {
                    ViewBag.Message = "Error obteniendo la Cita";
                    return View();

                }

            }
        }

        // GET: StatusController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatusController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveStatusModel saveStatus)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5261/api/");
                    var response = await client.PostAsJsonAsync("Status/CreateStatus", saveStatus);

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        await response.Content.ReadFromJsonAsync<OperationResultList<SaveStatusModel>>();
                        return RedirectToAction(nameof(Index));

                    }
                    else
                    {
                        ViewBag.Message = "Error al guardar los roles";
                        return View();
                    }

                }
            }
            catch (Exception ex)
            {

                ViewBag.Message = "Error al guardar los roles";
                return View();
            }
        }



        // GET: StatusController1/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261/api/");
                var response = await client.GetAsync($"Status/GetStatusById?ID={id}");

                string apiResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OperationResultSingle<UpdateStatusModel>>();
                    return View(result.Data);
                }
                else
                {
                    ViewBag.Message = "Error obteniendo la Cita";
                    return View();

                }

            }
        }


        // POST: StatusController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateStatusModel updateStatus)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5261/api/");
                    var response = await client.PutAsJsonAsync("Status/UpdateStatus", updateStatus);

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        await response.Content.ReadFromJsonAsync<OperationResultSingle<UpdateStatusModel>>();
                        return RedirectToAction(nameof(Index));

                    }
                    else
                    {
                        ViewBag.Message = "Error al guardar los roles";
                        return View();
                    }

                }
            }
            catch (Exception ex)
            {

                ViewBag.Message = "Error al guardar los roles";
                return View();
            }
        }

        // GET: StatusController1/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261/api/");
                var response = await client.GetAsync($"Status/GetStatusById?ID={id}");

                string apiResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OperationResultSingle<DeleteStatusModel>>();
                    return View(result.Data);
                }
                else
                {
                    ViewBag.Message = "Error obteniendo la Cita";
                    return View();

                }

            }
        }

        // POST: StatusController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id,DeleteStatusModel deleteStatusModel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(" http://localhost:5261/api/");

                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(client.BaseAddress, "Status/DeleteStatus"),
                        Content = new StringContent(JsonSerializer.Serialize(deleteStatusModel), Encoding.UTF8, "application/json")
                    };

                    var response = await client.SendAsync(request);

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        await response.Content.ReadFromJsonAsync<OperationResultList<DeleteStatusModel>>();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Message = "Error Eliminando la Cita";
                        return View();
                    }
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
