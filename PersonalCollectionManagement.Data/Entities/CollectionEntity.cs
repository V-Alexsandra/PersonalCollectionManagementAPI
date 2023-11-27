using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalCollectionManagement.Data.Entities
{
    public class CollectionEntity : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public UserEntity User { get; set; } = null!;
        public string UserId { get; set; }

        public TopicEntity Topic { get; set; } = null!;
        public int TopicId { get; set; }

        public IEnumerable<ItemEntity> Items { get; set; } = null!;
        public IEnumerable<CollectionFieldEntity> CollectionFields { get; set; } = null!;

        public string? ImageLink { get; set; }
    }
}
