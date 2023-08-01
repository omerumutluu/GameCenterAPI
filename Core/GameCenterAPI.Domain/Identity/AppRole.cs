using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace GameCenterAPI.Domain.Identity
{
    [CollectionName("roles")]
    public class AppRole : MongoIdentityRole<string>
    {
    }
}
