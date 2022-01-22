using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.IRepo;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Business
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public CategoryRepo(ApplicationDbContext db, IMapper map)
        {
            _db = db;
            _mapper = map;
        }
        public async Task<int> createCategory(CategoryDTO category)
        {
            if (category.Id!=0)
            {
                var oldCategory = _db.Categories.FirstOrDefault(i => i.Id == category.Id);
                oldCategory.Name = category.Name;
                oldCategory.Description = category.Description;
                return await _db.SaveChangesAsync();

            }
            Category newCategory = _mapper.Map<CategoryDTO, Category>(category);
            await _db.Categories.AddAsync(newCategory);
            return await _db.SaveChangesAsync();

        }

        public async Task<int> deleteCategory(int id)
        {
            Category category = await _db.Categories.FindAsync(id);
            if (category != null)
            {
                _db.Categories.Remove(category);
                return await _db.SaveChangesAsync();
            }
            return 0;

        }

        public async Task<CategoryDTO> GetCategory(int id, int storeId)
        {

            var category = _db.Categories.FirstOrDefault(i => i.Id == id && i.StoreId == storeId);

            return _mapper.Map<Category, CategoryDTO>(category);
        }
        public async Task<IEnumerable<CategoryDTO>> GetAllCategories(int StoreId)
        {
            IEnumerable<CategoryDTO> dto = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_db.Categories.Where(i => i.StoreId == StoreId));
            return dto;
        }
        public async Task<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            IEnumerable<CategoryDTO> dto = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_db.Categories);
            return dto;
        }

        public async Task<int> updateCategory(CategoryDTO category)
        {
            Category oldCategory = await _db.Categories.FindAsync(category.Id);
            Category newCategory = _mapper.Map<CategoryDTO, Category>(category, oldCategory);
            _db.Categories.Update(newCategory);
            return await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<StoreDTO>> getStoreByCategory(string categoryName)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(i => i.Name.ToLower() == categoryName.ToLower());
            return _mapper.Map<IEnumerable<Store>, IEnumerable<StoreDTO>>(category.Stores);
            
        }
    }

}
