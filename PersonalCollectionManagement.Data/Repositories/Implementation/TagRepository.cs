using Microsoft.EntityFrameworkCore;
using PersonalCollectionManagement.Data.Contexts;
using PersonalCollectionManagement.Data.Entities;
using PersonalCollectionManagement.Data.Repositories.Contracts;

namespace PersonalCollectionManagement.Data.Repositories.Implementation
{
    public class TagRepository : BaseRepository<TagEntity>, ITagRepository
    {
        protected IApplicationDbContext appContext;
        protected DbSet<TagEntity> DbSet;
        public TagRepository(IApplicationDbContext appContext) : base(appContext) 
        {
            DbSet = appContext.Set<TagEntity>();
        }

        public async Task<IEnumerable<TagEntity>> GetItemTagsAsync(int id)
        {
            return await DbSet
               .AsNoTracking()
               .Where(t => t.ItemId == id)
               .ToListAsync();
        }

        public async Task<IEnumerable<TagEntity>> GetUniqueTagsAsync()
        {
            var uniqueTags = await DbSet
            .Select(tag => tag.Tag)
            .Distinct()
            .ToListAsync();

            var tagEntities = uniqueTags.Select(tag => new TagEntity { Tag = tag });

            return tagEntities;
        }
    }
}
