using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepo
{
    public interface IProductRepo
    {
        public Task<int> createProduct(ProductDTO product);
        public Task<int> updateProduct(ProductDTO product);
        public Task<int> deleteProduct(int id);
        public Task<IEnumerable<ProductDTO>> getAllProducts();
        public Task<IEnumerable<ProductDTO>> getAllProducts(int storeId);
        public Task<ProductDTO> GetProduct(int id,int storeId);

    }
}
