using Client.Service.IService;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Service
{
    public class StoreService : IStoresService
    {
        private readonly HttpClient _httpClient;
        
        public StoreService(HttpClient client)
        {
            _httpClient = client;
        }
        public async Task<IEnumerable<StoreDTO>> getAllStores()
        {
            var response = await _httpClient.GetAsync($"api/Store");
            var content = await response.Content.ReadAsStringAsync();
            var stores = JsonConvert.DeserializeObject<IEnumerable<StoreDTO>>(content);
            return stores;
        }


        public async Task<StoreDTO> getStoreByName(string name)
        {
            var response = await _httpClient.GetAsync($"api/Store/{name}");
            var content = await response.Content.ReadAsStringAsync();
            var store = JsonConvert.DeserializeObject<StoreDTO>(content);
            return store;
        }
    }
}
