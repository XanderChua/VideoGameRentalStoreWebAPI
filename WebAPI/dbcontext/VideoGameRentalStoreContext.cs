using System;
using System.Data.Entity;
using WebAPI.Models;
using WebAPI.Interface;
namespace WebAPI.EntityFramework
{
    public class VideoGameRentalStoreContext : DbContext, IContext
    {
        public VideoGameRentalStoreContext() : base("VideoGameRentalConnectionString")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<VideoGameRentalStoreContext>());
        }
        public virtual DbSet<StoreStaff> StoreStaffs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Games> Games { get; set; }
    }
}