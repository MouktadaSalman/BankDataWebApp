using BankDataLB;
using BusinessTierWebServer.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Xml.Linq;

namespace BusinessTierWebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly string _dataServerApiUrl = "http://localhost:5181";

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        // GET: api/userprofile/byname/{name}
        [HttpGet("{name}")]
        public IActionResult GetAdminByName(string name)
        {
            try
            {
                RestClient client = new RestClient(_dataServerApiUrl);
                RestRequest request = new RestRequest($"/api/admin/byname/{name}", Method.Get);
                RestResponse response = client.Execute(request);

                if(response.Content != null)
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Admin? value = JsonConvert.DeserializeObject<Admin>(response.Content);

                        return Ok(value);
                    }

                    if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Exception? ex = JsonConvert.DeserializeObject<Exception>(response.Content);

                        throw new DataRetrievalFailException("'GetAdminByName'", ex);
                    }
                }

                throw new DataRetrievalFailException("'GetAdminByName' [Bad Request] ");

            }catch (DataRetrievalFailException ex)
            {
                _logger.LogWarning(ex, $"{DateTime.Now.ToString()}: {ex.Message}");
                return NotFound(ex.Message);
            }
        }

        // GET: api/userprofile/byemail/{email}
        [HttpGet("{email}")]
        public IActionResult GetAdminById(string email)
        {
            try
            {
                RestClient client = new RestClient(_dataServerApiUrl);
                RestRequest request = new RestRequest($"/api/admin/byemail/{email}", Method.Get);
                RestResponse response = client.Execute(request);

                if (response.Content != null)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Admin? value = JsonConvert.DeserializeObject<Admin>(response.Content);

                        return Ok(value);
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Exception? ex = JsonConvert.DeserializeObject<Exception>(response.Content);

                        throw new DataRetrievalFailException("'GetAdminByName'", ex);
                    }
                }

                throw new DataRetrievalFailException("[Bad Request]");

            }
            catch (DataRetrievalFailException ex)
            {
                _logger.LogWarning(ex, $"{DateTime.Now.ToString()}: {ex.Message}");
                return NotFound(ex.Message);
            }
        }
    }
}
