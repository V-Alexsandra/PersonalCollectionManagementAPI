using PersonalCollectionManagement.Business.DTOs.ItemDtos;
using PersonalCollectionManagement.Business.Services.Common;
using PersonalCollectionManagement.Data.Entities;

namespace PersonalCollectionManagement.Business.Services.Implementation
{
    public class ItemService : IItemService
    {
        public Task CreateItemAsync(ItemForCreationDto model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteItemnAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemEntity>> GetAllAync()
        {
            throw new NotImplementedException();
        }

        public Task<ItemEntity> GetItemByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateItemAsync(ItemForUpdateDto model)
        {
            throw new NotImplementedException();
        }
    }
}
