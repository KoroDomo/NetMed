using Microsoft.AspNetCore.Mvc;

namespace NetMed.WebApi.Controllers
{
    public class InsuranceProviderWebApiController : Controller
    {
        // GET: InsuranceProviderWebApiController
        public async Task<IActionResult> Index()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7099/api/");

                    var response = await client.GetAsync("InsuranceProviderApi/GetInsuranceProviders");

                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                    }
                }

                return View();
            }
            catch (Exception ex) 
            {
                return View($"Error: {ex.Message}");
            }
        }

        // GET: InsuranceProviderWebApiController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InsuranceProviderWebApiController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InsuranceProviderWebApiController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InsuranceProviderWebApiController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InsuranceProviderWebApiController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InsuranceProviderWebApiController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InsuranceProviderWebApiController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
