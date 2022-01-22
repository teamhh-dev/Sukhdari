using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Service.IService
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDTO>> getAllProducts(int storeId);
        public Task<ProductDTO> getProductById(int storeid, int productId);
        public Task<StoreDTO> getProductByName(string name);
    }
}
