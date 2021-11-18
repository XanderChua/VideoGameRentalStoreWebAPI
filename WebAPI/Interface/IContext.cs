using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Interface
{
    public interface IContext
    {
        DbSet<StoreStaff> StoreStaffs { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Games> Games { get; set; }
        int SaveChanges();
        DbEntityEntry Entry(object entity);
    }
}
