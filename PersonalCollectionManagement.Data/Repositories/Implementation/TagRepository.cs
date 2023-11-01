using PersonalCollectionManagement.Data.Contexts;
using PersonalCollectionManagement.Data.Entities;
using PersonalCollectionManagement.Data.Repositories.Contracts;

namespace PersonalCollectionManagement.Data.Repositories.Implementation
{
    public class TagRepository : BaseRepository<TagEntity>, ITagRepository
    {
        public TagRepository(IApplicationDbContext appContext) : base(appContext) {}
    }
}
