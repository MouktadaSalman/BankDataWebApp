/*
 * Module: BankAccountController
 * Description: Handles API requests related to bank account management, communicates with Data Tier for retrieving and updating bank accounts
 * Author: Ahmed, Moukhtada
 * ID: 21467369, 20640266
 * Version: 1.0.0.1
 */

using BankDataLB;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace BusinessTierWebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : Controller
    {
        // URL for the Data Tier API
        private readonly string _dataServerApiUrl = "http://localhost:5181";

        /*
         * Method: GetBankAccounts
         * Description: Retrieves bank accounts associated with a specific user ID by making a request to the Data Tier
         * Params:
         *   Id: The user ID to search for
         * Use: GET: api/bankaccount/{Id}
         */
        [HttpGet("{Id}")]
        public IActionResult GetBankAccounts(int Id)
        {
            // Create a RestClient to interact with the Data Tier API
            RestClient client = new RestClient(_dataServerApiUrl);

            // Prepare a GET request to retrieve accounts for the provided user ID
            RestRequest request = new RestRequest($"/api/account/{Id}", Method.Get);

            // Execute the request and get the response
            RestResponse response = client.Execute(request);

            // Handle unsuccessful responses
            if (!response.IsSuccessful)
            {
                return StatusCode((int)response.StatusCode, response.ErrorMessage);
            }

            // Handle empty content response
            if (string.IsNullOrEmpty(response.Content))
            {
                return NotFound("No content received from the data server.");
            }

            try
            {
                // Deserialize the response content into a list of BankAccount objects
                List<BankAccount>? value = JsonConvert.DeserializeObject<List<BankAccount>>(response.Content);

                // Print each bank account to the console (for debugging)
                value?.ForEach(bankAccount => Console.WriteLine(bankAccount));
                return Ok(value);
            }
            catch (JsonException ex)
            {
                // Return an error response in case of JSON parsing failure
                return StatusCode(500, $"JSON parsing error: {ex.Message}");
            }
        }

        /*
         * Method: AddBankAccount
         * Description: Adds a new bank account by making a POST request to the Data Tier
         * Params:
         *   bankAccount: The bank account details to be added
         * Use: POST: api/bankaccount
         */
        [HttpPost]
        public IActionResult AddBankAccount([FromBody]BankAccount bankAccount)
        {
            // Create a RestClient to interact with the Data Tier API
            RestClient client = new RestClient(_dataServerApiUrl);

            // Prepare a POST request to add a new bank account
            RestRequest request = new RestRequest($"/api/account", Method.Post);

            // Attach the bank account object as a JSON body
            request.AddJsonBody(bankAccount);
            // Execute the request and get the response
            RestResponse response = client.Execute(request);

            // Return OK if the request was successful, otherwise return an error status code
            if (response.IsSuccessful)
            {
                return Ok();
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ErrorMessage);
            }
        }

        /*
         * Method: GetHistory
         * Description: Retrieves the transaction history for a specific account from the Data Tier
         * Params:
         *   acctNo: The account number to retrieve history for
         *   startDate: Optional start date for filtering history
         *   endDate: Optional end date for filtering history
         * Use: GET: api/bankaccount/history/{acctNo}
         */
        [HttpGet("history/{acctNo}")]
        public IActionResult GetHistory(uint acctNo, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            // Build the query for the Data Tier API
            string apiUrl = $"/api/account/history/{acctNo}";

            // Append startDate and endDate as query parameters if they are provided
            if (startDate.HasValue || endDate.HasValue)
            {
                apiUrl += "?";
                if (startDate.HasValue)
                {
                    apiUrl += $"startDate={startDate.Value:yyyy-MM-dd}&";
                }
                if (endDate.HasValue)
                {
                    apiUrl += $"endDate={endDate.Value:yyyy-MM-dd}";
                }
            }

            // Create a RestClient to interact with the Data Tier API
            RestClient client = new RestClient(_dataServerApiUrl);

            // Prepare a GET request with the query URL
            RestRequest request = new RestRequest(apiUrl, Method.Get);

            // Execute the request and get the response
            RestResponse response = client.Execute(request);

            // Handle unsuccessful or empty responses
            if (!response.IsSuccessful || response.Content == null)
            {
                return StatusCode((int)response.StatusCode, "Failed to retrieve account data from the data server.");
            }

            // Deserialize the response content into a list of transaction history strings
            var value = JsonConvert.DeserializeObject<List<string>>(response.Content);
            if (value == null)
            {
                return NotFound("Account not found.");
            }

            return Ok(value);
        }

        /*
         * Method: Transaction
         * Description: Updates the balance of an account by making a transaction (deposit, withdrawal, send, receive)
         * Params:
         *   type: The type of transaction (deposit, withdraw, send, receive)
         *   acctNo: The account number to perform the transaction on
         *   amount: The amount to update the balance by
         * Use: PUT: api/bankaccount/{type}/{acctNo}/{amount}
         */
        [HttpPut("{type}/{acctNo}/{amount}")]
        public IActionResult Transaction(string type, uint acctNo, int amount)
        {
            // Create a RestClient to interact with the Data Tier API
            RestClient client = new RestClient(_dataServerApiUrl);

            // Prepare a PUT request to perform the transaction
            RestRequest request = new RestRequest($"/api/account/{type}/{acctNo}/{amount}", Method.Put);

            // Execute the request and get the response
            RestResponse response = client.Execute(request);

            // Return a success message if the transaction was successful, otherwise return an error status code
            if (response.IsSuccessful)
            {
                return Ok($"Successful transaction of amount :{amount}");
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ErrorMessage);
            }
        }
    }
}
