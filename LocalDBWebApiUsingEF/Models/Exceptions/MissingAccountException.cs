namespace DataTierWebServer.Models.Exceptions
{
    public class MissingAccountException : Exception
    {
        private const string BaseMessage = "User account does not exist for requested: ";
        public MissingAccountException(string name) : base(BaseMessage + name) { }

        public MissingAccountException(string name, Exception innerException) : base(BaseMessage + name, innerException) { }
    }
}
