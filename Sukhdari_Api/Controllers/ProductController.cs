﻿using Business.IRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukhdari_Api.Controllers
{

    [Route("api/[controller]/[action]")]

   
    public class ProductController : Controller
    {
        private readonly IStoreRepo _storeRepo;
        private readonly IProductRepo _productRepo;
        public ProductController(IProductRepo productRepo,IStoreRepo storeRepo)
        {
            _productRepo = productRepo;
            _storeRepo= storeRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(int storeId)
        {
            var products= await _productRepo.getAllProducts(storeId);
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProducts(int? productId)
        {
            var products = await _productRepo.getAllProducts();
            var productToFind = products.FirstOrDefault(i => i.Id == productId);

            if (productId==null||productToFind==null)
            {
                return BadRequest(new ErrorModelDTO() { ErrorMessage = "Invalid Product Id", Title = "", StatusCode = StatusCodes.Status400BadRequest });

            }
            return Ok(productToFind);
        }
        [HttpGet("{productName}")]
        public async Task<IActionResult> GetProductsWithName(string productName)
        {
            var products = await _productRepo.getAllProducts();
            var productsToFind = products.Where(i => i.Name.ToLower().Contains(productName)).Select(i=>i.StoreId);

            var stores = await _storeRepo.getAllStores();

            var searchedStores = stores.Where(t => productsToFind.Contains(t.Id)).ToList();
            //if (productId==null||productToFind==null)
            //{
            //    return BadRequest(new ErrorModelDTO() { ErrorMessage = "Invalid Product Id", Title = "", StatusCode = StatusCodes.Status400BadRequest });

            //}
            return Ok(searchedStores);
        }

    }
}
