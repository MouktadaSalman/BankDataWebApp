namespace BusinessTierWebServer.Models.Exceptions
{
    public class DataRetrievalFailException : Exception
    {
        private const string BaseMessage = "Failed to data from the request: ";

        public DataRetrievalFailException(string request) : base(BaseMessage + request) { }

        public DataRetrievalFailException(string request, Exception innerException) : base(BaseMessage + request, innerException) { }
    }
}
