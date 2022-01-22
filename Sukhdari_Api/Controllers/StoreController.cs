using Business.IRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukhdari_Api.Controllers
{
    [Route("api/[controller]")]
    public class StoreController : Controller
    {
        private readonly IStoreRepo _storeRepo;
        private readonly IProductRepo _productRepo;
        public StoreController(IStoreRepo storeRepo,IProductRepo productRepo)
        {

            _storeRepo = storeRepo;
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<IActionResult> SearchStore()
        {
            var allStores = await _storeRepo.getAllStores();
;            return Ok(allStores);
        }

        [HttpGet("{StoreName}")]
        public async Task<IActionResult> GetProductsWithStoreName(string StoreName)
        {
            var store = _storeRepo.GetStoreByName(StoreName.ToLower().Trim());
            if (store == null)
            {
                return BadRequest(new ErrorModelDTO() { ErrorMessage = "No such Store Exist (", Title = "", StatusCode = StatusCodes.Status404NotFound });

            }
            var s_Id = store.Id;
            var products = await _productRepo.getAllProducts();
            var productsToFind = products.FirstOrDefault(i => i.StoreId == s_Id);
            return Ok(productsToFind);
        }

        

        
    }
}
