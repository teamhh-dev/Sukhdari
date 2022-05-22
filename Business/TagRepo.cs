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
    public class TagRepo : ITagRepo
        {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public TagRepo(ApplicationDbContext db, IMapper mapper)
            {
            _db = db;
            _mapper = mapper;
            }

        public async Task<int> createTag(TagDTO tag)
            {
            Tag newTag = _mapper.Map<TagDTO, Tag>(tag);
            await _db.tags.AddAsync(newTag);
            return await _db.SaveChangesAsync();
            }

        public async Task<int> deleteTag(int id)
            {
            Tag tag = await _db.tags.FindAsync(id);
            if (tag != null)
                {
                _db.tags.Remove(tag);
                return await _db.SaveChangesAsync();
                }
            return 0;
            }

        public async Task<IEnumerable<TagDTO>> GetAllTags(int StoreId)
            {

            return _mapper.Map<IEnumerable<Tag>, IEnumerable<TagDTO>>(_db.tags.Where(i => i.StoreId == StoreId).ToList());
            }

        public async Task<IEnumerable<TagDTO>> GetAllTags()
            {
            return _mapper.Map<IEnumerable<Tag>, IEnumerable<TagDTO>>(_db.tags);
            }

        public async Task<IEnumerable<StoreDTO>> getStoreByTag(string tagName)
            {
            var storeTags = _db.tags.Where(i => i.Name.ToLower().Contains(tagName.ToLower())).ToList();
            List<Store> storesByTags = new List<Store>();
            foreach (var s in storeTags)
                {
                storesByTags.Add(await _db.Stores.Include(i => i.StoreImages).FirstOrDefaultAsync(i => i.Id == s.StoreId));
                }
            return _mapper.Map<IEnumerable<Store>, IEnumerable<StoreDTO>>(storesByTags);
            }

        public async Task<TagDTO> GetTag(int id, int storeId)
            {
            return _mapper.Map<Tag, TagDTO>(await _db.tags.FirstOrDefaultAsync(i => i.Id == id && i.StoreId == storeId));
            }

        public async Task<int> updateTag(TagDTO tag)
            {
            Tag oldTag = await _db.tags.FindAsync(tag.Id);
            Tag newTag = _mapper.Map<TagDTO, Tag>(tag, oldTag);
            _db.tags.Update(newTag);
            return await _db.SaveChangesAsync();
            }
        }
    }