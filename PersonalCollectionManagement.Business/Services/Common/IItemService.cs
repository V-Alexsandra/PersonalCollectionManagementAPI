﻿using PersonalCollectionManagement.Business.DTOs.ItemDtos;
using PersonalCollectionManagement.Business.DTOs.UserDtos;
using PersonalCollectionManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalCollectionManagement.Business.Services.Common
{
    public interface IItemService
    {
        Task<IEnumerable<ItemEntity>> GetAllAsync();
        Task CreateItemAsync(ItemForCreationDto model);
        //Task UpdateItemAsync(ItemForUpdateDto model);
        Task<ItemEntity> GetItemByIdAsync(int id);
        Task DeleteItemnAsync(int id);
        Task<IEnumerable<ItemEntity>> GetAllCollectionItemsAsync(int id);
    }
}
