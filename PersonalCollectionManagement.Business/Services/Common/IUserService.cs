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
        Task<UserEntity> FindUserAsync(LoginUserDto model);
        Task TryLoginAsync(UserEntity user, LoginUserDto model);
        Task ChangeUserNameAsync(string userName, string id);
        Task<ProfileDto> GetUserProfileAsync(string id);
    }
}
