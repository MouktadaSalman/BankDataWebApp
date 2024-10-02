using BankPresentationLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace BankPresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _dataServerApiUrl = "http://localhost:5265";
        private readonly ConcurrentDictionary<string, UserProfile> usersInSession = new ConcurrentDictionary<string, UserProfile>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        // Render the Login page
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

		public IActionResult Login()
		{
			return View("~/Views/Home/LoginView.cshtml"); // Ensure a view named "Login.cshtml" is present under Views/Home
		}


		// Render the Dashboard page after successful login
		public IActionResult Dashboard()
        {
            return View("~/Views/Home/DashboardView.cshtml");
        }

       

        public IActionResult LoginError()
        {
            return View();
        }

        [HttpGet("defaultview")]
        public IActionResult GetDefaultView()
        {
            if (Request.Cookies.ContainsKey("SessionID"))
            {
                var cookieValue = Request.Cookies["SessionID"];
                if (cookieValue == "1234567")
                {
                    return RedirectToAction("Dashboard");
                }

            }
            return RedirectToAction("Login");
        }

        [HttpGet("authenticate")]
        public IActionResult GetAuthenticatedView()
        {
            if (Request.Cookies.ContainsKey("SessionID"))
            {
                var sessionId = Request.Cookies["SessionID"];
                if (sessionId != null && usersInSession.ContainsKey(sessionId))
                {
                    _logger.LogInformation("User is authenticated with SessionID: {SessionID}", sessionId);

                    return RedirectToAction("Dashboard");
                }

                _logger.LogWarning("Session ID not found in list of current sessions.");
            }
            return RedirectToAction("LoginError");
        }

        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] LoginData user)
        {
            _logger.LogInformation("Authenticate method called with Username: {Username}", user.Username);

            try
            {
                RestClient client = new RestClient(_dataServerApiUrl);
                RestRequest request = new RestRequest($"/api/userprofile/name/{user.Username}", Method.Get);
                
                _logger.LogInformation("Sending request to: {RequestUrl}", request.Resource);

                RestResponse restResponse = client.Execute(request);

                if (!restResponse.IsSuccessful)
                {
                    _logger.LogWarning("Failed to get user profile. Status: {StatusCode}, Content: {Content}", restResponse.StatusCode, restResponse.Content);
                    
                    return Json(new { login = false });
                }

                UserProfile? userProfile = JsonConvert.DeserializeObject<UserProfile>(restResponse.Content);                

                if (user.Username.Equals(userProfile.FName) && user.Password.Equals(userProfile.Password))
                {
                    _logger.LogInformation("User authenticated successfully for Username: {Username}", user.Username);
                    
                    var sessionID = Guid.NewGuid().ToString(); // Generate ubnique session ID
                    Response.Cookies.Append("SessionID", sessionID, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                    });

                    usersInSession.TryAdd(sessionID, userProfile);
                    return Json(new { login = true });
                }

                _logger.LogWarning("Authentication failed for Username: {Username}", user.Username);
               
                return Json(new { login = false });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while authenticating user: {Username}", user.Username);
                
                return Json(new { login = false });
            }
        }

        [HttpGet("loadprofile")]
        public IActionResult LoadProfile()
        {
            try
            {
                if (Request.Cookies.ContainsKey("SessionID"))
                {
                    var sessionId = Request.Cookies["SessionID"];

                    if (sessionId != null && usersInSession.ContainsKey(sessionId))
                    {
                        UserProfile? currentUser = usersInSession[sessionId];

                        if (currentUser != null)
                        {
                            _logger.LogInformation("User profile loaded successfully for SessionID: {SessionID}", sessionId);
                            return Json(currentUser);
                        }
                    }
                    else
                    {
                        _logger.LogWarning("Session ID not found in list of current sessions.");
                        return Unauthorized();
                    }
                }

                // Return unauthorized if no SessionID cookie is found or user profile is not loaded.
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while loading user profile");
                return StatusCode(500, "An error occurred while processing the request");
            }

        }
    }
}
