using BankDataLB;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace BusinessTierWebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : Controller
    {
        private readonly string _dataServerApiUrl = "http://localhost:5181";

        // GET: api/userprofile/byname/{name}
        [HttpGet("name/{name}")]
        public IActionResult GetUserProfileByName(string name)
        {
            RestClient client = new RestClient(_dataServerApiUrl);
            RestRequest request = new RestRequest($"/api/userprofile/byname/{name}", Method.Get);
            RestResponse response = client.Execute(request);
            UserProfile? value = JsonConvert.DeserializeObject<UserProfile>(response.Content);
            return Ok(value);
        }

        // GET: api/userprofile/byemail/{email}
        [HttpGet("email/{email}")]
        public IActionResult GetUserProfileById(string email)
        {
            RestClient client = new RestClient(_dataServerApiUrl);
            RestRequest request = new RestRequest($"/api/userprofile/byemail/{email}", Method.Get);
            RestResponse response = client.Execute(request);
            UserProfile? value = JsonConvert.DeserializeObject<UserProfile>(response.Content);
            return Ok(value);
        }

        // Put: api/userprofile/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUserProfile(int id, [FromBody] UserProfile updatedProfile)
        {
            RestClient client = new RestClient(_dataServerApiUrl);
            RestRequest request = new RestRequest($"/api/userprofile/{id}", Method.Put);
            request.AddJsonBody(updatedProfile); 
            RestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                UserProfile? value = JsonConvert.DeserializeObject<UserProfile>(response.Content);
                return Ok(value);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.Content);
            }
        }

        // Post: api/userprofile
        [HttpPost]
        public IActionResult AddUserProfile([FromBody] UserProfile userProfile)
        {
            RestClient client = new RestClient(_dataServerApiUrl);
            RestRequest request = new RestRequest("/api/userprofile", Method.Post);
            request.AddJsonBody(userProfile);
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

        // Delete: api/userprofile/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUserProfile(int id)
        {
            RestClient client = new RestClient(_dataServerApiUrl);
            RestRequest request = new RestRequest($"/api/userprofile/{id}", Method.Delete);
            RestResponse response = client.Execute(request);
            UserProfile? value = JsonConvert.DeserializeObject<UserProfile>(response.Content);
            return Ok();
        }
    }
}
