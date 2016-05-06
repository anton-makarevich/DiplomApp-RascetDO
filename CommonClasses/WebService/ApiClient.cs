using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CommonClasses.Services.ApiClient
{
    public class DiplomApiClient:ApiClientBase
    {
        #region Instance
        static DiplomApiClient _instance;
        public static DiplomApiClient Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DiplomApiClient();
                return _instance;
            }
        }
        #endregion

        #region Constructor
        public DiplomApiClient(string url = "https://diplomapi.azurewebsites.net/api/") :base(url)
        {

        }
        #endregion

        #region API Methods
        public async Task<ApiResponse<PavementModel>> CalculateAsync(PavementModel model)
        {
            var request = "diplom";
            var content = new StringContent(JsonConvert.SerializeObject(model));
            var response = await GetResponseAsync<PavementModel>(HttpMethod.Post, request, content);

            return response;
        }
        
        #endregion
    }
}
