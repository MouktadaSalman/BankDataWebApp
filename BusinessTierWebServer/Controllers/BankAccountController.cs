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
        private readonly string _dataServerApiUrl = "http://localhost:5181";

        [HttpGet("{Id}")]
        public IActionResult GetBankAccounts(int Id)
        {
            RestClient client = new RestClient(_dataServerApiUrl);
            RestRequest request = new RestRequest($"/api/account/{Id}", Method.Get);
            RestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                return StatusCode((int)response.StatusCode, response.ErrorMessage);
            }

            if (string.IsNullOrEmpty(response.Content))
            {
                return NotFound("No content received from the data server.");
            }

            try
            {
                List<BankAccount>? value = JsonConvert.DeserializeObject<List<BankAccount>>(response.Content);
                value?.ForEach(bankAccount => Console.WriteLine(bankAccount));
                return Ok(value);
            }
            catch (JsonException ex)
            {
                return StatusCode(500, $"JSON parsing error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult AddBankAccount([FromBody]BankAccount bankAccount)
        {
            RestClient client = new RestClient(_dataServerApiUrl);
            RestRequest request = new RestRequest($"/api/account", Method.Post);
            request.AddJsonBody(bankAccount);
            RestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                return Ok();
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ErrorMessage);
            }
        }

        // GET: api/bankaccount/{acctno}
        //[HttpGet("{Id}")]
        //public IActionResult GetAccountsById(int Id)
        //{
        //    RestClient client = new RestClient(_dataServerApiUrl);
        //    RestRequest request = new RestRequest($"/api/account/{Id}", Method.Get);
        //    RestResponse response = client.Execute(request);
        //    BankAccount? value = JsonConvert.DeserializeObject<BankAccount>(response.Content);
        //    return Ok(value);
        //}

        // GET: api/bankaccount/history/{acctno}
        [HttpGet("history/{acctno}")]
        public IActionResult GetHistory(uint acctNo)
        {
            RestClient client = new RestClient(_dataServerApiUrl);
            RestRequest request = new RestRequest($"/api/account/history/{acctNo}", Method.Get);
            RestResponse response = client.Execute(request);

            if (!response.IsSuccessful || response.Content == null)
            {
                return StatusCode((int)response.StatusCode, "Failed to retrieve account data from the data server.");
            }

            var value = JsonConvert.DeserializeObject<List<string>>(response.Content);
            if (value == null)
            {
                return NotFound("Account not found.");
            }

            return Ok(value);
        }

        // Put: api/bankaccount/{acctNo}/{amount}
        [HttpPut("{type}/{acctNo}/{amount}")]
        public IActionResult Transaction(string type, uint acctNo, int amount)
        {
            RestClient client = new RestClient(_dataServerApiUrl);
            RestRequest request = new RestRequest($"/api/account/{type}/{acctNo}/{amount}", Method.Put);
            RestResponse response = client.Execute(request);

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
