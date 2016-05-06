using Newtonsoft.Json;
using System;
using System.IO;

namespace CommonClasses.Services.ApiClient
{
    public class ApiResponse<T>
    {
        public ApiResponse(string response, Exception error = null)
        {
            Result = JsonConvert.DeserializeObject<T>(response);
            Error = error;
        }
        public ApiResponse(T result, Exception error = null)
        {
            Result = result;
            Error = error;
        }

        public ApiResponse(Stream stream, Exception error = null)
        {
            ContentStream = stream;
            Error = error;
        }

        public ApiResponse(Exception error)
        {
            Result = default(T);
            Error = error;
        }
        public T Result { get; private set; }
        public Exception Error { get; private set; }

        public Stream ContentStream { get; private set; }

        public bool HasError
        {
            get
            {
                return Error != null;
            }
        }

        public bool HasResult
        {
            get
            {
                return Result != null;
            }
        }

        public bool HasStream
        {
            get
            {
                return ContentStream != null;
            }
        }

        public string ErrorMessage
        {
            get
            {
                return (HasError) ? Error.Message : string.Empty;
            }
        }
    }
}
