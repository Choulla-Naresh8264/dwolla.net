using System;
using System.Runtime.Serialization;

namespace Dwolla
{
    /// <summary>
    /// Custom exception class for invalid API responses.
    /// </summary>
    [Serializable]
    public class ApiException : ApplicationException
    {
        public ApiException() { }
        public ApiException(string message) { }
        protected ApiException(SerializationInfo info, StreamingContext context) { }
    }
}
