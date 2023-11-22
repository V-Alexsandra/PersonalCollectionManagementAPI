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
        Task<IEnumerable<CollectionEntity>> GetFiveLargestAsync();
        Task<int> GetTopicIdAsync(string topic);
        Task<IEnumerable<CollectionEntity>> GetAllUsersCollectionsAsync(string userId);
        Task <IEnumerable<CollectionFieldEntity>> GetAllFieldsAsync(int id);
        Task<IEnumerable<ItemFieldValueEntity>> GetAllFieldValuesAsync(int id);
        Task<IEnumerable<ItemFieldValueEntity>> GetItemFieldValuesAsync(int fieldId, int itemId);
        Task UpdateCollectionAsync(CollectionForUpdateDto model);
    }
}
