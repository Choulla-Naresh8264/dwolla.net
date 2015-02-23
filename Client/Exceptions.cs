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

    /// <summary>
    /// Custom exception class for invalid OAuth responses.
    /// </summary>
    [Serializable]
    public class OAuthException : ApplicationException
    {
        public OAuthException() { }
        public OAuthException(string message) { }
        protected OAuthException(SerializationInfo info, StreamingContext context) { }
    }
}
