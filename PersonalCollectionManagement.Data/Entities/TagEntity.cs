namespace PersonalCollectionManagement.Data.Entities
{
    public class TagEntity : BaseEntity
    {
        public string Tag { get; set; } = null!;

        public ItemEntity Item { get; set; } = null!;
        public int ItemId { get; set; }
    }
}
