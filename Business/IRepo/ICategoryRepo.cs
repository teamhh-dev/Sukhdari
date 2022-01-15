using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepo
{
    public interface ICategoryRepo
    {
        public Task<int> createCategory(CategoryDTO category);
        public Task<int> deleteCategory(int id);
        public Task<int> updateCategory(CategoryDTO category);
        public Task<IEnumerable<CategoryDTO>> GetAllCategories();
    }
}
