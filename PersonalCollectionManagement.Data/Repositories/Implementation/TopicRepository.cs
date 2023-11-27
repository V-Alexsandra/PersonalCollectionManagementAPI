using Microsoft.EntityFrameworkCore;
using PersonalCollectionManagement.Data.Contexts;
using PersonalCollectionManagement.Data.Entities;
using PersonalCollectionManagement.Data.Repositories.Contracts;

namespace PersonalCollectionManagement.Data.Repositories.Implementation
{
    public class TopicRepository : BaseRepository<TopicEntity>, ITopicRepository
    {

        protected IApplicationDbContext appContext;
        protected DbSet<TopicEntity> DbSet;

        public TopicRepository(IApplicationDbContext appContext) : base(appContext)
        {
            DbSet = appContext.Set<TopicEntity>();
        }

        public async Task<int> GetIdByTopicAsync(string topic)
        {
            var topicEntity = await DbSet
                .FirstOrDefaultAsync(t => t.Name == topic);

            if (topicEntity != null)
            {
                return topicEntity.Id;
            }
            
            return -1;
        }
    }
}
