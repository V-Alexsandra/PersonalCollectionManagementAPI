using PersonalCollectionManagement.Business.DTOs.LikeDtos;

namespace PersonalCollectionManagement.Business.Services.Common
{
    public interface ILikeService
    {
        Task<int> GetLikesCountAsync(int itemId);
        Task Like(LikeForCreationDto model);
    }
}
