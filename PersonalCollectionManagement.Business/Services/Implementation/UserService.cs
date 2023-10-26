using Microsoft.AspNetCore.Identity;
using PersonalCollectionManagement.Business.DTOs.UserDtos;
using PersonalCollectionManagement.Business.Exceptions;
using PersonalCollectionManagement.Business.Services.Common;
using PersonalCollectionManagement.Data.Entities;

namespace PersonalCollectionManagement.Business.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<UserEntity> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task RegisterUserAsync(RegisterUserDto model)
        {
            await CheckOldUserAsync(model);

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Register Model is null");
            }

            if (model.Password == model.RepeatPassword)
            {
                var result = await CreateUserAsync(model);

                if (!result.Succeeded)
                {
                    throw new NotSucceededException("Register failed");
                }
            }
            else
            {
                throw new NotSucceededException("The password does not match. Please try again.");
            }
        }

        public async Task CheckOldUserAsync(RegisterUserDto model)
        {
            var oldUser = await _userManager.FindByEmailAsync(model.Email);

            if (oldUser != null)
            {
                throw new NotSucceededException("Register failed. User already exist");
            }
        }

        public async Task<IdentityResult> CreateUserAsync(RegisterUserDto model)
        {
            var identityUser = new UserEntity
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(identityUser, model.Password);

            return result;
        }

        public async Task<LoginSuccessDto> LoginUserAsync(LoginUserDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "LoginUserDto Model is null");
            }

            var user = await FindUserAsync(model);

            await TryLoginAsync(user, model);

            return new LoginSuccessDto
            {
                Id = user.Id,
                Token = _tokenService.GenerateAccessToken(await _tokenService.GetClaimsAsync(user.Email))
            };
        }

        public async Task<UserEntity> FindUserAsync(LoginUserDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                throw new NotFoundException("No such account exists. Please check the entered data.");
            }

            return user;
        }

        public async Task TryLoginAsync(UserEntity user, LoginUserDto model)
        {
            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
            {
                throw new NotSucceededException("Invalid password");
            }
        }

        public async Task ChangeUserNameAsync(string userName, string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            user.UserName = userName;
            IdentityResult result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new NotSucceededException($"Failed to change user name with id {id}");
            }
        }

        public async Task<ProfileDto> GetUserProfileAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            return new ProfileDto
            {
                UserName = user.UserName,
                UserEmail = user.Email
            };
        }
    }
}
