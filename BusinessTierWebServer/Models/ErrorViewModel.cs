/*
 * Module: ErrorViewModel
 * Description: ViewModel for representing errors in the web application, including request ID information
 * Author: Jauhar
 * ID: 21494299
 * Version: 1.0.0.1
 */

namespace BusinessTierWebServer.Models
{
    public class ErrorViewModel
    {
        // Unique identifier for the request that caused the error, can be null
        public string? RequestId { get; set; }

        // Boolean property that returns true if RequestId is not null or empty
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
