using Microsoft.AspNetCore.Mvc;
using NetMedWebApi.Models.Status;


namespace NetMedWebApi.Controllers
{
    public class StatusController : Controller
    {
            public StatusController(StatusController services)
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
                    var result = await _services.GetById<StatusApiModel>(id);
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

            // POST: RolesCController/Create
            [HttpPost]
            [ValidateAntiForgeryToken]

            public async Task<IActionResult> Create(SaveStatusModel save)
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

            // GET: RolesCController/Edit/5
            public async Task<IActionResult> Edit(int id)
            {
                try
                {
                    var result = await _services.GetById<UpdateStatusModel>(id);
                    return View(result.Data);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = $"{ex.Message}";
                    return View();
                }
            }

        // POST:  StatusController/Edit/5
        [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(UpdateStatusModel update)
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




        // GET: StatusController/Delete/5
        public async Task<IActionResult> Delete(int id)
            {
                try
                {
                    var result = await _services.GetById<DeleteStatusModel>(id);
                    return View(result.Data);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = $"{ex.Message}";
                    return View();
                }
            }

            // POST: StatusController/Delete/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Delete(int id, DeleteStatusModel delete)
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
}
