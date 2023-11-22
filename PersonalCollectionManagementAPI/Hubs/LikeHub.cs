using Microsoft.AspNetCore.SignalR;
using PersonalCollectionManagement.Business.Services.Common;

namespace PersonalCollectionManagementAPI.Hubs
{
    public class LikeHub : Hub
    {
        private readonly ILikeService _likeService;

        public LikeHub(ILikeService likeService)
        {
            _likeService = likeService;
        }
        public async Task SendLikeCount(int itemId)
        {
            var count = await _likeService.GetLikesCountAsync(itemId);
            await Clients.All.SendAsync("ReceiveLikeCount", count);
        }
    }

}
