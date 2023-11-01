namespace PersonalCollectionManagement.Data.Entities
{
    public class CollectionFieldEntity : BaseEntity
    {
        public string Type { get; set; } = null!;
        public string Name { get; set; } = null!;

        public CollectionEntity Collection { get; set; } = null!;
        public int CollectionId { get; set; }

        public IEnumerable<ItemFieldValueEntity> ItemFieldValues { get; set; } = null!;
    }
}
