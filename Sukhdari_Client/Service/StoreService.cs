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

        public StoreService(HttpClient client)
        {
            _httpClient = client;
        }
        public async Task<IEnumerable<StoreDTO>> getAllStores()
        {
            var response = await _httpClient.GetAsync($"api/Store/getAllStores");
            var content = await response.Content.ReadAsStringAsync();
            var stores = JsonConvert.DeserializeObject<IEnumerable<StoreDTO>>(content);
            return stores;
        }

        public async Task<IEnumerable<StoreDTO>> getAllStoresByCategory(string categoryName)
        {
            var response = await _httpClient.GetAsync($"api/Store/GetProductsWithStoreName/{categoryName}");
            var content = await response.Content.ReadAsStringAsync();
            var stores = JsonConvert.DeserializeObject<IEnumerable<StoreDTO>>(content);
            return stores;
        }

        public async Task<IEnumerable<StoreDTO>> getAllStoresByProducts(string productName)
        {
            var response = await _httpClient.GetAsync($"api/Store/GetStoresByProduct/{productName}");
            var content = await response.Content.ReadAsStringAsync();
            var stores = JsonConvert.DeserializeObject<IEnumerable<StoreDTO>>(content);
            return stores;
        }

        public async Task<StoreDTO> getStoreByName(string storeName)
        {
            var response = await _httpClient.GetAsync($"api/Store/GetStoresByCategory/{storeName}");
            var content = await response.Content.ReadAsStringAsync();
            var stores = JsonConvert.DeserializeObject<StoreDTO>(content);
            return stores;
        }

        public async Task<IEnumerable<ProductDTO>> GetStoreProducts(int storeID)
        {
            var response = await _httpClient.GetAsync($"api/Store/GetStoreProducts/{storeID}");
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(content);
            return products;
        }
    }
}
