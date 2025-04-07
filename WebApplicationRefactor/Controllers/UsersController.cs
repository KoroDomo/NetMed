using Microsoft.AspNetCore.Mvc;
using WebApplicationRefactor.Application.Contracts;
using WebApplicationRefactor.Models.Users;
using WebApplicationRefactor.Persistence.Interfaces.IRepository;

namespace WebApplicationRefactor.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        // GET: UsersController
        public async Task<IActionResult> Index()
        {
            var result = await _usersService.GetAllData();
            if (result.success)
            {
                return View(result.data);
            }
            ViewBag.Message = result.message;
            return View("Error");
        }

        // GET: UsersController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = await _usersService.GetById(id);
            if (result.success)
            {
                return View(result.data);
            }
            ViewBag.Message = result.message;
            return View("Error");
        }

        // GET: UsersController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsersApiModel user)
        {
            var result = await _usersService.Add(user);
            if (result.success)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Message = result.message;
            return View(user);
        }

        // GET: UsersController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _usersService.GetById(id);
            if (result.success)
            {
                return View(result.data);
            }
            ViewBag.Message = result.message;
            return View("Error");
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UsersApiModel user)
        {
            var result = await _usersService.Update(user);
            if (result.success)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Message = result.message;
            return View(user);
        }

        // GET: UsersController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _usersService.GetById(id);
            if (result.success)
            {
                return View(result.data);
            }
            ViewBag.Message = result.message;
            return View("Error");
        }

        // POST: UsersController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _usersService.Delete(new UsersApiModel { Id = id });
            if (result.success)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Message = result.message;
            return View();
        }
    }
}