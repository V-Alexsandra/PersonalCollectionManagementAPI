using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalCollectionManagement.Data.Entities
{
    public class FieldEntity : BaseEntity
    {
        public string Type { get; set; } = null!;
        public string Value { get; set; } = null!;

        [ForeignKey("ItemId")]
        public ItemEntity Item { get; set; } = null!;
        public int ItemId { get; set; }
    }
}
