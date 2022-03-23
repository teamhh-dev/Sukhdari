using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukhdari_Client.Service.IService
{
    public interface IStoreService
    {
        public Task<IEnumerable<StoreDTO>> getAllStores();
        public Task<IEnumerable<ProductDTO>> getAllProducts();
        public Task<StoreDTO> getStoreByName(string storeName);
        public Task<IEnumerable<StoreDTO>> SearchStoreByCategory(string categoryName);
        public Task<IEnumerable<StoreDTO>> SearchStoreByProductName(string productName);
        public Task<IEnumerable<StoreDTO>> getStoresByAllFilters(string data);
        public Task<IEnumerable<ProductDTO>> GetStoreProducts(int storeID);
    }
}
