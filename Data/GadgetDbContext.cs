using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class GadgetDbContext : DbContext
    {
        public GadgetDbContext(DbContextOptions<GadgetDbContext> options) : base(options)
        {
            // this applies the data that was added during the OnModelCreating event
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gadget>().HasData(
                new Gadget()
                {
                    Id = 1,
                    GadgetType = "Type A",
                    UsageInstructions = "Use Gadget A to assemble Widgets.",
                    UserDefinedSequenceNumber = 1
                },
                new Gadget()
                {
                    Id = 2,
                    GadgetType = "Type B",
                    UsageInstructions = "Use Gadget B to test Widgets.",
                    UserDefinedSequenceNumber = 2
                },
                new Gadget()
                {
                    Id = 3,
                    GadgetType = "Type C",
                    UsageInstructions = "Use Gadget C to use Widgets.",
                    UserDefinedSequenceNumber = 3
                }
            );
        }

        public DbSet<Gadget> Gadgets => Set<Gadget>();
    }
}