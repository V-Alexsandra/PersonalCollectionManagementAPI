using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManagement.Business.DTOs.UserDtos;
using PersonalCollectionManagement.Business.Exceptions;
using PersonalCollectionManagement.Business.Services.Common;

namespace PersonalCollectionManagementAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserDto model)
        {
            try
            {
                await _userService.RegisterUserAsync(model);
                return Ok("Register Sucseed");
            }
            catch (NotSucceededException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserDto model)
        {
            try
            {
                var login = await _userService.LoginUserAsync(model);
                return Ok(login);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (NotSucceededException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetUserProfile(string id)
        {
            var profile = await _userService.GetUserProfileAsync(id);
            return Ok(profile);
        }
    }
}

