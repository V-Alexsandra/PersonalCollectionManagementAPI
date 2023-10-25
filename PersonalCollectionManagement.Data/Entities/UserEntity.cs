using Microsoft.AspNetCore.Identity;

namespace PersonalCollectionManagement.Data.Entities
{
    public class UserEntity : IdentityUser
    {
        public string? Language { get; set; }
        public string? Theme { get; set; }
    }
}
