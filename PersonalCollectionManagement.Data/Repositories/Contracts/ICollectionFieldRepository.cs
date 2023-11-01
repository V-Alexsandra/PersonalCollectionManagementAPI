using PersonalCollectionManagement.Data.Entities;

namespace PersonalCollectionManagement.Data.Repositories.Contracts
{
    public interface ICollectionFieldRepository  : IBaseRepository<CollectionFieldEntity>
    {
        Task<IEnumerable<CollectionFieldEntity>> GetCollectionFieldsByCollectionIdAsync(int collectionId);
    }
}
