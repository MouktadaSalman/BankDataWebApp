namespace BankPresentationLayer.Models
{
    public class DataRetrievalFailException : Exception
    {
        private const string BaseMessage = "Failed to data retrieve from the request: ";

        public DataRetrievalFailException(string extra) : base(BaseMessage + extra) { }

        public DataRetrievalFailException(string extra, Exception innerException) : base(BaseMessage + extra, innerException) { }
    }
}
