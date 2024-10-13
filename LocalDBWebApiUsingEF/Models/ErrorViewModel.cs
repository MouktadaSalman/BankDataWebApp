/* 
 * Module: ErrorViewModel
 * Description: Contains the ErrorViewModel class for handling error information
 * Author: Jauhar
 * ID: 21494299
 * Version: 1.0.0.1
 */

namespace DataTierWebServer.Models
{
    public class ErrorViewModel
    {
        // Unique identifier for the request that resulted in an error
        public string? RequestId { get; set; }

        // Indicates whether the RequestId should be displayed
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}