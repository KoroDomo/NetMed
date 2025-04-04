using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetMed.Web.Models;
using WebNetMedAPI.Models;

namespace NetMed.Web.Controllers
{

    public class UsersController : Controller
    {
        // GET: UsersModelController

        public async Task<IActionResult> Index()
        {
            List<UsersModel> users = new List<UsersModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261");

                var response = await client.GetAsync("api/Users/GetUsers");
                if (response.IsSuccessStatusCode)
                {
                    var dataResult = await response.Content.ReadFromJsonAsync<OperationResultList<UsersModel>>();
                    if (dataResult != null)
                    {
                        users = dataResult.data;
                    }

                }
            }
            return View(users);
        }

        // GET: UsersController/Details/5

        public async Task<IActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261");
                var response = await client.GetAsync($"api/Users/GetUserById/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OperationResult<UsersModel>>();
                  if (result == null)
                    {
                        return NotFound();
                    }
                     
                        return View(result.data);
                    
                }
            }
            return View(); 
        }

        // GET: UsersController/Create

        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsersModel usersModel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5261");
                    var response = await client.PostAsJsonAsync("api/SaveUsers", usersModel);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<OperationResult<UsersModel>>();
                        if (result != null)
                        {
                            return NotFound();
                        }

                    }
                    return View(usersModel);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Edit/5

        public async Task<IActionResult>Edit(int id)
        
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261");
                var response = await client.GetAsync($"api/Users/GetUserById/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OperationResult<UsersModel>>();
                    if (result == null)
                    {
                        return NotFound();
                    }
                    return View(result.data);
                }
            }
            return View();
        }


        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UsersModel usersModel)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261");
                var response = client.PutAsJsonAsync($"api/Users/UpdateUser/{id}", usersModel);
                if (response.Result.IsSuccessStatusCode)
                {
                    var result = response.Result.Content.ReadFromJsonAsync<OperationResult<UsersModel>>();
                    if (result != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(usersModel);
            }
        }

        // GET: UsersModelController/Delete/5

        public async Task<IActionResult>Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5261");
                var response = await client.GetAsync($"api/Users/GetUserById/{id}");
                if(response.IsSuccessStatusCode)
                {
                    string dataResponse = await response.Content.ReadAsStringAsync();

                    var dat = await response.Content.ReadFromJsonAsync<OperationResult<UsersModel>>();
                    if (dat == null)
                    {
                        return NotFound();
                    }
                    return View(dat.data);
                }
            }
            return View();
        }

        // POST: UsersModelController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, UsersModel usersModel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5261");
                    var resp = await client.DeleteAsync($"api/Users/DeleteUsers/{id}");
                    if (resp.IsSuccessStatusCode)
                    {
                        var data = await resp.Content.ReadFromJsonAsync<OperationResult<UsersModel>>();
                        if (data != null)
                        {
                            return RedirectToAction(nameof(Index));
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
            return View(usersModel);
        }
    }
}
