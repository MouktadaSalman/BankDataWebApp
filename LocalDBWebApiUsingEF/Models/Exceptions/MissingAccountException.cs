/*
 * Module: MissingAccountException
 * Description: Custom exception for handling cases where a user account is missing
 * Author: Jauhar
 * ID: 21494299
 * Version: 1.0.0.1
 */

namespace DataTierWebServer.Models.Exceptions
{
    public class MissingAccountException : Exception
    {
        // Base message for all instances of this exception
        private const string BaseMessage = "User account does not exist for requested: ";


        /*
         * Method: MissingAccountException
         * Description: Constructor for the MissingAccountException class
         * Params:
         *   name: The name for which the user account is missing
         */
        public MissingAccountException(string name) : base(BaseMessage + name) { }


        /*
         * Method: MissingAccountException
         * Description: Constructor for the MissingAccountException class with inner exception
         * Params:
         *   name: The name for which the user account is missing
         *   innerException: The inner exception that caused this error
         */
        public MissingAccountException(string name, Exception innerException) : base(BaseMessage + name, innerException) { }
    }
}
