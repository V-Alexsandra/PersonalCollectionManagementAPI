using PersonalCollectionManagement.Business.DTOs.CommentDtos;
using PersonalCollectionManagement.Data.Entities;

namespace PersonalCollectionManagement.Business.Services.Common
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentEntity>> GetAllCommentsForItemAsync(int id);
        Task CreateCommentAsync(CommentForCreationDto model);
    }
}
