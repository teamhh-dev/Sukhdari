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
        public Task<StoreDTO> getStoreByName(string storeName);
        public Task<IEnumerable<StoreDTO>> getAllStoresByCategory(string categoryName);
        public Task<IEnumerable<StoreDTO>> getAllStoresByProducts(string productName);
        public Task<IEnumerable<ProductDTO>> GetStoreProducts(int storeID);
    }
}
