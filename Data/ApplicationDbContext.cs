using Hotels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Hotels.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options) 
        {
            
        }

        public DbSet<Cart> cart { get; set; }
        public DbSet<Hotel> hotel { get; set; }
        public DbSet<RoomDetails> roomDetails { get; set; }
        public DbSet<Rooms> rooms { get; set; }
        public DbSet<Invoice> invoice { get; set; }
    }
}
