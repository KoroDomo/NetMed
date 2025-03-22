using Microsoft.AspNetCore.Mvc;
using NetMed.Application.Contracts;
using NetMed.Web.Models;
using System.Diagnostics;

namespace NetMed.Web.Controllers
{
    public class NetworkTypeController : Controller
    {
        private readonly INetworkTypeService _networkTypeService;
        

        public NetworkTypeController(INetworkTypeService networkType)
        {
            _networkTypeService = networkType;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _networkTypeService.GetAll();
            if (result.Success==true)
            {
                return View(result.Result);
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
