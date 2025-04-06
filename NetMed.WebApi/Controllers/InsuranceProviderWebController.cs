using Microsoft.AspNetCore.Mvc;
using NetMed.ApiConsummer.Application.Contracts;
using NetMed.ApiConsummer.Core.Models.InsuranceProvider;

namespace NetMed.ApiConsummer.Controllers
{
    public class InsuranceProviderWebController : Controller
    {
        private readonly IInsuranceProviderService _service;

        public InsuranceProviderWebController(IInsuranceProviderService service)
        {
            _service = service;
        }
        // GET: InsuranceProviderWebController
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _service.GetAll();
                return View(result.Result);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error: {ex.Message}";
                return View();
            }

        }

        // GET: InsuranceProviderWebController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var result = await _service.GetById(id);
                return View(result.Result);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error: {ex.Message}";
                return View();
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
                await _service.Save(insuranceProvider);
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }

        // GET: InsuranceProviderWebController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var result = await _service.GetById(id);
                return View(result.Result);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error: {ex.Message}";
                return View();
            }
        }

        // POST: InsuranceProviderWebController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateInsuranceProviderModel insuranceProvider)
        {
            
            //Hay que validar los datos
            try
            {
                await _service.Update(insuranceProvider);
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
            
        }

        // GET: InsuranceProviderWebController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.GetById(id);
                return View(result.Result);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error: {ex.Message}";
                return View();
            }
        }

        // POST: InsuranceProviderWebController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RemoveInsuranceProviderModel insuranceProvider)
        {

            //Hay que validar los datos
            try
            {
                await _service.Remove(insuranceProvider);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
           
        }
    }
}
