using PersonalCollectionManagement.Data.Entities;

namespace PersonalCollectionManagement.Data.Repositories.Contracts
{
    public interface ICollectionRepository : IBaseRepository<CollectionEntity>
    {
        Task<IEnumerable<CollectionEntity>> GetFiveLargestCollectionsAsync();
        Task<IEnumerable<CollectionEntity>> GetAllUsersCollectionsAsync(string userId);

    }
}
