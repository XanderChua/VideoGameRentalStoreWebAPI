using System;
using System.Data.Entity;
using WebAPI.Models;

namespace WebAPI.EntityFramework
{
    public class VideoGameRentalStoreContext : DbContext
    {
        public VideoGameRentalStoreContext() : base("VideoGameRentalConnectionString")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<VideoGameRentalStoreContext>());
        }
        public DbSet<StoreStaff> StoreStaffs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Games> Games { get; set; }
    }
}