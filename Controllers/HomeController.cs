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

        [HttpGet]
        public IActionResult Index()
        {
            //calling the model
            CreateInstanceDBandTables Connect = new CreateInstanceDBandTables();

            
            Connect.InitializeSystem();

            return View();
        
        
        }


        [HttpPost]
        public IActionResult Index(login user)
        {

            if (ModelState.IsValid)
            {

                user_query check_user = new user_query();
                string role = "none";
                string collect = check_user.check_users(user.Email, user.Password, role);
                string id = "0";
                if (collect.Contains( ",")) 
                {
                    string[] collected = collect.Split(",");

                    id = collected[0];

                    role = collected[1];
                }
 

                if ( id!="0")
                {
                    Console.WriteLine(role);
                    HttpContext.Session.SetString("role",role);
                    HttpContext.Session.SetString("id", id);

                    return RedirectToAction("Home", "Home");
                }
                else
                {
                    ViewBag.error = "Incorrect email or password";
                }
                
            }

            return View(user);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        //controller to navigate to the login page
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(register user)
        {
            if (ModelState.IsValid)
            {

                user_query check_user = new user_query();

                if (check_user.register_user(
                user.FirstName,
                user.Surname,
                user.Email,
                user.Gender,
                user.Role,
                user.Password) != "0")
                {

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Email already have an account";
                }

            }




            return View(user);
        }


        //controller to navigate to the submit claim page
        [HttpGet]
        public IActionResult SubmitClaim()
        {
            ViewBag.role = HttpContext.Session.GetString("role");
            ViewBag.id = HttpContext.Session.GetString("id");

            return View();
        }


        [HttpPost]
        public IActionResult SubmitClaim(submit_claims claims)
        {
            user_query check_user = new user_query();

            ViewBag.role = HttpContext.Session.GetString("role");
            ViewBag.id = HttpContext.Session.GetString("id");

            int rate = (int)claims.Rate;
            claims.TotalAmount = check_user.calculate_total(claims.Sessions ,rate);

            string? filePath = null;

            // Handle file upload
            string? savedFileName = null;

            if (claims.Documents != null && claims.Documents.Length > 0)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(claims.Documents.FileName)}";
                var fullFilePath = Path.Combine(folderPath, uniqueFileName);

                // Save file to server folder
                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    claims.Documents.CopyTo(stream);
                }

                savedFileName = uniqueFileName;
            }

            bool done = check_user.claimsbmit(
                ViewBag.id?.ToString(),
                claims.Sessions,
                claims.Hours,
                claims.Rate,
                claims.TotalAmount,
                claims.Module,
                claims.Faculty,
                savedFileName
            );

            if (done)
            {
                ViewBag.message = "Claim submitted successfully!";
            }
            else
            {
                ViewBag.message = "Failed to submit claim. Please try again.";
            }

            return View(claims);
        }

        //controller to navigate to the home page
        public IActionResult Home() 
        {


            ViewBag.role = HttpContext.Session.GetString("role");
            ViewBag.id = HttpContext.Session.GetString("id");


            Console.WriteLine(ViewBag.role);
            return View();
        }
        //controller for the track claim page
        public IActionResult TrackClaim() 
        {
            ViewBag.role = HttpContext.Session.GetString("role");
            ViewBag.id = HttpContext.Session.GetString("id");
            string? lecturerId = HttpContext.Session.GetString("id");

            user_query query = new user_query();
            var claims = query.GetClaims(lecturerId);

            return View(claims);

        }
        [HttpPost]
        public IActionResult CancelClaim(int id)
        {
            user_query query = new user_query();
            bool success = query.CancelClaim(id);

            TempData["Message"] = success ? "Claim cancelled successfully." : "Failed to cancel claim.";
            return RedirectToAction("TrackClaim");
        }

        [HttpPost]
        public IActionResult DeleteClaim(int id)
        {
            user_query query = new user_query();
            bool success = query.DeleteClaim(id);

            TempData["Message"] = success ? "Claim deleted successfully." : "Failed to delete claim.";
            return RedirectToAction("TrackClaim");
        }


        public IActionResult PreApproveClaim(int id)
        {
            // Instantiate the class inside the method
            user_query query = new user_query();

            // Call the class method
            bool done = query.PreApproveClaim(id);

            if (done)
            {
                TempData["Message"] = "Claim pre-approved successfully!";
            }
            else
            {
                TempData["Error"] = "Error pre-approving claim.";
            }

            return RedirectToAction("PreApproved");
        }

        public IActionResult RejectClaim(int id)
        {
            // Instantiate the class inside the method
            user_query query = new user_query();

            // Call the class method
            bool done = query.RejectClaim(id);

            if (done)
            {
                TempData["Message"] = "Claim rejected successfully!";
            }
            else
            {
                TempData["Error"] = "Error rejecting claim.";
            }

            return RedirectToAction("PreApproved");
        }










        //controller for the pre approved page
        public IActionResult PreApproved()
        {
            ViewBag.role = HttpContext.Session.GetString("role");
            ViewBag.id = HttpContext.Session.GetString("id");

            user_query query = new user_query();
            var claims = query.GetAllClaims(); 
            return View(claims);
        }
        //controller for the approved page
        public IActionResult Approved()
        {
            ViewBag.role = HttpContext.Session.GetString("role");
            ViewBag.id = HttpContext.Session.GetString("id");

            user_query query = new user_query();
            var claims = query.GetAllClaim();
            return View(claims);
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
