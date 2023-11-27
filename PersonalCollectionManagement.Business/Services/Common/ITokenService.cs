using System.Security.Claims;

namespace PersonalCollectionManagement.Business.Services.Common
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        Task<IEnumerable<Claim>> GetClaimsAsync(string email);
    }
}
