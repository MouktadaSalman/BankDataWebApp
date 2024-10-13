/*
 * Module: DataRetrievalFailException
 * Description: Custom exception for handling data retrieval failures in the system
 * Author: Ahmed, Moukhtada, Jauhar
 * ID: 21467369, 20640266, 21494299
 * Version: 1.0.0.1
 */

namespace BusinessTierWebServer.Models.Exceptions
{
    public class DataRetrievalFailException : Exception
    {
        // Base message for all instances of this exception
        private const string BaseMessage = "Failed to retrieve data from the request: ";

        /*
         * Method: DataRetrievalFailException
         * Description: Constructor for the DataRetrievalFailException class
         * Params:
         *   extra: Additional context or information about the failure
         */
        public DataRetrievalFailException(string extra) : base(BaseMessage + extra) { }

        /*
         * Method: DataRetrievalFailException
         * Description: Constructor for the DataRetrievalFailException class with inner exception
         * Params:
         *   extra: Additional context or information about the failure
         *   innerException: The inner exception that caused this data retrieval failure
         */
        public DataRetrievalFailException(string extra, Exception innerException) : base(BaseMessage + extra, innerException) { }
    }
}
