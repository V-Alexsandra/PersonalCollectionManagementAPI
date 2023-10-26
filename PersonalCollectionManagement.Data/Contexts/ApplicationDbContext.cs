using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalCollectionManagement.Data.Entities;
using System.Reflection;

namespace PersonalCollectionManagement.Data.Contexts
{
    public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
    {
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<CollectionEntity> Collections { get; set; } = null!;
        public DbSet<FieldEntity> Fields { get; set; } = null!;
        public DbSet<CommentEntity> Comments { get; set; } = null!;
        public DbSet<ItemEntity> Items { get; set; } = null!;
        public DbSet<LikeEntity> Likes { get; set; } = null!;
        public DbSet<TopicEntity> Topics { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public async Task<int> SaveChangesAsync() => await base.SaveChangesAsync();
    }
}