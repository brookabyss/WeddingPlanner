
using Microsoft.EntityFrameworkCore;
 
namespace wedding.Models
{
    public class WeddingContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public WeddingContext(DbContextOptions<WeddingContext> options) : base(options) { }
        public DbSet<Users> Users {get;set;}
        public DbSet<Weddings> Weddings {get;set;}
        public DbSet<Invitations> Invitations {get;set;}
    }
}