using System.Reflection.Metadata;

namespace DataTierWebServer.Models.Exceptions
{
    public class DataGenerationFailException : Exception
    {
        private const string BaseMessage = "Failed to generate entry data for: ";
        public DataGenerationFailException(string database) : base(BaseMessage + database) { }

        public DataGenerationFailException(string database, Exception innerException) : base(BaseMessage + database, innerException) { }
    }
}
