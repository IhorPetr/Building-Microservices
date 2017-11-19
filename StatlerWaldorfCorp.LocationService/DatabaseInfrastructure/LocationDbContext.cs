using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using StatlerWaldorfCorp.LocationService.Models;

namespace StatlerWaldorfCorp.LocationService.DatabaseInfrastructure
{
    public class LocationDbContext : DbContext
    {
        public LocationDbContext(DbContextOptions<LocationDbContext> option) : base(option)
        {
            //this.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<LocationRecord> LocationRecords { get; set; }
    }
    public class LocationDbContextFactory : IDesignTimeDbContextFactory<LocationDbContext>
    {
        public LocationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LocationDbContext>();
            var connectionString = Startup.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
            optionsBuilder.UseMySql(connectionString);
            return new LocationDbContext(optionsBuilder.Options);
        }
    }
}
