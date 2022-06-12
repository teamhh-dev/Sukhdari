using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> productImages { get; set; }
        public DbSet<IPAddress> ipAddresses { get; set; }
        public DbSet<StoreImage> storeImages { get; set; }
        public DbSet<TagType> tagTypes { get; set; }
        public DbSet<Tag> tags { get; set; }
        public DbSet<StoreTags> storeTags { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<Store>().Property(i => i.ClickCount).HasDefaultValue(0);
        //    builder.Entity<Category>().Property(i => i.ClickCount).HasDefaultValue(0);
        //    builder.Entity<Product>().Property(i => i.ClickCount).HasDefaultValue(0);

        //}
    }

}
