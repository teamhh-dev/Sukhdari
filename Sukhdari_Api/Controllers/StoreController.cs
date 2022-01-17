using Business.IRepo;
using Microsoft.AspNetCore.Mvc;
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
        public StoreController(IStoreRepo storeRepo)
        {

            _storeRepo = storeRepo;
        }

        [HttpGet]
        public async Task<IActionResult> SearchStore()
        {
            var allStores = await _storeRepo.getAllStores();
;            return Ok(allStores);
        }
    }
}
