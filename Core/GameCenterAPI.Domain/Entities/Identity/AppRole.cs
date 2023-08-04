using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace GameCenterAPI.Domain.Entities.Identity
{
    [CollectionName("roles")]
    public class AppRole : MongoIdentityRole<string>
    {
    }
}
