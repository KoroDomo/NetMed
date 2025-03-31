using Microsoft.AspNetCore.Mvc;
using NetMed.Web1.Models;
using System.Text.Json;
using System.Text;
using NetMed.Web1.Models.NetworkType;

namespace NetMed.Web1.Controllers
{
    public class NetworkTypeWebController : Controller
    {
        // GET: NetworkTypeWebController
        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7099/api/");

                var response = await client.GetAsync("NetworkTypeApi/GetNetworkTypeRepositorys");

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var data = await response.Content.ReadFromJsonAsync<ListOperationResult<GetNetworkTypeModel>>(options);
                    return View(data.Result);

                }
                else
                {
                    ViewBag.Message = "Error obteniendo los Network Type";
                    return View();
                }
            }
        }

        // GET: NetworkTypeWebController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7099/api/");

                var response = await client.GetAsync($"NetworkTypeApi/GetNetworkTypeRepositoryBy{id}");

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var data = await response.Content.ReadFromJsonAsync<OperationResult<GetNetworkTypeModel>>(options);

                    return View(data.Result);
                }
                else
                {
                    ViewBag.Message = "Error obteniendo el Network Type";
                    return View();
                }
            }
        }

        // GET: NetworkTypeWebController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NetworkTypeWebController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveNetworkTypeModel insuranceProvider)
        {
            try
            {
                //Hay que validar los datos
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7099/api/");
                    insuranceProvider.changeDate = DateTime.Now;

                    var response = await client.PostAsJsonAsync("NetworkTypeApi/SaveNetworkTypeRepository", insuranceProvider);

                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        var data = await response.Content.ReadFromJsonAsync<OperationResult<SaveNetworkTypeModel>>(options);

                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Message = "Error guardando el Network Type";
                        return View();
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: NetworkTypeWebController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7099/api/");

                var response = await client.GetAsync($"NetworkTypeApi/GetNetworkTypeRepositoryBy{id}");

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var data = await response.Content.ReadFromJsonAsync<OperationResult<UpdateNetworkTypeModel>>(options);

                    return View(data.Result);
                }
                else
                {
                    ViewBag.Message = "Error obteniendo el Network Type";
                    return View();
                }
            }
        }

        // POST: NetworkTypeWebController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateNetworkTypeModel insuranceProvider)
        {
            try
            {
                //Hay que validar los datos
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7099/api/");
                    insuranceProvider.changeDate = DateTime.Now;

                    var response = await client.PutAsJsonAsync($"NetworkTypeApi/UpdateNetworkTypeRepository", insuranceProvider);

                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        var data = await response.Content.ReadFromJsonAsync<OperationResult<UpdateNetworkTypeModel>>(options);

                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Message = "Error actualizando el Network Type";
                        return View();
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: NetworkTypeWebController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7099/api/");

                var response = await client.GetAsync($"NetworkTypeApi/GetNetworkTypeRepositoryBy{id}");

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var data = await response.Content.ReadFromJsonAsync<OperationResult<RemoveNetworkTypeModel>>(options);

                    return View(data.Result);
                }
                else
                {
                    ViewBag.Message = "Error obteniendo el Network Type";
                    return View();
                }
            }
        }

        // POST: NetworkTypeWebController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RemoveNetworkTypeModel insuranceProvider)
        {

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7099/api/");

                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(client.BaseAddress, "NetworkTypeApi/RemoveInsuranceProvider"),
                        Content = new StringContent(JsonSerializer.Serialize(insuranceProvider), Encoding.UTF8, "application/json")
                    };

                    var response = await client.SendAsync(request);

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        await response.Content.ReadFromJsonAsync<OperationResult<RemoveNetworkTypeModel>>();

                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Message = $"Error del servidor: {response.StatusCode}";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error inesperado: {ex.Message}";
                return View("Error");
            }
        }
    }
}
