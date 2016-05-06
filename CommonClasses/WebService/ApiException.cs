using System;

namespace CommonClasses.Services.ApiClient
{
    public class ApiException : Exception
    {
        public ApiException(string statusCode, string message, string method)
            : base(string.Format("Error occured in Api method {2}, code: {0} ({1})", statusCode, message, method))
        { }
    }
}
