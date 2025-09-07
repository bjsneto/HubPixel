using HubPixel.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HubPixel.Infrastructure.Data;
public class HubPixelDbContext : DbContext
{
    public HubPixelDbContext(DbContextOptions<HubPixelDbContext> options) 
        : base(options) { }

    public DbSet<MediaSource> MediaSources { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HubPixelDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
