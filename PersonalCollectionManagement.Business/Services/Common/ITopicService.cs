using PersonalCollectionManagement.Data.Entities;

namespace PersonalCollectionManagement.Business.Services.Common
{
    public interface ITopicService
    {
        Task CreateTopicAsync(string name);
        Task DeleteTopicAsync(int id);
        Task<IEnumerable<TopicEntity>> GetAllAsync();
    }
}
