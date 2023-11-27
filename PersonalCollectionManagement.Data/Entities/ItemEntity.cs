namespace PersonalCollectionManagement.Data.Entities
{
    public class ItemEntity : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? ImageLink { get; set; }
        public DateTime CreationDate { get; set; }

        public CollectionEntity Collection { get; set; } = null!;
        public int CollectionId { get; set; }

        public IEnumerable<ItemFieldValueEntity> ItemFieldValues { get; set; } = null!;
        public IEnumerable<TagEntity> Tags { get; set; } = null!;
        public IEnumerable<LikeEntity> Likes { get; set; } = null!;
        public IEnumerable<CommentEntity> Comments { get; set; } = null!;

    }
}
