using Microsoft.AspNetCore.Authorization;
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
                RegisterUserDtoValidator<RegisterUserDto> _validator = new RegisterUserDtoValidator<RegisterUserDto>();
                var validResult = _validator.Validate(model);
                if (!validResult.IsValid)
                {
                    return BadRequest("The entered password must have minimum 8 characters at least 1 alphabet, 1 number and 1 special Character.");
                }

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
        [Route("profile/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserProfile(string id)
        {
            try
            {
                var profile = await _userService.GetUserProfileAsync(id);
                return Ok(profile);
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

        [HttpPut]
        [Route("theme")]
        public async Task<IActionResult> ChangeUserThemeAsync(string theme, string id)
        {
            try
            {
                await _userService.ChangeUserThemeAsync(theme, id);
                return Ok(theme);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }     
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        [Route("language")]
        public async Task<IActionResult> ChangeUserLanguageAsync(string language, string id)
        {
            try
            {
                await _userService.ChangeUserLanguageAsync(language, id);
                return Ok(language);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        [Route("name")]
        [Authorize]
        public async Task<IActionResult> ChangeUserNameAsync(string name, string id)
        {
            try
            {
                await _userService.ChangeUserNameAsync(name, id);
                return Ok(name);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}

