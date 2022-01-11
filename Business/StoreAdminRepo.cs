using Business.IRepo;
using Common;
using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class StoreAdminRepo : IStoreAdminRepo
    {
        //private readonly SignInManager

        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public StoreAdminRepo(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<int> SignUpAdmin(StoreSignUpDTO storeSignUpDTO)
        {
            IdentityUser user = new IdentityUser() { UserName = storeSignUpDTO.UserName,Email=storeSignUpDTO.Email, PhoneNumber = storeSignUpDTO.phone};
            await _userManager.CreateAsync(user, storeSignUpDTO.Password);
            IdentityUser userForRole = await _userManager.FindByEmailAsync(storeSignUpDTO.Email);
            await _userManager.AddToRoleAsync(userForRole, StaticDetails.Role_StoreAdmin);
            var id = userForRole.Id;
            await _db.Stores.AddAsync(new Store() { StoreName = storeSignUpDTO.StoreName, StoreType = storeSignUpDTO.StoreType, Country = storeSignUpDTO.Country, User = id });
            return  _db.SaveChanges();
        }
    }
}
