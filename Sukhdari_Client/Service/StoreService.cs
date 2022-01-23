using Models;
using Newtonsoft.Json;
using Sukhdari_Client.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sukhdari_Client.Service
{
    public class StoreService : IStoreService
    {
        private readonly HttpClient _httpClient;
        public StoreService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }
        public async Task<IEnumerable<StoreDTO>> GetAllStores()
        {
            var response = await _httpClient.GetAsync("api/store/getAllStores");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var stores = JsonConvert.DeserializeObject <IEnumerable < StoreDTO >> (content);
                return stores;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }
    }
}
