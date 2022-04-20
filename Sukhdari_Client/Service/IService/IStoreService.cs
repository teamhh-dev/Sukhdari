using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukhdari_Client.Service.IService
{
    public interface IStoreService
    {
        public Task<IEnumerable<StoreDTO>> getAllStores();
        public Task<IEnumerable<ProductDTO>> getAllProducts();
        public Task<IEnumerable<CategoryDTO>> getStoreCategories(int storeId);
        public Task<IEnumerable<ProductDTO>> getDiscountedProducts(int storeId);
        public Task<StoreDTO> getStoreByName(string storeName);
        public Task<IEnumerable<StoreDTO>> SearchStoreByCategory(string categoryName);
        public Task<IEnumerable<StoreDTO>> SearchStoreByProductName(string productName);
        public Task<IEnumerable<StoreDTO>> getStoresByAllFilters(string data);
        public Task<IEnumerable<ProductDTO>> GetStoreProducts(int storeID);
        public Task<IEnumerable<StoreDTO>> SearchStoreByProductPrice(int min, int max);
        public Task<IEnumerable<StoreDTO>> SearchStoreByCountry(string country);
        public Task<IEnumerable<ProductDTO>> getCategoryProducts(int categoryId);
        public Task<ProductDTO> getSpecificProduct(int storeId, int productId);
    }
}