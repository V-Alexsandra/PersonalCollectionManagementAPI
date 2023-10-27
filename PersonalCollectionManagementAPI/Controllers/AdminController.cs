using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManagement.Business.Exceptions;
using PersonalCollectionManagement.Business.Services.Common;

namespace PersonalCollectionManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("allusers")]
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
    }
}