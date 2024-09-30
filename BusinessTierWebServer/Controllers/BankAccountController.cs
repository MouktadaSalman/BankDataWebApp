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

        // GET: api/bankaccount/{acctno}
        [HttpGet("{acctno}")]
        public IActionResult GetUserProfile(uint acctNo)
        {
            RestClient client = new RestClient(_dataServerApiUrl);
            RestRequest request = new RestRequest($"/api/account/{acctNo}", Method.Get);
            RestResponse response = client.Execute(request);
            BankAccount? value = JsonConvert.DeserializeObject<BankAccount>(response.Content);
            return Ok(value);
        }

        // GET: api/bankaccount/history/{acctno}
        [HttpGet("history/{acctno}")]
        public IActionResult GetAccountById(uint acctNo)
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
        [HttpPut("{acctNo}/{amount}")]
        public IActionResult Transaction(uint acctNo, int amount)
        {
            RestClient client = new RestClient(_dataServerApiUrl);
            RestRequest request = new RestRequest($"/api/account/{acctNo}/{amount}", Method.Put);
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
