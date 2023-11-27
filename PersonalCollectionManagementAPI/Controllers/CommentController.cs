using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PersonalCollectionManagement.Business.DTOs.CommentDtos;
using PersonalCollectionManagement.Business.Services.Common;
using PersonalCollectionManagementAPI.Hubs;

namespace PersonalCollectionManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IHubContext<CommentHub> _commentHubContext;

        public CommentController(ICommentService commentService, IHubContext<CommentHub> commentHubContext)
        {
            _commentService = commentService;
            _commentHubContext = commentHubContext;
        }

        [HttpPost]
        [Route("create")]
        [Authorize]
        public async Task<IActionResult> CreateCommentAsync([FromBody] CommentForCreationDto model)
        {
            try
            {
                await _commentService.CreateCommentAsync(model);
                await _commentHubContext.Clients.All.SendAsync("ReceiveComments", model.ItemId);
                return Ok("Comment created.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getcomments/{id}")]
        public async Task<IActionResult> GetItemCommentsAsync(int id)
        {
            try
            {
                var comments = await _commentService.GetAllCommentsForItemAsync(id);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}