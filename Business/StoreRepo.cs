using AutoMapper;
using Business.IRepo;
using DataAccess.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class StoreRepo : IStoreRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public StoreRepo(ApplicationDbContext db, IMapper map)
        {
            _db = db;
            _mapper = map;
        }
        public async Task<int> createStore(StoreDTO store)
        {
            var storeAdminId = _db.Users.FirstOrDefault(i => i.UserName == store.AdminName).Id;
            Store newStore = _mapper.Map<StoreDTO, Store>(store);
            newStore.UserId = storeAdminId;
            var result = await _db.Stores.AddAsync(newStore);
            return await _db.SaveChangesAsync();
        }
        public async Task<int> deleteStore(int id)
        {
            var store = await _db.Stores.FindAsync(id);
            if (store != null)
            {
                _db.Stores.Remove(store);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }
        public async Task<IEnumerable<StoreDTO>> getAllStores()
        {
            return _mapper.Map<IEnumerable<Store>, IEnumerable<StoreDTO>>(_db.Stores);
        }
        public StoreDTO GetStoreByAdminName(string adminName)
        {
            var AdminID = _db.Users.FirstOrDefault(i => i.UserName == adminName).Id;
            Store find = _db.Stores.FirstOrDefault(i => i.UserId == AdminID);
            if (find == null)
            {
                return null;
            }
            return _mapper.Map<Store, StoreDTO>(find);
        }
        public StoreDTO GetStoreByName(string name)
        {
            Store find = _db.Stores.FirstOrDefault(i => i.Name == name);
            if (find == null)
            {
                return null;
            }
            return _mapper.Map<Store, StoreDTO>(find);
        }
        public async Task<IEnumerable<StoreDTO>> getStoresByAllFilters(string data)
        {
            var products = _db.Products.Where(i => i.Name.ToLower().Contains(data.ToLower())).ToList();
            var storeCategories = _db.Categories.Where(i => i.Name.ToLower().Contains(data.ToLower())).ToList();
            var stores = _db.Stores.Where(i => i.Name.ToLower().Contains(data.ToLower())).ToList();
            List<Store> storesList = new List<Store>();
            foreach (var s in products)
            {
                storesList.Add(await _db.Stores.FindAsync(s.StoreId));
            }
            foreach (var s in storeCategories)
            {
                storesList.Add(await _db.Stores.FindAsync(s.StoreId));
            }
            foreach (var s in stores)
            {
                storesList.Add(s);
            }
            return _mapper.Map<IEnumerable<Store>, IEnumerable<StoreDTO>>(storesList);
        }

        public async Task<IEnumerable<StoreDTO>> getStoresByCountry(string country)
        {
            var stores = _db.Stores.Where(i => i.Country.ToLower().Contains(country.ToLower())).ToList();
            return _mapper.Map<IEnumerable<Store>, IEnumerable<StoreDTO>>(stores);
        }
        public async Task<int> updateStore(StoreDTO store)
        {
            Store oldStore = await _db.Stores.FindAsync(store.Id);
            Store newStore = _mapper.Map<StoreDTO, Store>(store, oldStore);
            _db.Stores.Update(newStore);
            return await _db.SaveChangesAsync();
        }
    }
}