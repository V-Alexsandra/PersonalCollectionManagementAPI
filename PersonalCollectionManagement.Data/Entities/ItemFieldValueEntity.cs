namespace PersonalCollectionManagement.Data.Entities
{
    public class ItemFieldValueEntity : BaseEntity
    {
        public string Value { get; set; }

        public CollectionFieldEntity CollectionField { get; set; } = null!;
        public int CollectionFieldId { get; set; }

        public ItemEntity Item { get; set; } = null!;
        public int ItemId { get; set; }
    }
}
