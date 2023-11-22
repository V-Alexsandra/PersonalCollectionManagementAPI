using Microsoft.AspNetCore.SignalR;
using PersonalCollectionManagement.Business.Services.Common;

namespace PersonalCollectionManagementAPI.Hubs
{
    public class CommentHub : Hub
    {
        private readonly ICommentService _commentService;

        public CommentHub(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task SendComment(int itemId)
        {
            var comments = await _commentService.GetAllCommentsForItemAsync(itemId);
            await Clients.All.SendAsync("ReceiveComments", comments);
        }
    }

}
