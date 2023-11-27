using PersonalCollectionManagement.Business.DTOs.LikeDtos;
using PersonalCollectionManagement.Business.Services.Common;
using PersonalCollectionManagement.Data.Entities;
using PersonalCollectionManagement.Data.Repositories.Contracts;

namespace PersonalCollectionManagement.Business.Services.Implementation
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;

        public LikeService(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        public async Task<int> GetLikesCountAsync(int itemId)
        {
            return await _likeRepository.GetLikesCountAsync(itemId);
        }

        public async Task Like(LikeForCreationDto model)
        {
            var existingLike = await _likeRepository.GetUserLike(model.UserId, model.ItemId);

            if (existingLike == null)
            {
                await CreateLikeAsync(model);
            }
            else
            {
                await UnlikeAsync(existingLike.Id);
            }
        }

        private async Task CreateLikeAsync(LikeForCreationDto model)
        {
            var newLike = new LikeEntity
            {
                ItemId = model.ItemId,
                UserId = model.UserId
            };

            await _likeRepository.CreateAsync(newLike);
        }

        private async Task UnlikeAsync(int likeId)
        {
            var likeToDelete = await _likeRepository.GetByIdAsync(likeId);
            if (likeToDelete != null)
            {
                await _likeRepository.DeleteAsync(likeToDelete);
            }
        }
    }
}