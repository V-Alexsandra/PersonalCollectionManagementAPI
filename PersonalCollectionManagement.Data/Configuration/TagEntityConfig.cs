using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalCollectionManagement.Data.Entities;
using System.Reflection.Emit;

namespace PersonalCollectionManagement.Data.Configuration
{
    public class TagEntityConfig
    {
        public void Configure(EntityTypeBuilder<TagEntity> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .UseIdentityColumn(1, 1)
            .ValueGeneratedOnAdd();

            builder.HasOne(t => t.Item)
               .WithMany(i => i.Tags)
               .HasForeignKey(t => t.ItemId);
        }
    }
}
