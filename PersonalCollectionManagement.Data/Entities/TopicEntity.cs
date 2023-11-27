namespace PersonalCollectionManagement.Data.Entities
{
    public class TopicEntity : BaseEntity
    {
        public string Name { get; set; } = null!;

        public IEnumerable<CollectionEntity> Collections { get; set; } = null!;
    }
}
