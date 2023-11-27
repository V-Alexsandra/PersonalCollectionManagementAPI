using PersonalCollectionManagement.Data.Entities;

namespace PersonalCollectionManagement.Data.Repositories.Contracts
{
    public interface ITagRepository : IBaseRepository<TagEntity>
    {
        Task<IEnumerable<TagEntity>> GetItemTagsAsync(int id);
        Task<IEnumerable<TagEntity>> GetUniqueTagsAsync();
        Task<List<int>> GetItemsIdByTag(string tag);
    }
}
