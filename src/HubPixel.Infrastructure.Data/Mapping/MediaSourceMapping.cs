using HubPixel.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubPixel.Infrastructure.Data.Mapping;
public class MediaSourceMapping : IEntityTypeConfiguration<MediaSource>
{
    public void Configure(EntityTypeBuilder<MediaSource> builder)
    {
        builder.ToTable("MediaSources");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(m => m.Path)
            .IsRequired();

        builder.Property(m => m.CreatedAt)
            .IsRequired();

        builder.HasMany<Channel>()
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "MediaSourceChannels",
                j => j.HasOne<Channel>().WithMany().HasForeignKey("ChannelId"),
                j => j.HasOne<MediaSource>().WithMany().HasForeignKey("MediaSourceId")
            );
    }
}