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

        public CollectionService(ICollectionRepository collectionRepository,
            ITopicRepository topicRepository,
            ICollectionFieldRepository collectionFieldRepository)
        {
            _collectionRepository = collectionRepository;
            _topicRepository = topicRepository;
            _collectionFieldRepository = collectionFieldRepository;
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

        public Task<IEnumerable<CollectionEntity>> GetAllUsersCollectionsAsync(string userId)
        {
            var collections = _collectionRepository.GetAllUsersCollectionsAsync(userId);

            if (collections == null)
            {
                throw new NotFoundException("Users haven't collections.");
            }

            return collections;
        }
    }
}
