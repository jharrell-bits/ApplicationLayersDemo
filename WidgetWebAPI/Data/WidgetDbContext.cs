using Microsoft.EntityFrameworkCore;
using WidgetWebAPI.Models;

namespace WidgetWebAPI.Data
{
    public class WidgetDbContext : DbContext
    {
        public WidgetDbContext(DbContextOptions<WidgetDbContext> options) : base(options)
        {
            // this applies the data that was added during the OnModelCreating event
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Widget>().HasData(
                new List<Widget>()
                {
                    new Widget()
                    {
                        Id = 1,
                        Name = "Widget1",
                        Description = "Widget 1",
                        Cost = 1m,
                        UserDefinedSequenceNumber = 1
                    },
                    new Widget()
                    {
                        Id = 2,
                        Name = "Widget2",
                        Description = "Widget 2",
                        Cost = 10m,
                        UserDefinedSequenceNumber = 2
                    },
                    new Widget()
                    {
                        Id = 3,
                        Name = "Widget3",
                        Description = "Widget 3",
                        Cost = 100m,
                        UserDefinedSequenceNumber = 3
                    }
                });
        }

        public DbSet<Widget> Widgets => Set<Widget>();
    }
}