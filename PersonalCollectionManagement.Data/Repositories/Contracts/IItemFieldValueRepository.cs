﻿using PersonalCollectionManagement.Data.Entities;

namespace PersonalCollectionManagement.Data.Repositories.Contracts
{
    public interface IItemFieldValueRepository : IBaseRepository<ItemFieldValueEntity>
    {
        Task<IEnumerable<ItemFieldValueEntity>> GetValueByFieldIdAsync(int id);
        Task<IEnumerable<ItemFieldValueEntity>> GetItemValueByFieldIdAsync(int fieldId, int itemId);
    }
}
