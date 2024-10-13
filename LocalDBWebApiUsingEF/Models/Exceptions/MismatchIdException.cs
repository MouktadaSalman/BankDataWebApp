/*
 * Module: MismatchIdException
 * Description: Custom exception for handling cases where IDs do not match in the system
 * Author: Jauhar
 * ID: 21494299
 * Version: 1.0.0.1
 */

namespace DataTierWebServer.Models.Exceptions
{
    public class MismatchIdException : Exception
    {
        // Base message for all instances of this exception
        private const string BaseMessage = "Id does not match: ";

        /*
         * Method: MismatchIdException
         * Description: Constructor for the MismatchIdException class
         * Params:
         *   Ids: The IDs that do not match
         */
        public MismatchIdException(string Ids) : base(BaseMessage + Ids) { }

        /*
         * Method: MismatchIdException
         * Description: Constructor for the MismatchIdException class with inner exception
         * Params:
         *   Ids: The IDs that do not match
         *   innerException: The inner exception that caused this mismatch
         */
        public MismatchIdException(string Ids, Exception innerException) : base(BaseMessage + Ids, innerException) { }
    }
}
