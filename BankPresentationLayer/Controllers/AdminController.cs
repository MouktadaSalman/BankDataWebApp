using BankPresentationLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace BankPresentationLayer.Controllers
{
    public class AdminController : Controller
    {
        private readonly string _dataServerApiUrl = "http://localhost:5265";
        private readonly ILogger<AdminController> _logger;
        private static readonly object _logLock = new object();

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        private void Log(string? message, LogLevel logLevel, Exception? ex)
        {
            lock (_logLock)
            {
                if (ex != null)
                {
                    _logger.Log(logLevel, ex, $"{DateTime.Now}:");
                }
                else
                {
                    string logEntry = $"{DateTime.Now}: {message}";
                    _logger.Log(logLevel, logEntry);
                }
            }
        }

        public IActionResult AdminLogin()
        {
            Log("Navigate to the admin login page", LogLevel.Information, null);
            return View("~/Views/Admin/AdminLoginView.cshtml");
        }

        public IActionResult AdminDashboard()
        {
            Log("Navigate to the admin dashboard page", LogLevel.Information, null);
            return View("~/Views/Admin/AdminDashboard.cshtml");
        }

        public IActionResult GoToHome()
        {
            Log("Navigate to the home login page", LogLevel.Information, null);
            return RedirectToAction("Index", "Home");
        }
    }
}
