using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalCollectionManagement.Data.Entities;

namespace PersonalCollectionManagement.Data.Configuration
{
    public class CollectionFieldEntityConfig : IEntityTypeConfiguration<CollectionFieldEntity>
    {
        public void Configure(EntityTypeBuilder<CollectionFieldEntity> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .UseIdentityColumn(1, 1)
                .ValueGeneratedOnAdd();

            builder.HasMany(cf => cf.ItemFieldValues)
                        .WithOne(ifv => ifv.CollectionField)
                        .HasForeignKey(ifv => ifv.CollectionFieldId)
                        .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
