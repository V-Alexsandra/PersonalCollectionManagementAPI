using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalCollectionManagement.Data.Entities
{
    public class CommentEntity : BaseEntity
    {
        public string Text { get; set; } = null!;
        public DateTime Date { get; set; }

        public ItemEntity Item { get; set; } = null!;
        public int ItemId { get; set; }

        public UserEntity User { get; set; } = null!;
        public string UserId { get; set; }
    }
}
