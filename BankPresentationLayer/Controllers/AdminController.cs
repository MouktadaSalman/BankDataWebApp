using BankPresentationLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace BankPresentationLayer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
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

        // GET: api/userprofile/byname/{name}
        [HttpGet("name/{name}")]
        public IActionResult GetAdminByName(string name)
        {
            try
            {
                Log("Connect to the Data tier web server", LogLevel.Information, null);
                RestClient client = new RestClient(_dataServerApiUrl);
                RestRequest request = new RestRequest($"/api/admin/byname/{name}", Method.Get);
                RestResponse response = client.Execute(request);

                Log($"Attempt to retrieve admin details: '{name}'", LogLevel.Information, null);
                if (response.Content != null)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Admin? value = JsonConvert.DeserializeObject<Admin>(response.Content);

                        Log($"Successful retrieval of admin details: '{name}'", LogLevel.Information, null);
                        return Ok(value);
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        throw new DataRetrievalFailException("Internal DatabaseGenerationFailException occurred");
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        throw new DataRetrievalFailException("Internal MissingProfileException occurred");
                    }
                }

                throw new DataRetrievalFailException("Internal unkown exception occurred/Failed to get a response from Data tier");

            }
            catch (DataRetrievalFailException ex)
            {
                Log(null, LogLevel.Warning, ex);
                return NotFound(ex.Message);
            }
        }

        // GET: api/userprofile/byemail/{email}
        [HttpGet("email/{email}")]
        public IActionResult GetAdminByEmail(string email)
        {
            try
            {
                Log("Connect to the Data tier web server", LogLevel.Information, null);
                RestClient client = new RestClient(_dataServerApiUrl);
                RestRequest request = new RestRequest($"/api/admin/byemail/{email}", Method.Get);
                RestResponse response = client.Execute(request);

                Log($"Attempt to retrieve admin details: '{email}'", LogLevel.Information, null);
                if (response.Content != null)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Admin? value = JsonConvert.DeserializeObject<Admin>(response.Content);

                        Log($"Successful retrieval of admin details: '{email}'", LogLevel.Information, null);
                        return Ok(value);
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        throw new DataRetrievalFailException("Internal DatabaseGenerationFailException occurred");
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        throw new DataRetrievalFailException("Internal MissingProfileException occurred");
                    }
                }

                throw new DataRetrievalFailException("Internal unkown exception occurred/Failed to get a response from Data tier");

            }
            catch (DataRetrievalFailException ex)
            {
                Log(null, LogLevel.Warning, ex);
                return NotFound(ex.Message);
            }
        }
    }
}
