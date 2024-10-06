﻿using BankPresentationLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Numerics;
using System.Xml.Linq;

namespace BankPresentationLayer.Controllers
{
    public class AdminController : Controller
    {
        private readonly string _dataServerApiUrl = "http://localhost:5265";
        private readonly ILogger<AdminController> _logger;
        private static readonly object _logLock = new object();
        private static readonly ConcurrentDictionary<string, Admin> adminsInSession = new ConcurrentDictionary<string, Admin>();

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

        private bool IsValidEmail(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);
                return true; // If no exception is thrown, the email is valid
            }
            catch (FormatException)
            {
                return false; // Invalid email format
            }
        }

        private RestResponse GetAdminDetails(string identifier)
        {
            if(identifier == null)
            {
                throw new ArgumentNullException("Null detected at entered username (identifier)");
            }

            RestClient client = new RestClient(_dataServerApiUrl);
            var endpoint = IsValidEmail(identifier) ? $"/api/admin/email/{identifier}" : $"/api/admin/name/{identifier}";
            RestRequest request = new RestRequest(endpoint, Method.Get);

            Log($"Attempt to retrieve admin details: '{identifier}'", LogLevel.Information, null);
            return client.Execute(request);
        }

        private bool IsUserAuthorized(string identifier)
        {
            if (Request.Cookies.ContainsKey("SessionID"))
            {
                var sessionId = Request.Cookies["SessionID"];

                // Check if the session ID corresponds to an authenticated admin
                if (adminsInSession.TryGetValue(sessionId, out Admin? currentAdmin))
                {
                    // Ensure the identifier matches the current admin's name or email
                    return (currentAdmin.FName.Equals(identifier));
                }
            }
            return false;
        }

        public IActionResult AdminLogin()
        {
            Log("Navigate to the admin login page", LogLevel.Information, null);
            return View("~/Views/Admin/AdminLoginView.cshtml");
        }

        [HttpGet("admindashboard/{id}/authorized/admin={identifier}-{lName}")]
        public IActionResult AdminDashboard(string identifier)
        {
            if (!IsUserAuthorized(identifier)) // Your authorization logic here
            {
                return RedirectToAction("LoginError"); // Redirect if not authorized
            }

            Log("Navigate to the admin dashboard page", LogLevel.Information, null);
            return View("~/Views/Admin/AdminDashboard.cshtml");
        }

        public IActionResult LoginError()
        {
            return RedirectToAction("LoginError", "Home");
        }

        public IActionResult Logout()
        {
            // Clear the user's session (for example, clear authentication session or token)
            HttpContext.Response.Cookies.Delete("SessionID");

            // Redirect to the login page
            return RedirectToAction("Login", "Home");
        }

        [HttpGet("getadmin/{identifier}")]
        public IActionResult GetAdmin(string identifier)
        {
            Log($"Attempt retrieval of admin details to update profile dashboard: '{identifier}'", LogLevel.Information, null);
            try
            {
                RestResponse response = GetAdminDetails(identifier);
                Admin? value = null;

                if (response.Content != null)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        value = JsonConvert.DeserializeObject<Admin>(response.Content);

                        Log($"Successful retrieval of admin details: '{identifier}'", LogLevel.Information, null);
                    }
                }

                if (value != null)
                {
                    Log($"Successful deserialization of details", LogLevel.Information, null);
                    string name = $"{value.FName} { value.LName}";
                    var email = value.Email != null ? value.Email : "";
                    var phone = value.PhoneNumber != null ? value.PhoneNumber : "";
                    var password = value.Password != null ? value.Password : "";

                    return Json(new
                    {
                        auth = true,
                        name,
                        email,
                        phone,
                        password
                    });
                }

                throw new DataRetrievalFailException("Failure to retrieve data occurred");
            }
            catch (DataRetrievalFailException e)
            {
                Log(null, LogLevel.Warning, e);
                return Json(new
                {
                    Check = false
                });
            }
            catch (ArgumentNullException e)
            {
                Log(null, LogLevel.Warning, e);
                return Json(new
                {
                    Check = false
                });
            }
            catch (Exception e)
            {
                Log(null, LogLevel.Critical, e);
                return Json(new
                {
                    Check = false
                });
            }
        }

        [HttpPost("authenticate")]
        public IActionResult AuthenticateAdmin([FromBody] LoginData admin)
        {
            Log($"Authentication initiated for username: {admin.Username}", LogLevel.Information, null);

            try
            {
                RestResponse response = GetAdminDetails(admin.Username);
                Admin? value = null;

                if (response.Content != null)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        value = JsonConvert.DeserializeObject<Admin>(response.Content);

                        Log($"Successful retrieval of admin details: '{admin.Username}'", LogLevel.Information, null);
                    }
                }

                if (value != null)
                {
                    Log($"Entered username: {admin.Username}, password: {admin.Password}", LogLevel.Information, null);
                    Log($"Retrieved username: {value.FName}, password: {value.Password}", LogLevel.Information, null);

                    //Check if either email/first name matches and see if password matches
                    if((value.FName.Equals(admin.Username) || value.Email.Equals(admin.Username)) 
                        && value.Password.Equals(admin.Password))
                    {
                        Log($"Authentication of admin account successful: '{admin.Username}'", LogLevel.Information, null);

                        var sessionID = Guid.NewGuid().ToString(); // Generate unique session ID
                        Response.Cookies.Append("SessionID", sessionID, new CookieOptions
                        {
                            Secure = true, // Only send over HTTPS
                            HttpOnly = true, // Prevent JavaScript access
                            SameSite = SameSiteMode.Strict, // Protect against CSRF
                            Expires = DateTimeOffset.UtcNow.AddHours(1)
                        });

                        Log($"Allocated User session Id: '{sessionID}'", LogLevel.Information, null);

                        if (adminsInSession.TryAdd(sessionID, value))
                        {
                            Log($"Admin session successfully added: '{sessionID}'", LogLevel.Information, null);
                        }
                        else
                        {
                            Log($"Failed to add admin session: '{sessionID}'", LogLevel.Critical, null);
                        }

                        return Json(new { login = true });
                    }
                }

                throw new DataRetrievalFailException("Failure to retrieve data occurred");
            }
            catch(DataRetrievalFailException e)
            {
                Log(null, LogLevel.Warning, e);
                return Json(new { login = false });
            }
            catch(ArgumentNullException e)
            {
                Log(null, LogLevel.Warning, e);
                return Json(new { login = false });
            }
            catch (Exception e)
            {
                Log(null, LogLevel.Critical, e);
                return Json(new { login = false });
            }
        }

        [HttpGet("authenticated/{identifier}")]
        public IActionResult GetAuthorizedView(string identifier)
        {
            Log($"Total active sessions: {adminsInSession.Count}", LogLevel.Information, null);
            Log($"Attempted to check identifier: {identifier}", LogLevel.Information, null);

            if (Request.Cookies.ContainsKey("SessionID"))
            {
                var sessionId = Request.Cookies["SessionID"];
                Log($"User is authenticated with SessionID", LogLevel.Information, null);

                if (adminsInSession.TryGetValue(sessionId, out Admin? currentAdmin))
                {
                    if ((currentAdmin.FName != null && currentAdmin.FName.Equals(identifier)) || 
                        (currentAdmin.Email != null && currentAdmin.Email.Equals(identifier)))
                    {
                        Log("Admin found and authenticated", LogLevel.Information, null);
                        return RedirectToAction("AdminDashboard", new { id = currentAdmin.Id, identifier = currentAdmin.FName, lName = currentAdmin.LName});
                    }
                    else
                    {
                        Log("Session found, but admin name does not match", LogLevel.Warning, null);
                    }
                }
                else
                {
                    Log("Session ID not found in list of current sessions.", LogLevel.Critical, null);
                }
            }
            return RedirectToAction("LoginError");
        }
    }
}
