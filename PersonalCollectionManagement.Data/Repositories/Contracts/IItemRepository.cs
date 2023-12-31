﻿using PersonalCollectionManagement.Data.Entities;

namespace PersonalCollectionManagement.Data.Repositories.Contracts
{
    public interface IItemRepository : IBaseRepository<ItemEntity>
    {
        Task<IEnumerable<ItemEntity>> GetAllCollectionItemsAsync(int id);
        Task<IEnumerable<ItemEntity>> GetLastAddedItemsAsync();
    }
}
