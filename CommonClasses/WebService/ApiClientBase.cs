using ModernHttpClient;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CommonClasses.Services.ApiClient
{
    public abstract class ApiClientBase
    {
        #region Constructor
        public ApiClientBase(string baseUrl)
        {
            _client = new HttpClient(new NativeMessageHandler());
            //set base web api url
            _client.BaseAddress = new Uri(baseUrl);
            // Add an Accept header for JSON format.
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion
        #region Fields
        //HttpClient for communication
        protected readonly HttpClient _client;

        #endregion

        /// <summary>
        /// Main Common call method
        /// </summary>
        /// <param name="method">Http method used for this request</param>
        /// <param name="request">request uri</param>
        /// <param name="content">http content when needed</param>
        /// <returns></returns>
        public virtual async Task<ApiResponse<T>> GetResponseAsync<T>(HttpMethod method, string request, HttpContent content = null,string contentType="application/json", [CallerMemberName] string methodName = null)
        {
            HttpResponseMessage response = null;
            string responseString = null;
            try
            {
                if (content != null)
                    content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                if (method == HttpMethod.Get)
                {
                    response = await _client.GetAsync(request);
                }
                else if (method == HttpMethod.Put)
                {
                    response = await _client.PutAsync(request, content);
                }
                else if (method == HttpMethod.Post)
                {
                    response = await _client.PostAsync(request, content);
                }
                else
                {
                    response = await _client.DeleteAsync(request);
                }

                if (response.Content.Headers.ContentType != null && response.Content.Headers.ContentType.MediaType == "application/json")
                {
                    responseString = await response.Content.ReadAsStringAsync();
                }
                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<T>(new ApiException(response.StatusCode.ToString(), response.ReasonPhrase, methodName));
                }
            }
            catch (Exception ex)
            {
                //check connection
                return new ApiResponse<T>(ex);
            }
            if (!string.IsNullOrEmpty(responseString))
                return new ApiResponse<T>(responseString);
            return new ApiResponse<T>(await response.Content.ReadAsStreamAsync());
        }
    }
}
