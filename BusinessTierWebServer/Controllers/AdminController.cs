/* 
 * Module: AdminController
 * Description: Handles HTTP requests related to admin operations
 * Author: Jauhar
 * ID: 21494299
 * Version: 1.0.0.1
 */


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

        // URL for the Data Tier API
        private readonly string _dataServerApiUrl = "http://localhost:5181";

        // Initialising Logger and lock of the AdminController
        private readonly ILogger<AdminController> _logger;
        private static readonly object _logLock = new object();

        /*
         * Method: AdminController
         * Description: Constructor for the AdminController class, initializing the logger
         * Params:
         *   logger: ILogger instance used for logging
         */
        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }


        /*
         * Method: Log
         * Description: method for logging messages and exceptions
         * Params:
         *   message: The log message
         *   logLevel: The severity of the log
         *   ex: Optional exception to log
         */
        private void Log(string? message, LogLevel logLevel, Exception? ex)
        {
            lock (_logLock)
            {
                // Log the exception or the message with the timestamp
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


        /*
        * Method: GetAdminByName
        * Description: Retrieves admin details by name
        * Params:
        *   name: The name of the admin
        * Use: GET: api/admin/name/{name}
        */
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetAdminByName(string name)
        {
            try
            {
                Log("Connect to the Data tier web server", LogLevel.Information, null);

                // Create a RestClient for the data server URL
                RestClient client = new RestClient(_dataServerApiUrl);

                // Prepare the request to fetch admin by name
                RestRequest request = new RestRequest($"/api/admin/byname/{name}", Method.Get);

                // Execute the request and await the response asynchronously
                RestResponse response = await client.ExecuteAsync(request);

                Log($"Attempt to retrieve admin details: '{name}'", LogLevel.Information, null);


                // Check if the response contains any content
                if (response.Content != null)
                {
                    // If the response is successful, deserialize the content into an Admin object
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Admin? value = JsonConvert.DeserializeObject<Admin>(response.Content);

                        Log($"Successful retrieval of admin details: '{name}'", LogLevel.Information, null);
                        return Ok(value); //Returning the admin details
                    }

                    // Handling the case where the admin is not found
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Log("Encountered internal missing data generation", LogLevel.Critical, null);
                        throw new DataRetrievalFailException("Internal DatabaseGenerationFailException occurred");
                    }

                    // Handling the case where the request is successful but there's no content
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        throw new DataRetrievalFailException("Internal MissingProfileException occurred");
                    }
                }

                // Handling unknown failures to get a response
                throw new DataRetrievalFailException("Internal unkown exception occurred/Failed to get a response from Data tier");

            }
            catch (DataRetrievalFailException ex)
            {
                // Log the exception and return a NotFound response
                Log(null, LogLevel.Warning, ex);
                return NotFound(ex.Message);
            }
        }

        /*
         * Method: GetAdminByEmail
         * Description: Retrieves admin details by email
         * Params:
         *   email: The email of the admin
         * Use: GET: api/admin/email/{email}
         */
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetAdminByEmail(string email)
        {
            try
            {
                Log("Connect to the Data tier web server", LogLevel.Information, null);

                // Create a RestClient for the data server URL
                RestClient client = new RestClient(_dataServerApiUrl);

                // Prepare the request to fetch admin by email
                RestRequest request = new RestRequest($"/api/admin/byemail/{email}", Method.Get);

                // Execute the request and await the response asynchronously
                RestResponse response = await client.ExecuteAsync(request);

                Log($"Attempt to retrieve admin details: '{email}'", LogLevel.Information, null);

                // Check if the response contains any content
                if (response.Content != null)
                {

                    // If the response is successful, deserialize the content into an Admin object
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Admin? value = JsonConvert.DeserializeObject<Admin>(response.Content);

                        Log($"Successful retrieval of admin details: '{email}'", LogLevel.Information, null);
                        return Ok(value);  // Return the admin details
                    }

                    // Handling the case where the admin is not found
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Log("Encountered internal missing data generation", LogLevel.Critical, null);
                        throw new DataRetrievalFailException("Internal DatabaseGenerationFailException occurred");
                    }

                    // Handling the case where the request is successful but there's no content
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        throw new DataRetrievalFailException("Internal MissingProfileException occurred");
                    }
                }

                // Handle unknown failures to get a response
                throw new DataRetrievalFailException("Internal unkown exception occurred/Failed to get a response from Data tier");

            }
            catch (DataRetrievalFailException ex)
            {
                // Log the exception and return a NotFound response
                Log(null, LogLevel.Warning, ex);
                return NotFound(ex.Message);
            }
        }

        /*
         * Method: GetAdminById
         * Description: Retrieves admin details by ID
         * Params:
         *   id: The ID of the admin
         * Use: GET: api/admin/id/{id}
         */
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetAdminById(int id)
        {
            try
            {
                Log("Connect to the Data tier web server", LogLevel.Information, null);

                // Create a RestClient for the data server URL
                RestClient client = new RestClient(_dataServerApiUrl);

                // Prepare the request to fetch admin by ID
                RestRequest request = new RestRequest($"/api/admin/byid/{id}", Method.Get);

                // Execute the request and await the response asynchronously
                RestResponse response = await client.ExecuteAsync(request);

                Log($"Attempt to retrieve admin details: '{id}'", LogLevel.Information, null);

                // Check if the response contains any content
                if (response.Content != null)
                {
                    // If the response is successful, deserialize the content into an Admin object
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Admin? value = JsonConvert.DeserializeObject<Admin>(response.Content);

                        Log($"Successful retrieval of admin details: '{id}'", LogLevel.Information, null);
                        return Ok(value); // Return the admin details
                    }

                    // Handling the case where the admin is not found
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Log("Encountered internal missing data generation", LogLevel.Critical, null);
                        throw new DataRetrievalFailException("Internal DatabaseGenerationFailException occurred");
                    }

                    // Handling the case where the request is successful but there's no content
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        throw new DataRetrievalFailException("Internal MissingProfileException occurred");
                    }
                }

                // Handling unknown failures to get a response
                throw new DataRetrievalFailException("Internal unkown exception occurred/Failed to get a response from Data tier");

            }
            catch (DataRetrievalFailException ex)
            {
                // Log the exception and return a NotFound response
                Log(null, LogLevel.Warning, ex);
                return NotFound(ex.Message);
            }
        }

        /*
         * Method: AddBankAccount
         * Description: Adds a new bank account for an admin by sending a POST request to the Data Tier
         * Params:
         *   bankAccount: The bank account details to be created
         * Use: POST: api/admin/createaccount
         */
        [HttpPost("createaccount")]
        public async Task<IActionResult> AddBankAccount([FromBody] BankAccount bankAccount)
        {
            try
            {
                Log("Connect to the Data tier web server", LogLevel.Information, null);

                // Create a RestClient for the data server URL
                RestClient client = new RestClient(_dataServerApiUrl);

                // Prepare the POST request to add a new bank account
                RestRequest request = new RestRequest("/api/account/addaccount", Method.Post);

                // Add the bank account data to the request body
                request.AddJsonBody(bankAccount);

                // Execute the request and await the response asynchronously
                RestResponse response = await client.ExecuteAsync(request);

                Log($"Attempt to create account: '{bankAccount.AcctNo}'", LogLevel.Information, null);

                // Check if the response contains any content
                if (response.Content != null)
                {
                    // If the response is successful, return OK
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Log($"Successful creation of account: '{bankAccount.AcctNo}'", LogLevel.Information, null);
                        return Ok();
                    }

                    // Handling the case where the account creation failed due to missing data
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Log("Encountered internal missing data generation", LogLevel.Critical, null);
                        throw new DataRetrievalFailException("Internal error missing data");
                    }

                    // Handling the case where the request failed due to a bad request
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        Log("Encountered internal missing/mismatch data", LogLevel.Warning, null);
                        throw new DataRetrievalFailException("Internal poor request occurred");
                    }

                    // Handling the case where there is a concurrency conflict
                    if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                    {
                        Log("Encountered internal concurrency conflict", LogLevel.Warning, null);
                        throw new DataRetrievalFailException("Internal concurrency conflict occurred");
                    }
                }

                // Handling unknown failures to get a response
                throw new DataRetrievalFailException("Internal unkown exception occurred/Failed to get a response from Data tier");
            }
            catch (DataRetrievalFailException e)
            {
                Log(null, LogLevel.Warning, e);
                return NotFound(e.Message);
            }
        }

        /*
         * Method: UpdateAdminProfile
         * Description: Updates an admin profile using the provided admin ID and new admin details.
         * Params:
         *   id: The ID of the admin to be updated.
         *   updatedAdmin: The updated admin details.
         * Use: PUT: api/admin/update/{id}
         */
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAdminProfile(int id, [FromBody] Admin updatedAdmin)
        {
            try
            {
                Log("Connect to the Data tier web server", LogLevel.Information, null);

                // Create a RestClient to connect to the data server
                RestClient client = new RestClient(_dataServerApiUrl);

                // Create a PUT request to update admin details by ID
                RestRequest request = new RestRequest($"/api/admin/update/{id}", Method.Put);

                // Add the updated admin details to the request body
                request.AddJsonBody(updatedAdmin);

                // Execute the request and await the response
                RestResponse response = await client.ExecuteAsync(request);

                Log($"Attempt to update admin details id: '{id}'", LogLevel.Information, null);

                // Check if the response contains any content
                if (response.Content != null)
                {
                    // If successful, deserialize the response into an Admin object
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Admin? value = JsonConvert.DeserializeObject<Admin>(response.Content);

                        Log($"Successful retrieval of updated admin details: '{id}'", LogLevel.Information, null);
                        return Ok(value); // Return the updated admin details
                    }

                    // Handle case where admin data is not found
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Log("Encountered internal missing data generation", LogLevel.Critical, null);
                        throw new DataRetrievalFailException("Internal error missing data");
                    }

                    // Handle bad request scenario
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        Log("Encountered internal missing/mismatch data", LogLevel.Warning, null);
                        throw new DataRetrievalFailException("Internal poor request occurred");
                    }

                    // Handle concurrency conflict scenario
                    if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                    {
                        Log("Encountered internal concurrency conflict", LogLevel.Warning, null);
                        throw new DataRetrievalFailException("Internal concurrency conflict occurred");
                    }
                }

                // Handle unknown failures to get a response
                throw new DataRetrievalFailException("Internal unkown exception occurred/Failed to get a response from Data tier");
            }
            catch (DataRetrievalFailException e)
            {
                // Log and return the exception message if the request fails
                Log(null, LogLevel.Warning, e);
                return NotFound(e.Message);
            }
        }

        /*
         * Method: GetAccountByNo
         * Description: Retrieves account details using the account number.
         * Params:
         *   acctNo: The account number to search for.
         * Use: GET: api/admin/no/{acctNo}
         */
        [HttpGet("no/{acctNo}")]
        public async Task<IActionResult> GetAccountByNo(uint acctNo)
        {
            try
            {
                Log("Connect to the Data tier web server", LogLevel.Information, null);

                // Create a RestClient to connect to the data server
                RestClient client = new RestClient(_dataServerApiUrl);

                // Create a GET request to retrieve account details by account number
                RestRequest request = new RestRequest($"/api/admin/byno/{acctNo}", Method.Get);

                // Execute the request and await the response asynchronously
                RestResponse response = await client.ExecuteAsync(request);

                Log($"Attempt to retrieve account details: '{acctNo}'", LogLevel.Information, null);

                // Check if the response contains any content
                if (response.Content != null)
                {
                    // If the response is successful, deserialize the response content into a BankAccount object
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        BankAccount? value = JsonConvert.DeserializeObject<BankAccount>(response.Content);

                        Log($"Successful retrieval of account details: '{acctNo}'", LogLevel.Information, null);
                        return Ok(value); // Return the account details
                    }

                    // Handle case where the account is not found
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Log("Encountered internal missing data generation", LogLevel.Critical, null);
                        throw new DataRetrievalFailException("Internal DatabaseGenerationFailException occurred");
                    }

                    // Handle case where no content is returned
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        throw new DataRetrievalFailException("Internal MissingAccountException occurred");
                    }
                }

                // Handling
                throw new DataRetrievalFailException("Internal unkown exception occurred/Failed to get a response from Data tier");

            }
            catch (DataRetrievalFailException ex)
            {
                // Log and return the exception message if the request fails
                Log(null, LogLevel.Warning, ex);
                return NotFound(ex.Message);
            }
        }

        /*
         * Method: UpdateAccountByNo
         * Description: Updates account details using the account number.
         * Params:
         *   acctNo: The account number to update.
         *   updatedAccount: The updated account details to be saved.
         * Use: PUT: api/admin/updateaccount/{acctNo}
         */
        [HttpPut("updateaccount/{acctNo}")]
        public async Task<IActionResult> UpdateAccountByNo(uint acctNo, [FromBody] BankAccount updatedAccount)
        {
            try
            {
                Log("Connect to the Data tier web server", LogLevel.Information, null);

                // Create a RestClient to connect to the data server
                RestClient client = new RestClient(_dataServerApiUrl);

                // Create a PUT request to update account details by account number
                RestRequest request = new RestRequest($"/api/account/fromadmin/{acctNo}", Method.Put);

                // Add the updated account details to the request body
                request.AddJsonBody(updatedAccount);

                // Execute the request and await the response asynchronously
                RestResponse response = await client.ExecuteAsync(request);

                Log($"Attempt to update account details: '{acctNo}'", LogLevel.Information, null);

                // Check if the response contains any content
                if (response.Content != null)
                {
                    // If the response is successful, log and return OK
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Log($"Successful update of account details: '{acctNo}'", LogLevel.Information, null);
                        return Ok();
                    }

                    // Handle case where account data is not found
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Log("Encountered internal missing data generation", LogLevel.Critical, null);
                        throw new DataRetrievalFailException("Internal error missing data");
                    }

                    // Handle bad request scenario
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        Log("Encountered internal missing/mismatch data", LogLevel.Warning, null);
                        throw new DataRetrievalFailException("Internal poor request occurred");
                    }

                    // Handle concurrency conflict scenario
                    if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                    {
                        Log("Encountered internal concurrency conflict", LogLevel.Warning, null);
                        throw new DataRetrievalFailException("Internal concurrency conflict occurred");
                    }
                }

                // Handle unknown failures to get a response
                throw new DataRetrievalFailException("Internal unkown exception occurred/Failed to get a response from Data tier");
            }
            catch (DataRetrievalFailException e)
            {
                // Log and return the exception message if the request fails
                Log(null, LogLevel.Warning, e);
                return NotFound(e.Message);
            }
        }


        /*
         * Method: DeleteAccountByNo
         * Description: Deletes account details by the account number.
         * Params:
         *   acctNo: The account number to delete.
         * Use: DELETE: api/admin/deleteaccount/{acctNo}
         */
        [HttpDelete("deleteaccount/{acctNo}")]
        public async Task<IActionResult> DeleteAccountByNo(uint acctNo)
        {
            try
            {
                Log("Connect to the Data tier web server", LogLevel.Information, null);

                // Create a RestClient to connect to the data server
                RestClient client = new RestClient(_dataServerApiUrl);

                // Create a DELETE request to delete the account by account number
                RestRequest request = new RestRequest($"/api/account/fromadmin/{acctNo}", Method.Delete);

                // Execute the request and await the response asynchronously
                RestResponse response = await client.ExecuteAsync(request);

                Log($"Attempt to delete account details: '{acctNo}'", LogLevel.Information, null);

                // Check if the response contains any content
                if (response.Content != null)
                {
                    // If the response is successful, log and return OK
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Log($"Successful deletion of account details: '{acctNo}'", LogLevel.Information, null);
                        return Ok();
                    }

                    // Handle case where account data is not found
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Log("Encountered internal missing data generation", LogLevel.Critical, null);
                        throw new DataRetrievalFailException("Internal error missing data");
                    }

                    // Handle bad request scenario
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        Log("Encountered internal missing/mismatch data", LogLevel.Warning, null);
                        throw new DataRetrievalFailException("Internal poor request occurred");
                    }

                    // Handle concurrency conflict scenario
                    if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                    {
                        Log("Encountered internal concurrency conflict", LogLevel.Warning, null);
                        throw new DataRetrievalFailException("Internal concurrency conflict occurred");
                    }
                }

                // Handle unknown failures to get a response
                throw new DataRetrievalFailException("Internal unkown exception occurred/Failed to get a response from Data tier");
            }
            catch (DataRetrievalFailException e)
            {
                // Log and return the exception message if the request fails
                Log(null, LogLevel.Warning, e);
                return NotFound(e.Message);
            }
        }


        /*
         * Method: GetAllAccounts
         * Description: Retrieves all accounts from the data tier.
         * Params: None
         * Use: GET: api/admin/getaccounts
         */
        [HttpGet("getaccounts")]
        public async Task<IActionResult> GetAllAccounts()
        {
            try
            {
                Log("Connect to the Data tier web server", LogLevel.Information, null);

                // Create a RestClient to connect to the data server
                RestClient client = new RestClient(_dataServerApiUrl);

                // Create a GET request to retrieve all accounts
                RestRequest request = new RestRequest($"/api/account", Method.Get);

                // Execute the request and await the response asynchronously
                RestResponse response = await client.ExecuteAsync(request);

                List<BankAccount>? value = null;

                Log("Attempt to retrieve all accounts", LogLevel.Information, null);

                // Check if the response contains any content
                if (response.Content != null)
                {
                    // If the response is successful, deserialize the response content into a list of BankAccount objects
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Log($"Successful retrieval of accounts", LogLevel.Information, null);
                        value = JsonConvert.DeserializeObject<List<BankAccount>>(response.Content);
                        value?.ForEach(bankAccount => Console.WriteLine(bankAccount.AcctNo));
                    }

                    // Handle case where accounts data is not found
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Log("Encountered internal missing data generation", LogLevel.Critical, null);
                        throw new DataRetrievalFailException("Internal DatabaseGenerationFailException occurred");
                    }
                }

                // If the deserialization is successful, return the list of accounts
                if (value != null)
                {
                    Log($"Successful deserialization of accounts", LogLevel.Information, null);
                    return Ok(value);
                }

                // Handle unknown failures to get a response
                throw new DataRetrievalFailException("Internal unkown exception occurred/Failed to get a response from Data tier");
            }
            catch (DataRetrievalFailException ex)
            {
                // Log and return the exception message if the request fails
                Log(null, LogLevel.Warning, ex);
                return NotFound(ex.Message);
            }
        }


        /*
         * Method: GetAllUsers
         * Description: Retrieves all user profiles from the data tier.
         * Params: None
         * Use: GET: api/admin/getusers
         */
        [HttpGet("getusers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                Log("Connect to the Data tier web server", LogLevel.Information, null);

                // Create a RestClient to connect to the data server
                RestClient client = new RestClient(_dataServerApiUrl);

                // Create a GET request to retrieve all user profiles
                RestRequest request = new RestRequest($"/api/userprofile", Method.Get);

                // Execute the request and await the response asynchronously
                RestResponse response = await client.ExecuteAsync(request);

                List<UserProfile>? value = null;

                Log("Attempt to retrieve all users", LogLevel.Information, null);

                // Check if the response contains any content
                if (response.Content != null)
                {
                    // If the response is successful, deserialize the response content into a list of UserProfile objects
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Log($"Successful retrieval of userprofiles", LogLevel.Information, null);
                        value = JsonConvert.DeserializeObject<List<UserProfile>>(response.Content);
                    }

                    // Handle case where user profiles data is not found
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Log("Encountered internal missing data generation", LogLevel.Critical, null);
                        throw new DataRetrievalFailException("Internal DatabaseGenerationFailException occurred");
                    }
                }

                // If the deserialization is successful, return the list of user profiles
                if (value != null)
                {
                    Log($"Successful deserialization of userprofiles", LogLevel.Information, null);
                    return Ok(value);
                }

                // Handle unknown failures to get a response
                throw new DataRetrievalFailException("Internal unkown exception occurred/Failed to get a response from Data tier");
            }
            catch (DataRetrievalFailException ex)
            {
                // Log and return the exception message if the request fails
                Log(null, LogLevel.Warning, ex);
                return NotFound(ex.Message);
            }
        }


        /*
         * Method: GetAllHistories
         * Description: Retrieves all user transaction histories from the data tier.
         * Params: None
         * Use: GET: api/admin/getuserhistories
         */
        [HttpGet("getuserhistories")]
        public async Task<IActionResult> GetAllHistories()
        {
            try
            {
                Log("Connect to the Data tier web server", LogLevel.Information, null);

                // Create a RestClient to connect to the data server
                RestClient client = new RestClient(_dataServerApiUrl);

                // Create a GET request to retrieve all user transaction histories
                RestRequest request = new RestRequest($"/api/admin/userhistory", Method.Get);

                // Execute the request and await the response asynchronously
                RestResponse response = await client.ExecuteAsync(request);

                List<UserHistory>? value = null;

                Log("Attempt to retrieve all transactions", LogLevel.Information, null);

                // Check if the response contains any content
                if (response.Content != null)
                {
                    // If the response is successful, deserialize the response content into a list of UserHistory objects
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Log($"Successful retrieval of histories", LogLevel.Information, null);
                        value = JsonConvert.DeserializeObject<List<UserHistory>>(response.Content);
                    }

                    // Handle case where transaction histories data is not found
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Log("Encountered internal missing data generation", LogLevel.Critical, null);
                        throw new DataRetrievalFailException("Internal DatabaseGenerationFailException occurred");
                    }
                }

                // If the deserialization is successful, return the list of transaction histories
                if (value != null)
                {
                    Log($"Successful deserialization of transactions", LogLevel.Information, null);
                    return Ok(value);
                }

                // Handle unknown failures to get a response
                throw new DataRetrievalFailException("Internal unkown exception occurred/Failed to get a response from Data tier");
            }
            catch (DataRetrievalFailException ex)
            {
                // Log and return the exception message if the request fails
                Log(null, LogLevel.Warning, ex);
                return NotFound(ex.Message);
            }
        }
    }
}
