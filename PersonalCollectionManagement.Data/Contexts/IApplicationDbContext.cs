using Microsoft.EntityFrameworkCore;
using PersonalCollectionManagement.Data.Entities;

namespace PersonalCollectionManagement.Data.Contexts
{
    public interface IApplicationDbContext
    {
        DbSet<UserEntity> Users { get; set; }
        DbSet<CollectionEntity> Collections { get; set; }
        DbSet<FieldEntity> Fields { get; set; }
        DbSet<CommentEntity> Comments { get; set; }
        DbSet<ItemEntity> Items { get; set; }
        DbSet<LikeEntity> Likes { get; set; }
        DbSet<TopicEntity> Topics { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync();
    }
}
