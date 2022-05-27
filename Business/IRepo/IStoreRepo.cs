﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepo
{
    public interface IStoreRepo
    {
        public Task<StoreDTO> createStore(StoreDTO store);
        public Task<int> updateStore(StoreDTO store);
        public Task<int> deleteStore(int id);
        public Task<IEnumerable<StoreDTO>> getAllStores();
        public StoreDTO GetStoreByName(string name);
        public StoreDTO GetStoreByAdminName(string adminName);
        public Task<IEnumerable<StoreDTO>> getStoresByAllFilters(string data);
        public Task<IEnumerable<StoreDTO>> getStoresByCountry(string country);
        public Task<StoreDTO> GetStoreByID(int storeID);
        public Task<int> getStoreCount();
        public Task<int> clickStoreCount(int storeID);


    }
}
