using BTRS.Models;
using Microsoft.EntityFrameworkCore;


namespace BTRS.Data
{
    public class SystemDbContext : DbContext
    {
        public SystemDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Admin> admin { get; set; }
        public DbSet<Passenger> passenger { get; set; }
        public DbSet<Bus> bus { get; set; }
        public DbSet<BusTrip> busTrip { get; set; }
        public DbSet<Passenger_BusTrip> passenger_bustrip { get; set; }



    }

}
