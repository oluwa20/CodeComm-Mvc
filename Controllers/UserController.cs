using CodeComm.Models.ViewModel;
using CodeComm.Models;
using CodeComm;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Reflection;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.AspNetCore.Razor.Language;
using System.Net.Http;


namespace CodeComm.Controllers
{
    [Authorize]
    public class UserController : Controller
    {

        Uri baseurl = new Uri("http://dejidabiri-001-site1.atempurl.com");
        private readonly HttpClient _client;

       
        private readonly AuthApiService _authApiService;

        public UserController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseurl;
            _authApiService = new AuthApiService();
        }
        public async Task<IActionResult> Homepage()
        {
            var usersActionResult = await People();

            
            if (usersActionResult is ViewResult viewResult)
            {
                var users = viewResult.Model as List<GetAllUsersDto>;

                return View(users);
            }

           
            return View(new List<GetAllUsersDto>());
        }

        [HttpGet]
        public async Task<ActionResult> People()
        {
            string apiUrl = "http://dejidabiri-001-site1.atempurl.com/api/User/GetAllUsers";

            try
            {
                HttpResponseMessage response = await _client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Response: {apiResponse}");

                    
                    if (string.IsNullOrEmpty(apiResponse))
                    {
                        Console.WriteLine("API Response is empty.");
                        return View(new List<GetAllUsersDto>());
                    }

                    var apiResponseObject = JsonConvert.DeserializeObject<ApiResponse<List<GetAllUsersDto>>>(apiResponse);

                    
                    if (apiResponseObject.status)
                    {
                        var result = apiResponseObject.data;

                        

                        Console.WriteLine($"Number of items in the result: {result?.Count ?? 0}");

                        return View(result ?? new List<GetAllUsersDto>());
                    }
                    else
                    {
                        ModelState.AddModelError("ApiError", $"API response indicates failure. Response Code: {apiResponseObject.responseCode}, Message: {apiResponseObject.responseMessage}");
                        return View(new List<GetAllUsersDto>());
                    }
                }
                else
                {
                    ModelState.AddModelError("ApiError", $"API request failed with status code {response.StatusCode}");

                  
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Error: {errorMessage}");

                    return View(new List<GetAllUsersDto>());
                }
            }
            catch (Exception ex)
            {
                
                ModelState.AddModelError("ApiError", $"An error occurred: {ex.Message}");
                Console.WriteLine($"Exception: {ex}");

                return View(new List<GetAllUsersDto>());
            }
        }



        [HttpPost]
        public async Task<IActionResult> StartChat(string userId, [FromServices] AuthApiService authApiService)
        {
            
            var currentUserId = "YourCurrentUserId"; 

            // Create a chat model with the current user's ID and the selected user's ID
            var chatModel = new CreateChat
            {
                userID1 = currentUserId,
                userID2 = userId
            };

            // Call your chat creation API using the ChatApiService
            bool chatCreated = await authApiService.CreateChatAsync(chatModel);

            if (chatCreated)
            {
                // Redirect to a chat page or handle the chat creation success/failure as needed
                return RedirectToAction("Chat", new { userId });
            }
            else
            {
                // Handle chat creation failure
                ViewBag.ErrorMessage = "Failed to start a chat. Please try again.";
                return View("Error");
            }
        }



        public IActionResult Message()
        {
            return View();
        }


        public IActionResult Notification()
        {
            return View();
        }


        public IActionResult Setting()
        {
            return View();
        }
      

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

           
            return RedirectToAction("Index", "Home");
        }
    }
}