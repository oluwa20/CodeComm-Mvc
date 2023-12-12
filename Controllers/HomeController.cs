using CodeComm.Models;
using CodeComm.Models.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace CodeComm.Controllers
{
    public class HomeController : Controller
    {
        private readonly AuthApiService _authApiService;

        public HomeController(AuthApiService authApiService)
        {
            _authApiService = authApiService;
        }

        [HttpGet]
    
        public IActionResult Index()
        {
            return View(new UserLoginDto());
        }

        [HttpPost]
   
        public async Task<ActionResult> Index(string usernameOrEmail, string userPassword)
        {
            bool isAuthenticated = await _authApiService.AuthenticateAsync(usernameOrEmail, userPassword);

            if (isAuthenticated)
            {
           
                HttpContext.Session.SetString("Username", usernameOrEmail);

                
                ViewBag.Username = usernameOrEmail;

         
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, usernameOrEmail));

             


                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                
                return RedirectToAction("Homepage", "User");
            }
            else
            {
               
                ViewBag.ErrorMessage = "Invalid username or password";
                return View("Index");
            }
        }

        [HttpGet]

        public IActionResult Register()
        {
            //return View(new CreateUserDto());
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserDto model)
        {
            if (ModelState.IsValid)
            {
                bool isRegistered = await _authApiService.RegisterAsync(model);

                if (isRegistered)
                {
                    // Registration successful, redirect to login or another page
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Registration failed, handle accordingly
                    ViewBag.ErrorMessage = "Registration failed. Please try again.";
                    return View();
                }
            }

            // If ModelState is not valid, redisplay the form with validation errors
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
