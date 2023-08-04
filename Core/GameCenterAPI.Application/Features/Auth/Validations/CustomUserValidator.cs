using GameCenterAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace GameCenterAPI.Application.Features.Auth.Validations
{
    public class CustomUserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            throw new NotImplementedException();
        }
    }
}
