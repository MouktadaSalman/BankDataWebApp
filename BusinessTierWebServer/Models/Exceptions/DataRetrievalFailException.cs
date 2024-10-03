namespace BusinessTierWebServer.Models.Exceptions
{
    public class DataRetrievalFailException : Exception
    {
        private const string BaseMessage = "Failed to data retrieve from the request";

        public DataRetrievalFailException() : base(BaseMessage) { }

        public DataRetrievalFailException(Exception innerException) : base(BaseMessage, innerException) { }
    }
}
