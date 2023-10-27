using Microsoft.AspNetCore.Identity;

namespace PersonalCollectionManagement.Data.Entities
{
    public class UserEntity : IdentityUser
    {
        public string Language { get; set; } = null!;
        public string Theme { get; set; } = null!;
        public bool IsBlocked { get; set; }
        public IEnumerable<CollectionEntity> Collections { get; set; } = null!;
        public IEnumerable<LikeEntity> Likes { get; set; } = null!;
        public IEnumerable<CommentEntity> Comments { get; set; } = null!;
    }
}
