using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManagement.Business.DTOs.CommentDtos;
using PersonalCollectionManagement.Business.DTOs.ItemDtos;
using PersonalCollectionManagement.Business.Exceptions;
using PersonalCollectionManagement.Business.Services.Common;
using PersonalCollectionManagement.Business.Services.Implementation;

namespace PersonalCollectionManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [Route("create")]
        [Authorize]
        public async Task<IActionResult> CreateCommentAsync([FromBody] CommentForCreationDto model)
        {
            try
            {
                await _commentService.CreateCommentAsync(model);
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