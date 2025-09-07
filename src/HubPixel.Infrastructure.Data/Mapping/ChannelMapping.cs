using HubPixel.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubPixel.Infrastructure.Data.Mapping;

public class ChannelMapping : IEntityTypeConfiguration<Channel>
{
    public void Configure(EntityTypeBuilder<Channel> builder)
    {
        builder.ToTable("Channels");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(c => c.Description)
            .HasMaxLength(10000);

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.OwnsOne(c => c.UrlStream, nav =>
        {
            nav.Property(url => url.Value)
                .HasColumnName("UrlStream")
                .IsRequired();
        });

        builder.HasMany<Category>()
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ChannelCategories",
                j => j.HasOne<Category>().WithMany().HasForeignKey("CategoryId"),
                j => j.HasOne<Channel>().WithMany().HasForeignKey("ChannelId")
            );
    }
}
