using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalCollectionManagement.Data.Entities
{
    public class ItemEntity : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Tag { get; set; }
        public List<FieldEntity> Fields { get; set; } = null!;
        public string? ImageLink { get; set; }

        [ForeignKey("CollectionId")]
        public CollectionEntity Collection { get; set; } = null!;
        public int CollectionId { get; set; }
        public IEnumerable<LikeEntity> Likes { get; set; } = null!;
        public IEnumerable<CommentEntity> Comments { get; set; } = null!;

    }
}
