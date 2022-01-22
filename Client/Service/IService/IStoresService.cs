using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Service.IService
{
    public interface IStoresService
    {
        public Task<IEnumerable<StoreDTO>> getAllStores();
        public Task<StoreDTO> getStoreByName(string name);
        public Task<StoreDTO> getStoreByCategory(string category);
        
    }
}
