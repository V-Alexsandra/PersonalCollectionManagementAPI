using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalCollectionManagement.Business.DTOs.UserDtos;
using PersonalCollectionManagement.Business.Exceptions;
using PersonalCollectionManagement.Business.Services.Common;
using PersonalCollectionManagement.Data.Entities;
using System.Runtime.CompilerServices;

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
                UserName = model.Name,
                Theme = "light",
                Language = "en"
            };

            var result = await _userManager.CreateAsync(identityUser, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(identityUser, "User");
            }

            return result;
        }

        public async Task<LoginSuccessDto> LoginUserAsync(LoginUserDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "LoginUserDto Model is null");
            }

            var user = await FindUserByEmailAsync(model);

            await TryLoginAsync(user, model);

            return new LoginSuccessDto
            {
                Id = user.Id,
                Token = _tokenService.GenerateAccessToken(await _tokenService.GetClaimsAsync(user.Email))
            };
        }

        public async Task<UserEntity> FindUserByEmailAsync(LoginUserDto model)
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
            var canLogin = await CanUserLogin(user.Id);

            if (!canLogin)
            {
                throw new NotSucceededException("Your account is temporarily blocked. If you believe this was an error, please contact your administrator.");
            }

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
            {
                throw new NotSucceededException("Invalid password");
            }
        }
        
        public async Task<bool> CanUserLogin(string id)
        {
            var user = await GetUserByIdAsync(id);

            return !user.IsBlocked;
        }


        public async Task ChangeUserNameAsync(string userName, string id)
        {
            var user = await GetUserByIdAsync(id);

            user.UserName = userName;

            await UpdateUserAsync(user);
        }

        public async Task UpdateUserAsync(UserEntity user)
        {
            IdentityResult result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new NotSucceededException($"Failed to update user");
            }
        }

        public async Task<ProfileDto> GetUserProfileAsync(string id)
        {
            var user = await GetUserByIdAsync(id);

            return new ProfileDto
            {
                UserName = user.UserName,
                UserEmail = user.Email,
                UserTheme = user.Theme,
                UserLanguage = user.Language
            };
        }

        public async Task ChangeUserThemeAsync(string theme, string id)
        {
            var user = await GetUserByIdAsync(id);
            user.Theme = theme;

            await UpdateUserAsync(user);
        }

        public async Task<UserEntity> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            return user;
        }

        public async Task ChangeUserLanguageAsync(string language, string id)
        {
            var user = await GetUserByIdAsync(id);
            user.Language = language;

            await UpdateUserAsync(user);
        }

        public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            return users;
        }

        public async Task BlockUserAsync(string id)
        {
            var user = await GetUserByIdAsync(id);

            user.IsBlocked = true;

            await UpdateUserAsync(user);
        }

        public async Task UnblockUserAsync(string id)
        {
            var user = await GetUserByIdAsync(id);
            
            user.IsBlocked = false;
              
            await UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await GetUserByIdAsync(id);

            await _userManager.DeleteAsync(user);
        }

        public async Task<bool> ChangeUserRoleToAdminAsync(string id)
        {
            var user = await GetUserByIdAsync(id);

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            var result = await _userManager.AddToRoleAsync(user, "Admin");

            return result.Succeeded;
        }

        public async Task<bool> ChangeUserRoleToUserAsync(string id)
        {
            var user = await GetUserByIdAsync(id);

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            var result = await _userManager.AddToRoleAsync(user, "User");

            return result.Succeeded;
        }
    }
}
