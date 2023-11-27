using PersonalCollectionManagement.Business.DTOs.CommentDtos;
using PersonalCollectionManagement.Business.Services.Common;
using PersonalCollectionManagement.Data.Entities;
using PersonalCollectionManagement.Data.Repositories.Contracts;

namespace PersonalCollectionManagement.Business.Services.Implementation
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task CreateCommentAsync(CommentForCreationDto model)
        {
            var comment = CreateCommentEntity(model);
            await _commentRepository.CreateAsync(comment);
        }

        public async Task<IEnumerable<CommentEntity>> GetAllCommentsForItemAsync(int id)
        {
            return await _commentRepository.GetAllCommentsForItemAsync(id);
        }

        private CommentEntity CreateCommentEntity(CommentForCreationDto model)
        {
            return new CommentEntity
            {
                Text = model.Text,
                Date = DateTime.Now,
                ItemId = model.ItemId,
                UserId = model.UserId
            };
        }
    }
}