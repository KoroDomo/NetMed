using Microsoft.AspNetCore.Mvc;
using NetMed.ApiConsummer.Core.Models.NetworkType;
using NetMed.ApiConsummer.Application.Contracts;

namespace NetMed.Web1.Controllers
{
    public class NetworkTypeWebController : Controller
    {
        private readonly INetworkTypeService _service;

        public NetworkTypeWebController(INetworkTypeService service)
        {
            _service = service;
        }
        // GET: NetworkTypeWebController
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

        // GET: NetworkTypeWebController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var result = await _service.GetById<GetNetworkTypeModel>(id);
                return View(result.Result);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error: {ex.Message}";
                return View();
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
        public async Task<IActionResult> Create(SaveNetworkTypeModel network)
        {
            try
            {
                try
                {
                    //Hay que validar los datos
                    await _service.Save(network);
                    return RedirectToAction(nameof(Index));

                }
                catch
                {
                    return View();
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
            try
            {
                var result = await _service.GetById<UpdateNetworkTypeModel>(id);
                return View(result.Result);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error: {ex.Message}";
                return View();
            }
        }

        // POST: NetworkTypeWebController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateNetworkTypeModel network)
        {
            try
            {
                //Hay que validar los datos
                try
                {
                    await _service.Update(network);
                    return RedirectToAction(nameof(Index));

                }
                catch
                {
                    return View();
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
            try
            {
                var result = await _service.GetById<RemoveNetworkTypeModel>(id);
                return View(result.Result);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error: {ex.Message}";
                return View();
            }
        }

        // POST: NetworkTypeWebController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RemoveNetworkTypeModel network)
        {

            //Hay que validar los datos
            try
            {
                await _service.Remove(network);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
