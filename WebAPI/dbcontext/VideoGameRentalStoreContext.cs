using System;
using System.Data.Entity;
using WebAPI.Models;

namespace WebAPI.EntityFramework
{
    public class VideoGameRentalStoreContext : DbContext
    {
        public VideoGameRentalStoreContext() : base()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", "D:\\");
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