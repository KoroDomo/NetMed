using Microsoft.AspNetCore.Mvc;
using NetMedWebApi.Application.Contracts;
using NetMedWebApi.Models.Notification;


namespace NetMedWebApi.Controllers
{
    public class NotificationController : Controller
    {

        private readonly INotificationContract _services;

        public NotificationController(INotificationContract services)
        {
            _services = services;
        }

        // GET: NotificationController
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

        // GET: NotificationController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var result = await _services.GetById<NotificationApiModel>(id);
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

        // POST: NotificationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(SaveNotificationModel saveNotification)
        {
       
            try  
            {
                await _services.Create(saveNotification);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return View();
            }
        }

        // GET: NotificationController/Edit/5
        public async Task<IActionResult>  Edit(int id)
        {
            try
            {
                var result = await _services.GetById<UpdateNotificationModel>(id);
                return View(result.Data);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"{ex.Message}";
                return View();
            }
        }

        // POST: NotificationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateNotificationModel updateNotificationModel)
        {
            try
            {
                await _services.Update(updateNotificationModel);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return View();
            }
           
        }
             
        // GET: NotificationController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _services.GetById<DeleteNotificationModel>(id);
                return View(result.Data);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"{ex.Message}";
                return View();
            }
        }

        // POST: NotificationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DeleteNotificationModel deleteNotification)
        {
            try
            {
                await _services.Delete(deleteNotification);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return View();
            }
        }
    }
}
