using System;
using System.Runtime.Serialization;

namespace dwolla
{
    /// <summary>
    /// Custom exception class for invalid API responses.
    /// </summary>
    public class APIException : ApplicationException
    {
        public APIException() { }
        public APIException(string message) { }
        protected APIException(SerializationInfo info, StreamingContext context) { }
    }
}
