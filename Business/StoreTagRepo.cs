using AutoMapper;
using Business.IRepo;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class StoreTagRepo : IStoreTagRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public StoreTagRepo(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> AddStoreTag(int storeID, int tagID)
        {
            if(tagID != 0 && storeID != 0)
            {
                StoreTags addTags = new StoreTags();
                addTags.storeId = storeID;
                addTags.tagId = tagID;
                await _db.storeTags.AddAsync(addTags);
            }
            return await _db.SaveChangesAsync();
        }
        public async Task<int> DeleteStoreTag(int tagID, int storeID)
        {
            if (tagID != 0 && storeID != 0)
            {
                var checkTag = _db.storeTags.FirstOrDefault(i=>i.tagId == tagID && i.storeId == storeID);
                _db.storeTags.Remove(checkTag);
            }
            return await _db.SaveChangesAsync();
        }
        public async Task<IEnumerable<StoreTagDTO>> getStoreTags(int storeID)
        {
            return _mapper.Map<IEnumerable<StoreTags>, IEnumerable<StoreTagDTO>>(_db.storeTags.Where(i => i.storeId == storeID).ToList());
        }
        public bool isTagAvailable(int tagID, int storeID)
        {
            var checkTag = _db.storeTags.FirstOrDefault(i => i.tagId == tagID && i.storeId == storeID);
            if (checkTag != null)
            {
                return true;
            }
            return false;
        }
    }
}
