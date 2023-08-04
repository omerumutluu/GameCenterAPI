using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace GameCenterAPI.Domain.Entities.Identity
{
    [CollectionName("users")]
    public class AppUser : MongoIdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public List<string> OperationClaims { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
