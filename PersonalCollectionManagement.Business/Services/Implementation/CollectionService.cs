using PersonalCollectionManagement.Business.DTOs.CollectionDtos;
using PersonalCollectionManagement.Business.Exceptions;
using PersonalCollectionManagement.Business.Services.Common;
using PersonalCollectionManagement.Data.Entities;
using PersonalCollectionManagement.Data.Repositories.Contracts;

namespace PersonalCollectionManagement.Business.Services.Implementation
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly ITopicRepository _topicRepository;
        private readonly ICollectionFieldRepository _collectionFieldRepository;
        private readonly IItemFieldValueRepository _itemFieldValueRepository;

        public CollectionService(ICollectionRepository collectionRepository,
            ITopicRepository topicRepository,
            ICollectionFieldRepository collectionFieldRepository,
            IItemFieldValueRepository itemFieldValueRepository)
        {
            _collectionRepository = collectionRepository;
            _topicRepository = topicRepository;
            _collectionFieldRepository = collectionFieldRepository;
            _itemFieldValueRepository = itemFieldValueRepository;
        }

        public async Task<IEnumerable<CollectionEntity>> GetAllAsync()
        {
            return await _collectionRepository.GetAllAsync();
        }

        public async Task CreateCollectionAsync(CollectionForCreationDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "CollectionForCreatingDto Model is null");
            }

            var collection = await CreateAsync(model);
            await CreateCollectionFieldEntities(model.Fields, collection.Id);
        }

        private async Task<CollectionEntity> CreateAsync(CollectionForCreationDto model)
        {
            var collection = new CollectionEntity
            {
                Name = model.Name,
                Description = model.Description,
                TopicId = await GetTopicIdAsync(model.Topic),
                UserId = model.UserId,
                ImageLink = model.ImageLink
            };

            return await _collectionRepository.CreateAsync(collection);
        }

        private async Task CreateCollectionFieldEntities(List<CollectionFieldDto> fields, int collectionId)
        {
            foreach (CollectionFieldDto field in fields)
            {
                var collectionField = new CollectionFieldEntity
                {
                    Type = field.Type,
                    Name = field.Name,
                    CollectionId = collectionId
                };

                await _collectionFieldRepository.CreateAsync(collectionField);
            }
        }

        public async Task<int> GetTopicIdAsync(string topic)
        {
            var id = await _topicRepository.GetIdByTopicAsync(topic);

            if (id == -1)
            {
                throw new NotFoundException("Topic doesn't exist");
            }

            return id;
        }

        public async Task DeleteCollectionAsync(int id, string userId)
        {
            var collecion = await GetCollectionByIdAsync(id);

            if (collecion.UserId == userId)
            {
                await _collectionRepository.DeleteAsync(collecion);
            }
            else
            {
                throw new NotSucceededException("You can't delete this collection");
            }
        }

        public async Task<CollectionEntity> GetCollectionByIdAsync(int id)
        {
            var collecion = await _collectionRepository.GetByIdAsync(id);

            if (collecion == null)
            {
                throw new NotFoundException("No such collecion exists.");
            }

            return collecion;
        }

        public async Task<IEnumerable<CollectionEntity>> GetFivaLargestAsync()
        {
            var largestCollections = await _collectionRepository.GetFiveLargestAsync();

            if (largestCollections == null)
            {
                throw new NotSucceededException("Cannot find five largest collections");
            }

            return largestCollections;
        }

        public async Task<IEnumerable<CollectionEntity>> GetAllUsersCollectionsAsync(string userId)
        {
            var collections = await _collectionRepository.GetAllUsersCollectionsAsync(userId);

            if (collections == null)
            {
                throw new NotFoundException("Users haven't collections.");
            }

            return collections;
        }

        public async Task<IEnumerable<CollectionFieldEntity>> GetAllFieldsAsync(int id)
        {
            var collectionFields = await _collectionFieldRepository.GetCollectionFieldsByCollectionIdAsync(id);

            if (collectionFields == null)
            {
                throw new NotFoundException("Collection fields not found.");
            }

            return collectionFields;
        }

        public async Task<IEnumerable<ItemFieldValueEntity>> GetAllFieldValuesAsync(int id)
        {
            var values = await _itemFieldValueRepository.GetValueByFieldIdAsync(id);

            if (values == null)
            {
                throw new NotFoundException("Fields values empty.");
            }

            return values;
        }

        public async Task UpdateCollectionAsync(CollectionForUpdateDto model)
        {
            var collection = await GetCollectionByIdAsync(model.Id);

            await UpdateCollectionProperties(collection, model);

            await _collectionRepository.UpdateAsync(collection);

            foreach (CollectionFieldForUpdateDto field in model.Fields)
            {
                await UpdateCollectionFieldAsync(field);
            }
        }

        private async Task UpdateCollectionProperties(CollectionEntity collection, CollectionForUpdateDto model)
        {
            collection.Name = model.Name;
            collection.Description = model.Description;
            collection.TopicId = await _topicRepository.GetIdByTopicAsync(model.Topic); 
        }

        private async Task UpdateCollectionFieldAsync(CollectionFieldForUpdateDto field)
        {
            var collectionField = await _collectionFieldRepository.GetByIdAsync(field.Id);

            if (collectionField == null)
            {
                throw new NotFoundException("Field not found.");
            }

            UpdateCollectionFieldProperties(collectionField, field);
            await _collectionFieldRepository.UpdateAsync(collectionField);
        }

        private void UpdateCollectionFieldProperties(CollectionFieldEntity collectionField, CollectionFieldForUpdateDto field)
        {
            collectionField.Name = field.Name;
            collectionField.Type = field.Type;
        }
    }
}
