namespace DataTierWebServer.Models.Exceptions
{
    public class MismatchIdException : Exception
    {
        private const string BaseMessage = "Id does not match: ";

        public MismatchIdException(string Ids) : base(BaseMessage + Ids) { }

        public MismatchIdException(string Ids, Exception innerException) : base(BaseMessage + Ids, innerException) { }
    }
}
