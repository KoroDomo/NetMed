using Microsoft.AspNetCore.Mvc;
using NetMedWebApi.Application.Contracts;
using NetMedWebApi.Models.Roles;


namespace NetMedWebApi.Controllers
{
    public class RolesController : Controller
    {
        private readonly IRolesContract _services;

        public RolesController(IRolesContract services)
        {
            _services = services;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _services.GetAll();
                return View(result.Data);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"{ex.Message}";
                return View();
            }

        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var result = await _services.GetById<RolesApiModel>(id);
                return View(result.Data);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"{ex.Message}";
                return View();
            }

        }

        public ActionResult Create()
        {
            return View();

        }

        // POST: RolesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(SaveRolesModel save)
        {

            try
            {
                await _services.Create(save);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return View();
            }
        }

        // GET: RolesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var result = await _services.GetById<UpdateRolesModel>(id);
                return View(result.Data);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"{ex.Message}";
                return View();
            }
        }

        // POST: RolesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateRolesModel update)
        {
            try
            {
                await _services.Update(update);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return View();
            }

        }

        // GET: RolesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _services.GetById<DeleteRolesModel>(id);
                return View(result.Data);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"{ex.Message}";
                return View();
            }
        }

        // POST: RolesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DeleteRolesModel delete)
        {
            try
            {
                await _services.Delete(delete);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return View();
            }
        }
    }


}