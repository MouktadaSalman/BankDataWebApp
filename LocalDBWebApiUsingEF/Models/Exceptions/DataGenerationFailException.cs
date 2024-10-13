/* 
 * Module: DataGenerationFailException
 * Description: Custom exception for data generation failures in the database
 * Author: Jauhar
 * ID: 21494299
 * Version: 1.0.0.1
 */

using System.Reflection.Metadata;

namespace DataTierWebServer.Models.Exceptions
{
    public class DataGenerationFailException : Exception
    {
        // Base message for all instances of this exception
        private const string BaseMessage = "Failed to generate entry data for: ";


        /* 
         * Method: DataGenerationFailException
         * Description: Constructor for the DataGenerationFailException class
         * Params:
         *   database: The name of the database where data generation failed
         */
        public DataGenerationFailException(string database) : base(BaseMessage + database) { }



        /* 
         * Method: DataGenerationFailException
         * Description: Constructor for the DataGenerationFailException class with inner exception
         * Params:
         *   database: The name of the database where data generation failed
         *   innerException: The inner exception that caused this exception
         */
        public DataGenerationFailException(string database, Exception innerException) : base(BaseMessage + database, innerException) { }
    }
}
