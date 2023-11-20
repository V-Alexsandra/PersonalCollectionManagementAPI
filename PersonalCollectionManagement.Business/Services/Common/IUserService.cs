using Microsoft.AspNetCore.Identity;
using PersonalCollectionManagement.Business.DTOs.UserDtos;
using PersonalCollectionManagement.Data.Entities;

namespace PersonalCollectionManagement.Business.Services.Common
{
    public interface IUserService
    {
        Task RegisterUserAsync(RegisterUserDto model);
        Task CheckOldUserAsync(RegisterUserDto model);
        Task<IdentityResult> CreateUserAsync(RegisterUserDto model);
        Task<LoginSuccessDto> LoginUserAsync(LoginUserDto model);
        Task<UserEntity> FindUserByEmailAsync(LoginUserDto model);
        Task TryLoginAsync(UserEntity user, LoginUserDto model);
        Task<bool> CanUserLogin(string id);
        Task ChangeUserNameAsync(string userName, string id);
        Task UpdateUserAsync (UserEntity user);
        Task<ProfileDto> GetUserProfileAsync(string id);
        Task ChangeUserThemeAsync(string theme, string id);
        Task<UserEntity> GetUserByIdAsync(string id);
        Task ChangeUserLanguageAsync(string language, string id);
        Task<IEnumerable<UserEntity>> GetAllUsersAsync();
        Task BlockUserAsync(string id);
        Task UnblockUserAsync(string id);
        Task DeleteUserAsync(string id);
        Task<bool> ChangeUserRoleToAdminAsync(string id);
        Task<bool> ChangeUserRoleToUserAsync(string id);
        Task<string> GetUserThemeAsync(string id);
        Task<string> GetUserRoleAsync(string id);
    }
}
