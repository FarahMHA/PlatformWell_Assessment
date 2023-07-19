using Microsoft.EntityFrameworkCore;
using PlatformWell_Assessment.Models;

namespace PlatformWell_Assessment.Entities
{
    public class PlatformWellDbContext : DbContext
    {
        //constructor
        public PlatformWellDbContext(DbContextOptions<PlatformWellDbContext> options) : base(options) 
        { }

        //specify model
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Well> Wells { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */
           
            builder.Entity<Platform>(b =>
            {
                b.HasKey(c => c.Id);
                b.Property(c => c.Id).ValueGeneratedNever();
            });

            builder.Entity<Well>(b =>
            {
                b.HasKey(c => c.Id);
                b.Property(c => c.Id).ValueGeneratedNever();
            });
        }
    }
}
