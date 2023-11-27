using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PersonalCollectionManagement.Business.DTOs.LikeDtos;
using PersonalCollectionManagement.Business.Services.Common;
using PersonalCollectionManagementAPI.Hubs;

namespace PersonalCollectionManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;
        private readonly IHubContext<LikeHub> _likeHubContext;

        public LikeController(ILikeService likeService, IHubContext<LikeHub> likeHubContext)
        {
            _likeService = likeService;
            _likeHubContext = likeHubContext;
        }

        [HttpGet]
        [Route("count/{itemId}")]
        public async Task<IActionResult> CountLikesAsync(int itemId)
        {
            try
            {
                var count = await _likeService.GetLikesCountAsync(itemId);
                return Ok(count);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("like")]
        [Authorize]
        public async Task<IActionResult> LikeAsync([FromBody] LikeForCreationDto model)
        {
            try
            {
                await _likeService.Like(model);

                await _likeHubContext.Clients.All.SendAsync("ReceiveLikeCount", model.ItemId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
