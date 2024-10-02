using Microsoft.VisualBasic;

namespace DataTierWebServer.Models.Exceptions
{
    public class MissingProfileException : Exception
    {
        private const string BaseMessage = "User profile does not exist for requested: ";
        public MissingProfileException(string name) : base(BaseMessage + name) { }

        public MissingProfileException(string name, Exception innerException) : base(BaseMessage + name, innerException) { }
    }
}
