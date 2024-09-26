using DynamicAPI.Entity.DynamicObjectDb;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

namespace DynamicAPI.DAL.Context
{
    public class DynamicApiDbContext : DbContext
    {
        public DynamicApiDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Objects> Objects {  get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
