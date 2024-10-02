using BankPresentationLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Diagnostics;

namespace BankPresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

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
                var cookieValue = Request.Cookies["SessionID"];
                if (cookieValue == "1234567")
                {
                    return RedirectToAction("Dashboard");
                }

            }
            return RedirectToAction("LoginError");
        }

        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] LoginData user)
        {
            _logger.LogInformation("Authenticate method called with Username: {Username}", user.Username);

            try
            {
                RestClient client = new RestClient("http://localhost:5265");
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
                    Response.Cookies.Append("SessionID", "1234567");
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

    }
}
