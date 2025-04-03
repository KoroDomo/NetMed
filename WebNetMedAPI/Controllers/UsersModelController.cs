using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetMed.Web.Controllers
{
    public class UsersModelController : Controller
    {
        // GET: UsersModelController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UsersModelController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsersModelController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersModelController/Create
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

        // GET: UsersModelController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsersModelController/Edit/5
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

        // GET: UsersModelController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsersModelController/Delete/5
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
