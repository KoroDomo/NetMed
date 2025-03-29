using Microsoft.AspNetCore.Mvc;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.InsuranceProvider;

namespace NetMed.Web.Controllers
{
    public class InsuranceProviderWebController : Controller
    {
        private readonly IInsuranceProviderService _providerService;

        public InsuranceProviderWebController(IInsuranceProviderService providerService) 
        {
            _providerService = providerService;
        }
        // GET: InsuranceProviderController
        public async Task<IActionResult> Index()
        {
            var result = await _providerService.GetAll();

            if (result.Success == true)
            {
                return View(result.Result);
            }
            return View();
        }

        // GET: InsuranceProviderController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = await _providerService.GetById(id);

            if (result.Success == true)
            {
                return View(result.Result);
            }
            return View();
        }

        // GET: InsuranceProviderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InsuranceProviderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveInsuranceProviderDto saveInsuranceProvider)
        {
            try
            {
                await _providerService.Save(saveInsuranceProvider);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InsuranceProviderController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _providerService.GetById(id);

            if (result.Success == true)
            {
                return View(result.Result);
            }
            return View();
        }

        // POST: InsuranceProviderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateInsuranceProviderDto updateInsuranceProvider)
        {
            try
            {
                await _providerService.Update(updateInsuranceProvider);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InsuranceProviderController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _providerService.GetById(id);

            if (result.Success == true)
            {
                return View(result.Result);
            }
            return View();
        }

        // POST: InsuranceProviderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RemoveInsuranceProviderDto removeInsuranceProvider)
        {
            try
            {
                await _providerService.Remove(removeInsuranceProvider);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
