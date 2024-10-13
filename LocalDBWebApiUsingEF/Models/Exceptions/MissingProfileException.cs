/*
 * Module: MissingProfileException
 * Description: Custom exception for handling cases where a user profile is missing
 * Author: Jauhar
 * ID: 21494299
 * Version: 1.0.0.1
 */

using Microsoft.VisualBasic;

namespace DataTierWebServer.Models.Exceptions
{
    public class MissingProfileException : Exception
    {

        // Base message for all instances of this exception
        private const string BaseMessage = "User profile does not exist for requested: ";


        /*
         * Method: MissingProfileException
         * Description: Constructor for the MissingProfileException class
         * Params:
         *   name: The name for which the user profile is missing
         */
        public MissingProfileException(string name) : base(BaseMessage + name) { }

        /*
         * Method: MissingProfileException
         * Description: Constructor for the MissingProfileException class with inner exception
         * Params:
         *   name: The name for which the user profile is missing
         *   innerException: The inner exception that caused this error
         */
        public MissingProfileException(string name, Exception innerException) : base(BaseMessage + name, innerException) { }
    }
}
