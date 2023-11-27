using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PersonalCollectionManagement.Data.Entities;

namespace PersonalCollectionManagement.Data.Configuration
{
    public class CollectionEntityConfig : IEntityTypeConfiguration<CollectionEntity>
    {
        public void Configure(EntityTypeBuilder<CollectionEntity> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .UseIdentityColumn(1, 1)
                .ValueGeneratedOnAdd();

            builder.HasMany(c => c.Items)
               .WithOne(i => i.Collection)
               .HasForeignKey(i => i.CollectionId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.User)
                .WithMany(u => u.Collections)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
