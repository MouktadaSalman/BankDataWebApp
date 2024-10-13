/*
 * Module: UserProfileController
 * Description: Handles API requests related to user profile management, including retrieval, creation, updating, and deletion of user profiles
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
    public class UserProfileController : Controller
    {
        // URL for the Data Tier API
        private readonly string _dataServerApiUrl = "http://localhost:5181";

        /*
         * Method: GetAllUserProfiles
         * Description: Retrieves all user profiles to determine the number of users that exist, useful for assigning new IDs
         * Params: None
         * Use: GET: api/userprofile
         */
        [HttpGet]
        public IActionResult GetAllUserProfiles()
        {
            try
            {
                // Create a RestClient to interact with the Data Tier API
                RestClient client = new RestClient(_dataServerApiUrl);

                // Prepare a GET request to retrieve all user profiles
                RestRequest request = new RestRequest("/api/userprofile", Method.Get);

                // Execute the request and get the response
                RestResponse response = client.Execute(request);

                // Handle unsuccessful responses
                if (!response.IsSuccessful)
                {
                    return StatusCode(500, "Error retrieving user profiles");
                }

                // Deserialize the response content into a list of UserProfile objects
                List<UserProfile>? userProfiles = JsonConvert.DeserializeObject<List<UserProfile>>(response.Content);

                return Ok(userProfiles);
            }
            catch (Exception)
            {
                // Return an error response in case of an exception
                return StatusCode(500, "Internal server error.");
            }
        }

        /*
         * Method: GetUserProfileByName
         * Description: Retrieves a user profile by the user's name
         * Params:
         *   name: The name of the user to search for
         * Use: GET: api/userprofile/name/{name}
         */
        [HttpGet("name/{name}")]
        public IActionResult GetUserProfileByName(string name)
        {
            // Create a RestClient to interact with the Data Tier API
            RestClient client = new RestClient(_dataServerApiUrl);

            // Prepare a GET request to retrieve the user profile by name
            RestRequest request = new RestRequest($"/api/userprofile/byname/{name}", Method.Get);

            // Execute the request and get the response
            RestResponse response = client.Execute(request);

            // Deserialize the response content into a UserProfile object
            UserProfile? value = JsonConvert.DeserializeObject<UserProfile>(response.Content);
            return Ok(value);
        }

        /*
         * Method: GetUserProfileById
         * Description: Retrieves a user profile by the user's email
         * Params:
         *   email: The email of the user to search for
         * Use: GET: api/userprofile/email/{email}
         */
        [HttpGet("email/{email}")]
        public IActionResult GetUserProfileById(string email)
        {
            // Create a RestClient to interact with the Data Tier API
            RestClient client = new RestClient(_dataServerApiUrl);

            // Prepare a GET request to retrieve the user profile by email
            RestRequest request = new RestRequest($"/api/userprofile/byemail/{email}", Method.Get);

            // Execute the request and get the response
            RestResponse response = client.Execute(request);


            // Deserialize the response content into a UserProfile object
            UserProfile? value = JsonConvert.DeserializeObject<UserProfile>(response.Content);
            return Ok(value);
        }

        /*
         * Method: UpdateUserProfile
         * Description: Updates an existing user profile by sending a PUT request to the Data Tier
         * Params:
         *   id: The ID of the user to update
         *   updatedProfile: The updated user profile details
         * Use: PUT: api/userprofile/{id}
         */
        [HttpPut("{id}")]
        public IActionResult UpdateUserProfile(int id, [FromBody] UserProfile updatedProfile)
        {
            // Create a RestClient to interact with the Data Tier API
            RestClient client = new RestClient(_dataServerApiUrl);

            // Prepare a PUT request to update the user profile by ID
            RestRequest request = new RestRequest($"/api/userprofile/{id}", Method.Put);

            // Attach the updated user profile as a JSON body
            request.AddJsonBody(updatedProfile);


            // Execute the request and get the response
            RestResponse response = client.Execute(request);

            // If successful, return the updated profile; otherwise, return an error status code
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

        /*
         * Method: AddUserProfile
         * Description: Creates a new user profile by sending a POST request to the Data Tier
         * Params:
         *   userProfile: The user profile details to be added
         * Use: POST: api/userprofile
         */
        [HttpPost]
        public IActionResult AddUserProfile([FromBody] UserProfile userProfile)
        {
            // Create a RestClient to interact with the Data Tier API
            RestClient client = new RestClient(_dataServerApiUrl);

            // Prepare a POST request to add a new user profile
            RestRequest request = new RestRequest("/api/userprofile", Method.Post);

            //Attach the user profile as a JSON body
            request.AddJsonBody(userProfile);

            // Execute the request and get the response
            RestResponse response = client.Execute(request);

            // If successful, return OK, else return an error status code
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
         * Method: DeleteUserProfile
         * Description: Deletes an existing user profile by sending a DELETE request to the Data Tier
         * Params:
         *   id: The ID of the user to delete
         * Use: DELETE: api/userprofile/{id}
         */
        [HttpDelete("{id}")]
        public IActionResult DeleteUserProfile(int id)
        {
            // Create a RestClient to interact with the Data Tier API
            RestClient client = new RestClient(_dataServerApiUrl);

            // Prepare a DELETE request to remove the user profile by ID
            RestRequest request = new RestRequest($"/api/userprofile/{id}", Method.Delete);

            // Execute the request and get the response
            RestResponse response = client.Execute(request);


            UserProfile? value = JsonConvert.DeserializeObject<UserProfile>(response.Content);
            return Ok();
        }
    }
}
