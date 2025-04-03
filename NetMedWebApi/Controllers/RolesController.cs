using Microsoft.AspNetCore.Mvc;
using NetMedWebApi.Models;
using NetMedWebApi.Models.Roles;
using System.Text;
using System.Text.Json;

namespace NetMedWebApi.Controllers
{
    public class RolesController : Controller
    {
        // GET: RolesController
        public async Task<IActionResult> Index()
        {
            List<RolesApiModel> roles = new List<RolesApiModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261/api/");
                var reponse = await client.GetAsync("Roles/GetAllRoles");

                if (reponse.IsSuccessStatusCode)
                {
                    var data = await reponse.Content.ReadFromJsonAsync<OperationResultList<RolesApiModel>>();
                    return View(data.Data);
                }

            }
            return View();

        }

        // GET: RolesController/Details/5
        public async Task<IActionResult> Details(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261/api/");
                var response = await client.GetAsync($"Roles/GetRolesById?roles={id}");

                string apiResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OperationResultSingle<RolesApiModel>>();
                    return View(result.Data);
                }
                else
                {
                    ViewBag.Message = "Error obteniendo la Cita";
                    return View();

                }
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: RolesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveRolesModel saveRolesModel)
        {
            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(" http://localhost:5261/api/");
                    var response = await client.PostAsJsonAsync("Roles/CreateRole", saveRolesModel);

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        await response.Content.ReadFromJsonAsync<OperationResultList<SaveRolesModel>>();
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

        //GET: RolesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261/api/");
                var response = await client.GetAsync($"Roles/GetRolesById?roles={id}");

                string apiResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OperationResultSingle<UpdateRolesModel>>();
                    return View(result.Data);
                }
                else
                {
                    ViewBag.Message = "Error obteniendo la Cita";
                    return View();

                }
            }
        }

        // POST: RolesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateRolesModel updateRolesModel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5261/api/");
                    var response = await client.PutAsJsonAsync("Roles/UpdateRole", updateRolesModel);

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        await response.Content.ReadFromJsonAsync<OperationResultSingle<UpdateRolesModel>>();
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




        // GET: RolesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261/api/");
                var response = await client.GetAsync($"Roles/GetRolesById?roles={id}");

                string apiResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OperationResultSingle<DeleteRolesModel>>();
                    return View(result.Data);
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
        public async Task<IActionResult> Delete(DeleteRolesModel delete)
        {
            try 
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5261/api/");

                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(client.BaseAddress, "Roles/DeleteRole"),
                        Content = new StringContent(JsonSerializer.Serialize(delete), Encoding.UTF8, "application/json")
                    };

                    var response = await client.SendAsync(request);

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        await response.Content.ReadFromJsonAsync<OperationResultList<DeleteRolesModel>>();
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