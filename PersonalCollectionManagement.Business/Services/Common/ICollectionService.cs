using PersonalCollectionManagement.Business.DTOs.CollectionDtos;
using PersonalCollectionManagement.Data.Entities;

namespace PersonalCollectionManagement.Business.Services.Common
{
    public interface ICollectionService
    {
        Task<IEnumerable<CollectionEntity>> GetAllAsync();
        Task CreateCollectionAsync(CollectionForCreationDto model);
        Task<CollectionEntity> GetCollectionByIdAsync(int id);
        Task DeleteCollectionAsync(int id, string userId);
        Task<IEnumerable<CollectionEntity>> GetFivaLargestAsync();
        Task<int> GetTopicIdAsync(string topic);
        Task<IEnumerable<CollectionEntity>> GetAllUsersCollectionsAsync(string userId);
    }
}
