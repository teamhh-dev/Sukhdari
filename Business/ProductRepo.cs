using AutoMapper;
using Business.IRepo;
using DataAccess.Data;
using Models;
using System;
using System.Collections.Generic;
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
        public async Task<int> createProduct(ProductDTO product)
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
                return await _db.SaveChangesAsync();

            }

            Product newProd = _mapper.Map<ProductDTO, Product>(product);
            await _db.Products.AddAsync(newProd);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> deleteProduct(int id)
        {
            var prod = await _db.Products.FindAsync(id);
            if (prod != null)
            {
                _db.Products.Remove(prod);
                return await _db.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<IEnumerable<ProductDTO>> getAllProducts(int storeId)
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_db.Products.Where(i=>i.StoreId==storeId));
        }

        public async Task<IEnumerable<ProductDTO>> getAllProducts()
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_db.Products);
        }

        public async Task<ProductDTO> GetProduct(int id, int storeId)
        {
            var product = _db.Products.FirstOrDefault(i => i.Id == id && i.StoreId == storeId);

            return  _mapper.Map<Product, ProductDTO>(product);
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