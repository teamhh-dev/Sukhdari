 using AutoMapper;
using Business.IRepo;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ProductRepo : IProductRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProductRepo(ApplicationDbContext db, IMapper map)
        {
            _db = db;
            _mapper = map;
        }
        public async Task<ProductDTO> createProduct(ProductDTO product)
        {
            if (product.Id != 0)
            {
                var oldCategory = _db.Products.FirstOrDefault(i => i.Id == product.Id);
                oldCategory.Name = product.Name;
                oldCategory.Price = product.Price;
                oldCategory.Description = product.Description;
                oldCategory.CategoryId = product.CategoryId;
                oldCategory.Image = product.Image;
                oldCategory.Quantity = product.Quantity;
                await _db.SaveChangesAsync();
                return _mapper.Map<Product, ProductDTO>(oldCategory);

            }

            Product newProd = _mapper.Map<ProductDTO, Product>(product);
            await _db.Products.AddAsync(newProd);
            await _db.SaveChangesAsync();
            return _mapper.Map<Product, ProductDTO>(newProd);
        }

        public async Task<int> deleteProduct(int id)
        {
            var prod = await _db.Products.FindAsync(id);
            if (prod != null)
            {
                var images = _db.productImages.Where(i => i.ProductId == id).ToList();
                foreach(var image in images)
                {
                    if(File.Exists(image.ProductImageUrl))
                    {
                        File.Delete(image.ProductImageUrl);
                    }
                }
                _db.productImages.RemoveRange(images);
                _db.Products.Remove(prod);
                return await _db.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<IEnumerable<ProductDTO>> getAllProducts(int storeId)
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_db.Products.Include(i => i.ProductImages).Where(i => i.StoreId == storeId)).ToList();
        }

        public async Task<IEnumerable<ProductDTO>> getAllProducts()
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_db.Products.Include(i => i.ProductImages));
        }

        public async Task<ProductDTO> GetProduct(int id, int storeId)
        {
            var product = _db.Products.Include(i=>i.ProductImages).FirstOrDefault(i => i.Id == id && i.StoreId == storeId);

            return _mapper.Map<Product, ProductDTO>(product);
        }

        public async Task<ProductDTO> getProduct(int id)
        {
            try
            {
                ProductDTO product = _mapper.Map<Product, ProductDTO>(await _db.Products.Include(i => i.ProductImages).FirstOrDefaultAsync(i => i.Id == id));
                return product; 
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<IEnumerable<StoreDTO>> getStoresByProductName(string productName)
        {
            var products = _db.Products.Where(i => i.Name.ToLower().Contains(productName.ToLower())).ToList();
            List<Store> stores = new List<Store>();
            foreach (var s in products)
            {
                stores.Add(await _db.Stores.FindAsync(s.StoreId));
            }
            return _mapper.Map<IEnumerable<Store>, IEnumerable<StoreDTO>>(stores);
        }

        public async Task<IEnumerable<StoreDTO>> getStoresByProductPriceRange(int low, int high)
        {
            var products = _db.Products.Where(i => i.Price >= low && i.Price <= high).ToList();
            List<Store> stores = new List<Store>();
            foreach (var s in products)
            {
                stores.Add(await _db.Stores.FindAsync(s.StoreId));
            }
            return _mapper.Map<IEnumerable<Store>, IEnumerable<StoreDTO>>(stores);

        }

        public async Task<int> updateProduct(ProductDTO product)
        {
            var prod = await _db.Products.FindAsync(product.Id);
            var updatedProd = _mapper.Map<ProductDTO, Product>(product, prod);
            _db.Products.Update(updatedProd);
            return await _db.SaveChangesAsync();
        }
    }
}