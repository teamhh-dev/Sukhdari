using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepo
    {
    public interface ITagRepo
        {
        public Task<int> createTag(TagDTO tag);
        public Task<int> deleteTag(int id);
        public Task<int> updateTag(TagDTO tag);
        public Task<IEnumerable<TagDTO>> GetAllTags(int StoreId);
        public Task<IEnumerable<TagDTO>> GetAllTags();
        public Task<TagDTO> GetTag(int id, int storeId);
        public Task<IEnumerable<StoreDTO>> getStoreByTag(string tagName);
        }
    }