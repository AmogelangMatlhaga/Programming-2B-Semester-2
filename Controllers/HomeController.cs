using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using part1_poe.Models;

namespace part1_poe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //controller to navigate to the login page
        public IActionResult Register()
        {
            return View();
        }
        //controller to navigate to the submit claim page
        public IActionResult SubmitClaim()
        {
            return View();
        }
        //controller to navigate to the home page
        public IActionResult Home() 
        {
            return View();
        }
        //controller for the track claim page
        public IActionResult TrackClaim() 
        {
            return View();
        }
        //controller for the pre approved page
        public IActionResult PreApproved()
        {
            return View();
        }
        //controller for the approved page
        public IActionResult Approved()
        {
            return View();
        }

        //form http request login
        [HttpPost]
        public IActionResult user_login()
        {
            return RedirectToAction("Home", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
