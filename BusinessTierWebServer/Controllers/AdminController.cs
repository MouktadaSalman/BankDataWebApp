using BankDataLB;
using BusinessTierWebServer.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Xml.Linq;

namespace BusinessTierWebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly string _dataServerApiUrl = "http://localhost:5181";
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

        // GET: api/admin/name/{name}
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
                if(response.Content != null)
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Admin? value = JsonConvert.DeserializeObject<Admin>(response.Content);

                        Log($"Successful retrieval of admin details: '{name}'", LogLevel.Information, null);
                        return Ok(value);
                    }

                    if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Log("Encountered internal missing data generation", LogLevel.Critical, null);
                        throw new DataRetrievalFailException("Internal DatabaseGenerationFailException occurred");
                    }

                    if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
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

        // GET: api/admin/email/{email}
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
                        Log("Encountered internal missing data generation", LogLevel.Critical, null);
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

        // GET: api/admin/id/{id}
        [HttpGet("id/{id}")]
        public IActionResult GetAdminById(int id)
        {
            try
            {
                Log("Connect to the Data tier web server", LogLevel.Information, null);
                RestClient client = new RestClient(_dataServerApiUrl);
                RestRequest request = new RestRequest($"/api/admin/byid/{id}", Method.Get);
                RestResponse response = client.Execute(request);

                Log($"Attempt to retrieve admin details: '{id}'", LogLevel.Information, null);
                if (response.Content != null)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Admin? value = JsonConvert.DeserializeObject<Admin>(response.Content);

                        Log($"Successful retrieval of admin details: '{id}'", LogLevel.Information, null);
                        return Ok(value);
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Log("Encountered internal missing data generation", LogLevel.Critical, null);
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

        // Put: api/admin/update/{id}
        [HttpPut("update/{id}")]
        public IActionResult UpdateAdminProfile(int id, [FromBody] Admin updatedAdmin)
        {
            try
            {
                Log("Connect to the Data tier web server", LogLevel.Information, null);
                RestClient client = new RestClient(_dataServerApiUrl);
                RestRequest request = new RestRequest($"/api/admin/update/{id}", Method.Put);
                request.AddJsonBody(updatedAdmin);
                RestResponse response = client.Execute(request);

                Log($"Attempt to update admin details id: '{id}'", LogLevel.Information, null);
                if (response.Content != null)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Admin? value = JsonConvert.DeserializeObject<Admin>(response.Content);

                        Log($"Successful retrieval of updated admin details: '{id}'", LogLevel.Information, null);
                        return Ok(value);
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Log("Encountered internal missing data generation", LogLevel.Critical, null);
                        throw new DataRetrievalFailException("Internal error missing data");
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        Log("Encountered internal missing/mismatch data", LogLevel.Warning, null);
                        throw new DataRetrievalFailException("Internal poor request occurred");
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                    {
                        Log("Encountered internal concurrency conflict", LogLevel.Warning, null);
                        throw new DataRetrievalFailException("Internal concurrency conflict occurred");
                    }
                }
                throw new DataRetrievalFailException("Internal unkown exception occurred/Failed to get a response from Data tier");
            }
            catch (DataRetrievalFailException e)
            {
                Log(null, LogLevel.Warning, e);
                return NotFound(e.Message);
            }
        }

        // GET: api/admin/no/{acctNo}
        [HttpGet("no/{acctNo}")]
        public IActionResult GetAccountByNo(uint acctNo)
        {
            try
            {
                Log("Connect to the Data tier web server", LogLevel.Information, null);
                RestClient client = new RestClient(_dataServerApiUrl);
                RestRequest request = new RestRequest($"/api/admin/byno/{acctNo}", Method.Get);
                RestResponse response = client.Execute(request);

                Log($"Attempt to retrieve account details: '{acctNo}'", LogLevel.Information, null);
                if (response.Content != null)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        BankAccount? value = JsonConvert.DeserializeObject<BankAccount>(response.Content);

                        Log($"Successful retrieval of account details: '{acctNo}'", LogLevel.Information, null);
                        return Ok(value);
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Log("Encountered internal missing data generation", LogLevel.Critical, null);
                        throw new DataRetrievalFailException("Internal DatabaseGenerationFailException occurred");
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        throw new DataRetrievalFailException("Internal MissingAccountException occurred");
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

        [HttpPut("updateaccount/{acctNo}")]
        public IActionResult UpdateAccountByNo(uint acctNo, [FromBody] BankAccount updatedAccount)
        {
            try
            {
                Log("Connect to the Data tier web server", LogLevel.Information, null);
                RestClient client = new RestClient(_dataServerApiUrl);
                RestRequest request = new RestRequest($"/api/account/{acctNo}", Method.Put);
                request.AddJsonBody(updatedAccount);
                RestResponse response = client.Execute(request);

                Log($"Attempt to update account details: '{acctNo}'", LogLevel.Information, null);
                if (response.Content != null)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Log($"Successful update of account details: '{acctNo}'", LogLevel.Information, null);
                        return Ok();
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Log("Encountered internal missing data generation", LogLevel.Critical, null);
                        throw new DataRetrievalFailException("Internal error missing data");
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        Log("Encountered internal missing/mismatch data", LogLevel.Warning, null);
                        throw new DataRetrievalFailException("Internal poor request occurred");
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                    {
                        Log("Encountered internal concurrency conflict", LogLevel.Warning, null);
                        throw new DataRetrievalFailException("Internal concurrency conflict occurred");
                    }
                }
                throw new DataRetrievalFailException("Internal unkown exception occurred/Failed to get a response from Data tier");
            }
            catch (DataRetrievalFailException e)
            {
                Log(null, LogLevel.Warning, e);
                return NotFound(e.Message);
            }
        }

        [HttpGet("getaccounts")]
        public IActionResult GetAllAccounts()
        {
            try
            {
                Log("Connect to the Data tier web server", LogLevel.Information, null);
                RestClient client = new RestClient(_dataServerApiUrl);
                RestRequest request = new RestRequest($"/api/account", Method.Get);
                RestResponse response = client.Execute(request);

                List<BankAccount>? value = null;

                Log("Attempt to retrieve all accounts", LogLevel.Information, null);
                if (response.Content != null)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    { 
                        Log($"Successful retrieval of accounts", LogLevel.Information, null);
                        value = JsonConvert.DeserializeObject<List<BankAccount>>(response.Content);
                        value?.ForEach(bankAccount => Console.WriteLine(bankAccount.AcctNo));
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Log("Encountered internal missing data generation", LogLevel.Critical, null);
                        throw new DataRetrievalFailException("Internal DatabaseGenerationFailException occurred");
                    }
                }

                if (value != null)
                {
                    Log($"Successful deserialization of accounts", LogLevel.Information, null);
                    return Ok(value);
                }

                throw new DataRetrievalFailException("Internal unkown exception occurred/Failed to get a response from Data tier");
            }
            catch (DataRetrievalFailException ex)
            {
                Log(null, LogLevel.Warning, ex);
                return NotFound(ex.Message);
            }
        }

        [HttpGet("getusers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                Log("Connect to the Data tier web server", LogLevel.Information, null);
                RestClient client = new RestClient(_dataServerApiUrl);
                RestRequest request = new RestRequest($"/api/userprofile", Method.Get);
                RestResponse response = client.Execute(request);

                List<UserProfile>? value = null;

                Log("Attempt to retrieve all users", LogLevel.Information, null);
                if (response.Content != null)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Log($"Successful retrieval of userprofiles", LogLevel.Information, null);
                        value = JsonConvert.DeserializeObject<List<UserProfile>>(response.Content);
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Log("Encountered internal missing data generation", LogLevel.Critical, null);
                        throw new DataRetrievalFailException("Internal DatabaseGenerationFailException occurred");
                    }
                }

                if (value != null)
                {
                    Log($"Successful deserialization of userprofiles", LogLevel.Information, null);
                    return Ok(value);
                }

                throw new DataRetrievalFailException("Internal unkown exception occurred/Failed to get a response from Data tier");
            }
            catch (DataRetrievalFailException ex)
            {
                Log(null, LogLevel.Warning, ex);
                return NotFound(ex.Message);
            }
        }

        [HttpGet("getuserhistories")]
        public IActionResult GetAllHistories()
        {
            try
            {
                Log("Connect to the Data tier web server", LogLevel.Information, null);
                RestClient client = new RestClient(_dataServerApiUrl);
                RestRequest request = new RestRequest($"/api/admin/userhistory", Method.Get);
                RestResponse response = client.Execute(request);

                List<UserHistory>? value = null;

                Log("Attempt to retrieve all transactions", LogLevel.Information, null);
                if (response.Content != null)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Log($"Successful retrieval of histories", LogLevel.Information, null);
                        value = JsonConvert.DeserializeObject<List<UserHistory>>(response.Content);
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Log("Encountered internal missing data generation", LogLevel.Critical, null);
                        throw new DataRetrievalFailException("Internal DatabaseGenerationFailException occurred");
                    }
                }

                if (value != null)
                {
                    Log($"Successful deserialization of transactions", LogLevel.Information, null);
                    return Ok(value);
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
