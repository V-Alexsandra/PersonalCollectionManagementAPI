using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManagement.Business.Exceptions;
using PersonalCollectionManagement.Business.Services.Common;

namespace PersonalCollectionManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITopicService _topicService;

        public AdminController(IUserService userService, ITopicService topicService)
        {
            _userService = userService;
            _topicService = topicService;
        }

        [HttpGet]
        [Route("allusers")]
        [Authorize]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return Ok("User deleted.");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpPut]
        [Route("block/{id}")]
        [Authorize]
        public async Task<IActionResult> BlockUserAsync(string id)
        {
            try
            {
                await _userService.BlockUserAsync(id);
                return Ok("User blocked.");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpPut]
        [Route("unblock/{id}")]
        [Authorize]
        public async Task<IActionResult> UnblockUserAsync(string id)
        {
            try
            {
                await _userService.UnblockUserAsync(id);
                return Ok("User unblocked.");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpPut]
        [Route("roleadmin/{id}")]
        [Authorize]
        public async Task<IActionResult> ChangeUserRoleToAdminAsync(string id)
        {
            try
            {
                var result = await _userService.ChangeUserRoleToAdminAsync(id);
                if (result)
                {
                    return Ok("Role changed.");
                }
                else
                {
                    return StatusCode(500, "Internal Server Error.");
                }
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpPut]
        [Route("roleuser/{id}")]
        [Authorize]
        public async Task<IActionResult> ChangeUserRoleToUserAsync(string id)
        {
            try
            {
                var result = await _userService.ChangeUserRoleToUserAsync(id);
                if (result)
                {
                    return Ok("Role changed.");
                }
                else
                {
                    return StatusCode(500, "Internal Server Error.");
                }
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpDelete]
        [Route("deletetopic/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTopicAsync(int id)
        {
            try
            {
                await _topicService.DeleteTopicAsync(id);
                return Ok("Topic deleted.");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpPost]
        [Route("createtopic")]
        public async Task<IActionResult> CreateTopicAsync(string name)
        {
            try
            {
                await _topicService.CreateTopicAsync(name);
                return Ok("Topic created.");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("getrole/{id}")]
        public async Task<IActionResult> GetUserRoleAsync(string id)
        {
            try
            {
                var role = await _userService.GetUserRoleAsync(id);
                return Ok(role);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}