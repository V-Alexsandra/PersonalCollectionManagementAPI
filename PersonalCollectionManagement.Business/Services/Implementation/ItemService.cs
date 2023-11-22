using Microsoft.AspNetCore.Identity;
using PersonalCollectionManagement.Business.DTOs.ItemDtos;
using PersonalCollectionManagement.Business.Exceptions;
using PersonalCollectionManagement.Business.Services.Common;
using PersonalCollectionManagement.Data.Contexts;
using PersonalCollectionManagement.Data.Entities;
using PersonalCollectionManagement.Data.Repositories.Contracts;

namespace PersonalCollectionManagement.Business.Services.Implementation
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IItemFieldValueRepository _itemFieldValueRepository;
        private readonly ICollectionFieldRepository _collectionFieldRepository;
        private readonly ICollectionRepository _collectionRepository;
        private readonly UserManager<UserEntity> _userManager;

        public ItemService(IItemRepository itemRepository,
            ITagRepository tagRepository,
            IItemFieldValueRepository itemFieldValueRepository,
            ICollectionFieldRepository collectionFieldRepository,
            ICollectionRepository collectionRepository,
            UserManager<UserEntity> userManager)
        {
            _itemRepository = itemRepository;
            _tagRepository = tagRepository;
            _itemFieldValueRepository = itemFieldValueRepository;
            _collectionFieldRepository = collectionFieldRepository;
            _collectionRepository = collectionRepository;
            _userManager = userManager;
        }

        public async Task CreateItemAsync(ItemForCreationDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "ItemForCreationDto Model is null");
            }

            using (var context = new ApplicationDbContext())
            {
                var item = await CreateAsync(model);
                await CreateTagsEntities(model.Tags, item.Id);
                await CreateItemFieldsValuesEntities(model.ItemFieldValues, item.Id);
            }
        }

        private async Task<ItemEntity> CreateAsync(ItemForCreationDto model)
        {
            var item = new ItemEntity
            {
                Name = model.Name,
                ImageLink = model.ImageLink,
                CreationDate = DateTime.Now,
                CollectionId = model.CollectionId
            };

            return await _itemRepository.CreateAsync(item);
        }

        private async Task CreateTagsEntities(List<TagDto> tags, int itemId)
        {
            foreach (TagDto tag in tags)
            {
                var tagEntity = new TagEntity
                {
                    Tag = tag.Tag,
                    ItemId = itemId
                };

                await _tagRepository.CreateAsync(tagEntity);
            }
        }

        private async Task CreateItemFieldsValuesEntities(List<ItemFieldValuesDto> values, int itemId)
        {
            foreach (ItemFieldValuesDto value in values)
            {
                var collectionField = await _collectionFieldRepository.GetByIdAsync(value.CollectionFieldId);

                if (collectionField == null)
                {
                    throw new NotFoundException("Cannot found collection field.");
                }


                if (IsValueOfType(value.Value, collectionField.Type))
                {
                    var newValue = value.Value;
                    var valueEntity = new ItemFieldValueEntity
                    {
                        Value = newValue,
                        CollectionFieldId = value.CollectionFieldId,
                        ItemId = itemId
                    };

                    await _itemFieldValueRepository.CreateAsync(valueEntity);
                }
                else
                {
                    throw new NotSucceededException("Your value does not match the field type.");
                }
            }
        }

        private bool IsValueOfType(string value, string typeName)
        {
            Type targetType = GetTypeFromTypeName(typeName);

            if (targetType != null)
            {
                try
                {
                    object convertedValue = Convert.ChangeType(value, targetType);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new NotSucceededException("Your value does not match the field type.");
                }
            }
            else
            {
                throw new NotSucceededException("Invalid type name.");
            }
        }

        private Type GetTypeFromTypeName(string typeName)
        {
            switch (typeName.ToLower())
            {
                case "int":
                    return typeof(int);
                case "string":
                    return typeof(string);
                case "bool":
                    return typeof(bool);
                case "double":
                    return typeof(double);
                case "date":
                    return typeof(DateTime);
                default:
                    throw new NotSucceededException("Invalid type name.");
            }
        }

        public async Task DeleteItemnAsync(int id)
        {
            var item = await _itemRepository.GetByIdAsync(id);

            if (item == null)
            {
                throw new NotFoundException("No such item exists.");
            }

            await _itemRepository.DeleteAsync(item);
        }

        public async Task<IEnumerable<ItemEntity>> GetAllAsync()
        {
            var items = await _itemRepository.GetAllAsync();

            if (items == null)
            {
                throw new NotFoundException("No such items exists.");
            }

            return items;
        }

        public async Task<ItemEntity> GetItemByIdAsync(int id)
        {
            var item = await _itemRepository.GetByIdAsync(id);

            if (item == null)
            {
                throw new NotFoundException("No such item exists.");
            }

            return item;
        }

        public async Task<IEnumerable<ItemEntity>> GetAllCollectionItemsAsync(int id)
        {
            return await _itemRepository.GetAllCollectionItemsAsync(id);
        }

        public async Task<IEnumerable<TagEntity>> GetAllTagsAsync()
        {
            return await _tagRepository.GetAllAsync();
        }

        public async Task UpdateItemAsync(ItemForUpdateDto model)
        {
            var item = await GetItemByIdAsync(model.Id);

            item.Name = model.Name;

            await _itemRepository.UpdateAsync(item);

            foreach (ItemFieldValuesForUpdateDto value in model.ItemFieldValues)
            {
                var collectionField = await _collectionFieldRepository.GetByIdAsync(value.CollectionFieldId);

                if (collectionField == null)
                {
                    throw new NotFoundException("Cannot found collection field.");
                }

                var itemFieldValue = await _itemFieldValueRepository.GetByIdAsync(value.Id); 

                if (itemFieldValue == null)
                {
                    throw new NotFoundException("Cannot found item field value.");
                }

                if (IsValueOfType(value.Value, collectionField.Type))
                {
                    var newValue = value.Value;

                    itemFieldValue.Value = newValue;
                    
                    await _itemFieldValueRepository.UpdateAsync(itemFieldValue);
                }
                else
                {
                    throw new NotSucceededException("Your value does not match the field type.");
                }
            }
        }

        public async Task UpdateTagsEntities(TagForUpdateDto tag)
        {
            var oldTag = await _tagRepository.GetByIdAsync(tag.Id);

            if (oldTag == null)
            {
                throw new NotFoundException("Tag not found");
            }

            oldTag.Tag = tag.Tag;
            
            await _tagRepository.UpdateAsync(oldTag);
        }

        public async Task<IEnumerable<LastAddedItemForWiewDto>> GetLastAddedItemsAsync()
        {
            var lastAddedItems = await _itemRepository.GetLastAddedItemsAsync();

            var lastAddedItemsForWiew = new List<LastAddedItemForWiewDto>(); 

            foreach (var item in lastAddedItems)
            {
                var collection = await _collectionRepository.GetByIdAsync(item.CollectionId);

                if (collection == null)
                {
                    throw new NotFoundException("Collection not found");
                }

                var author = await _userManager.FindByIdAsync(collection.UserId);

                if(author == null)
                {
                    throw new NotFoundException("Author not found");
                }

                var lastAdded = new LastAddedItemForWiewDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    CollectionName = collection.Name,
                    Author = await _userManager.GetUserNameAsync(author),
                    CollectionId = item.CollectionId
                };

                lastAddedItemsForWiew.Add(lastAdded);
            }

            return lastAddedItemsForWiew;
        }

        public async Task<IEnumerable<TagEntity>> GetItemTagsAsync(int id)
        {
            return await _tagRepository.GetItemTagsAsync(id);
        }

        public async Task<IEnumerable<TagEntity>> GetUniqueTagsAsync()
        {
            return await _tagRepository.GetUniqueTagsAsync();
        }
    }
}
