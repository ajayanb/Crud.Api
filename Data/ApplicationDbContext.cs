using CodPulse.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CodPulse.API.Data
{
    public class ApplicationDbContex : DbContext
    {
        public ApplicationDbContex(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts{ get; set; }

        public DbSet<Category> Categories{ get; set; }

        public DbSet<BlogImage> BlogImages{ get; set; }

    }
}
