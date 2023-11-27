using PersonalCollectionManagement.Data.Entities;

namespace PersonalCollectionManagement.Data.Repositories.Contracts
{
    public interface ITopicRepository : IBaseRepository<TopicEntity>
    {
        Task<int> GetIdByTopicAsync(string topic);
    }
}
