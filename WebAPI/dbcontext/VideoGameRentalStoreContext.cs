using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebAPI.Models;

namespace WebAPI.EntityFramework
{
    public class VideoGameRentalStoreContext : DbContext
    {
        public VideoGameRentalStoreContext() : base()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<VideoGameRentalStoreContext>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<StoreStaff> StoreStaffs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Games> Games { get; set; }
    }
}