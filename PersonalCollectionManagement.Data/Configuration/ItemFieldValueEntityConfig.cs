using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalCollectionManagement.Data.Entities;
using System.Reflection.Emit;

namespace PersonalCollectionManagement.Data.Configuration
{
    public class ItemFieldValueEntityConfig
    {
        public void Configure(EntityTypeBuilder<ItemFieldValueEntity> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .UseIdentityColumn(1, 1)
            .ValueGeneratedOnAdd();

            builder.HasOne(ifv => ifv.Item)
                .WithMany(i => i.ItemFieldValues)
                .HasForeignKey(ifv => ifv.ItemId);
        }
    }
}
