using Microsoft.AspNetCore.Mvc;
using NetMedWebApi.Models;
using NetMedWebApi.Models.Notification;
using System.Text;
using System.Text.Json;

namespace NetMedWebApi.Controllers
{
    public class NotificationController : Controller
    {
        // GET: NotificationController
        public async Task<IActionResult> Index()
        {
            List<NotificationApiModel> notifications = new List<NotificationApiModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261/api/");
                var reponse = await client.GetAsync("Notification/GetAllNotifications");

                if (reponse.IsSuccessStatusCode)
                {
                    var data = await reponse.Content.ReadFromJsonAsync<OperationResultList<NotificationApiModel>>();
                    return View(data.Data);

                }

            }
            return View();

        }

        // GET: NotificationController/Details/5
        public async Task<IActionResult> Details(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261/api/");
                var response = await client.GetAsync($"Notification/GetById?Id={id}");

                string apiResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OperationResultSingle<NotificationApiModel>>();
                    return View(result.Data);
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

        // POST: NotificationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(SaveNotificationModel saveNotification)
        {
       
            try  
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(" http://localhost:5261/api/");
                    var response = await client.PostAsJsonAsync("Notification/CreatedNotifications", saveNotification);

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        await response.Content.ReadFromJsonAsync < OperationResultList<NotificationApiModel>>();
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

        // GET: NotificationController/Edit/5
        public async Task<IActionResult>  Edit(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261/api/");
                var response = await client.GetAsync($"Notification/GetById?Id={id}");

                string apiResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync <OperationResultSingle<UpdateNotificationModel>>();
                    return View(result.Data);
                }
                else
                {
                    ViewBag.Message = "Error obteniendo la Cita";
                    return View();

                }

            }
        }

        // POST: NotificationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateNotificationModel updateNotificationModel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5261/api/");
                    var response = await client.PutAsJsonAsync("Notification/UpdateNotifications", updateNotificationModel);

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        await response.Content.ReadFromJsonAsync<OperationResultSingle<UpdateNotificationModel>>();
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



             
        // GET: NotificationController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261/api/");
                var response = await client.GetAsync($"Notification/GetById?Id={id}");

                string apiResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OperationResultSingle<DeleteNotificationModel>>();
                    return View(result.Data);
                }
                else
                {
                    ViewBag.Message = "Error obteniendo la Cita";
                    return View();

                }

            }
        }

        // POST: NotificationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DeleteNotificationModel deleteNotification)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5261/api/");

                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(client.BaseAddress, "Notification/DeleteNotifications"),
                        Content = new StringContent(JsonSerializer.Serialize(deleteNotification), Encoding.UTF8, "application/json")
                    };

                    var response = await client.SendAsync(request);

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        await response.Content.ReadFromJsonAsync<OperationResultList<DeleteNotificationModel>>();
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
