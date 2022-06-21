using AutoMapper;
using Business.IRepo;
using DataAccess.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;

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
        public async Task<StoreDTO> createStore(StoreDTO store)
        {
            if (store.Id != 0)
            {
                Store oldStore = await _db.Stores.FindAsync(store.Id);
                oldStore.Name = store.Name;
                oldStore.Address = store.Address;
               
                oldStore.phoneNo = store.phoneNo;
                if (oldStore.Type != store.Type)
                {
                    var storeTags = _db.storeTags.Where(i => i.storeId == store.Id).ToList();
                    if(storeTags.Any())
                    {
                        foreach(var tag in storeTags)
                        {
                            _db.storeTags.Remove(tag);
                        }
                    }
                    oldStore.Type = store.Type;
                }
                oldStore.Country = store.Country;
                oldStore.Image = store.Image;
                if(oldStore.ClickCount != null)
                {
                    oldStore.ClickCount = store.ClickCount;
                }
                else
                {
                    oldStore.ClickCount = 0;
                }
                await _db.SaveChangesAsync();
                return _mapper.Map<Store, StoreDTO>(oldStore);
            }
            var storeAdminId = _db.Users.FirstOrDefault(i => i.UserName == store.AdminName).Id;
            Store newStore = _mapper.Map<StoreDTO, Store>(store);
            newStore.UserId = storeAdminId;
            newStore.ClickCount = 0;
            newStore.timeNow = DateTime.UtcNow.Date;
            
            await _db.Stores.AddAsync(newStore);
            await _db.SaveChangesAsync();
            return _mapper.Map<Store, StoreDTO>(newStore);
        }
        public async Task<int> deleteStore(int id)
        {
            var store = await _db.Stores.FindAsync(id);
            if (store != null)
            {
                var images = _db.storeImages.Where(i => i.StoreId == id).ToList();
                foreach (var image in images)
                {
                    if (File.Exists(image.StoreImageUrl))
                    {
                        File.Delete(image.StoreImageUrl);
                    }
                }
                _db.storeImages.RemoveRange(images);
                _db.Stores.Remove(store);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }
        public async Task<IEnumerable<StoreDTO>> getAllStores()
        {
        
            return _mapper.Map<IEnumerable<Store>, IEnumerable<StoreDTO>>(_db.Stores.Include(i => i.StoreImages));

        }
        public StoreDTO GetStoreByAdminName(string adminName)
        {
            var AdminID = _db.Users.FirstOrDefault(i => i.UserName == adminName).Id;
            Store find = _db.Stores.Include(i => i.StoreImages).FirstOrDefault(i => i.UserId == AdminID);
            if (find == null)
            {
                return null;
            }
            return _mapper.Map<Store, StoreDTO>(find);
        }
        public StoreDTO GetStoreByName(string name)
        {

            Store find = _db.Stores.Include(i => i.StoreImages).FirstOrDefault(i => i.Name == name);
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
            var stores = _db.Stores.Include(i => i.StoreImages).Where(i => i.Name.ToLower().Contains(data.ToLower())).ToList();

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
            var stores = _db.Stores.Include(i => i.StoreImages).Where(i => i.Country.ToLower().Contains(country.ToLower())).ToList();
            return _mapper.Map<IEnumerable<Store>, IEnumerable<StoreDTO>>(stores);
        }
        public async Task<int> updateStore(StoreDTO store)
        {
            Store oldStore = await _db.Stores.FindAsync(store.Id);
            Store newStore = _mapper.Map<StoreDTO, Store>(store, oldStore);
            _db.Stores.Update(newStore);
            return await _db.SaveChangesAsync();
        }
        public async Task<int> getStoreCount()
        {
            return await _db.Stores.CountAsync();
        }
        public async Task<int> clickStoreCount(int storeID)
        {
            var store = _db.Stores.FirstOrDefault(i => i.Id == storeID);
            if (store.ClickCount == null)
            {
                store.ClickCount = 1;
            }
            else
            {
                store.ClickCount++;
            }
            return await _db.SaveChangesAsync();
        }
    }
}

