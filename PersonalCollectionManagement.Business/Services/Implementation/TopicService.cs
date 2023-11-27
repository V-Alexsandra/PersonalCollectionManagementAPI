using PersonalCollectionManagement.Business.Services.Common;
using PersonalCollectionManagement.Data.Entities;
using PersonalCollectionManagement.Data.Repositories.Contracts;

namespace PersonalCollectionManagement.Business.Services.Implementation
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository; 

        public TopicService(ITopicRepository topicRepository) 
        {
            _topicRepository = topicRepository;
        }

        public async Task CreateTopicAsync(string name)
        {
            var topic = new TopicEntity
            {
                Name = name,
            };

            await _topicRepository.CreateAsync(topic);
        }

        public async Task DeleteTopicAsync(int id)
        {
            await _topicRepository.DeleteAsync(await _topicRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<TopicEntity>> GetAllAsync()
        {
            return await _topicRepository.GetAllAsync();
        }

        public async Task<TopicEntity> GetByTopicIdAsync(int id)
        {
            return await _topicRepository.GetByIdAsync(id);
        }
    }
}
