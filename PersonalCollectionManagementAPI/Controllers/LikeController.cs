using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManagement.Business.DTOs.LikeDtos;
using PersonalCollectionManagement.Business.Services.Common;

namespace PersonalCollectionManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
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
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
