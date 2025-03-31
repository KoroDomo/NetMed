using Microsoft.AspNetCore.Mvc;
using NetMed.Web1.Models;
using NetMed.Web1.Models.InsuranceProvider;
using System.Text;
using System.Text.Json;

namespace NetMed.Web1.Controllers
{
    public class InsuranceProviderWebController : Controller
    {
        // GET: InsuranceProviderWebController
        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7099/api/");

                var response = await client.GetAsync("InsuranceProviderApi/GetInsuranceProviders");

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var data = await response.Content.ReadFromJsonAsync<ListOperationResult<GetInsuranceProviderModel>>(options);
                    return View(data.Result);
                    
                }
                else
                {
                    ViewBag.Message = "Error obteniendo los InsuranceProviders";
                    return View();
                }
            }
        }

        // GET: InsuranceProviderWebController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7099/api/");

                var response = await client.GetAsync($"InsuranceProviderApi/GetInsuranceProviderBy{id}");

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var data = await response.Content.ReadFromJsonAsync<OperationResult<GetInsuranceProviderModel>>(options);
                    
                    return View(data.Result);
                }
                else
                {
                    ViewBag.Message = "Error obteniendo el InsuranceProvider";
                    return View();
                }
            }
        }

        // GET: InsuranceProviderWebController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InsuranceProviderWebController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveInsuranceProviderModel insuranceProvider)
        {
            try
            {
                //Hay que validar los datos
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7099/api/");
                    insuranceProvider.changeDate = DateTime.Now;

                    var response = await client.PostAsJsonAsync("InsuranceProviderApi/SaveInsuranceProvider", insuranceProvider);

                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        var data = await response.Content.ReadFromJsonAsync<OperationResult<SaveInsuranceProviderModel>>(options);

                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Message = "Error guardando el InsuranceProvider";
                        return View();
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: InsuranceProviderWebController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7099/api/");

                var response = await client.GetAsync($"InsuranceProviderApi/GetInsuranceProviderBy{id}");

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var data = await response.Content.ReadFromJsonAsync<OperationResult<UpdateInsuranceProviderModel>>(options);

                    return View(data.Result);
                }
                else
                {
                    ViewBag.Message = "Error obteniendo el InsuranceProvider";
                    return View();
                }
            }
        }

        // POST: InsuranceProviderWebController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateInsuranceProviderModel insuranceProvider)
        {
            try
            {
                //Hay que validar los datos
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7099/api/");
                    insuranceProvider.changeDate = DateTime.Now;

                    var response = await client.PutAsJsonAsync($"InsuranceProviderApi/UpdateInsuranceProvider", insuranceProvider);

                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        var data = await response.Content.ReadFromJsonAsync<OperationResult<UpdateInsuranceProviderModel>>(options);

                        return RedirectToAction(nameof(Index)); 
                    }
                    else
                    {
                        ViewBag.Message = "Error actualizando el InsuranceProvider";
                        return View();
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: InsuranceProviderWebController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7099/api/");

                var response = await client.GetAsync($"InsuranceProviderApi/GetInsuranceProviderBy{id}");

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var data = await response.Content.ReadFromJsonAsync<OperationResult<RemoveInsuranceProviderModel>>(options);

                    return View(data.Result);
                }
                else
                {
                    ViewBag.Message = "Error obteniendo el InsuranceProvider";
                    return View();
                }
            }
        }

        // POST: InsuranceProviderWebController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RemoveInsuranceProviderModel insuranceProvider)
        {

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7099/api/");

                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(client.BaseAddress, "InsuranceProviderApi/RemoveInsuranceProvider"),
                        Content = new StringContent(JsonSerializer.Serialize(insuranceProvider), Encoding.UTF8, "application/json")
                    };

                    var response = await client.SendAsync(request);

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        await response.Content.ReadFromJsonAsync<OperationResult<RemoveInsuranceProviderModel>>();

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
