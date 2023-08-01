using Microsoft.AspNetCore.Identity;

namespace GameCenterAPI.Application.Features.Auth.Validations
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DefaultError() => new() { Code = nameof(DefaultError), Description = ErrorMessages.DefaultError };
        public override IdentityError ConcurrencyFailure() => new() { Code = nameof(ConcurrencyFailure), Description = ErrorMessages.ConcurrencyFailure };
        public override IdentityError PasswordMismatch() => new() { Code = nameof(PasswordMismatch), Description = ErrorMessages.PasswordMissMatch };
        public override IdentityError InvalidToken() => new() { Code = nameof(InvalidToken), Description = ErrorMessages.InvalidToken };
        public override IdentityError RecoveryCodeRedemptionFailed() => new() { Code = nameof(RecoveryCodeRedemptionFailed), Description = ErrorMessages.RecoveryCodeRedemptionFailed };
        public override IdentityError LoginAlreadyAssociated() => new() { Code = nameof(LoginAlreadyAssociated), Description = ErrorMessages.LoginAlreadyAssociated };
        public override IdentityError InvalidUserName(string userName) => new() { Code = nameof(InvalidUserName), Description = ErrorMessages.InvalidUserName };
        public override IdentityError InvalidEmail(string email) => new() { Code = nameof(InvalidEmail), Description = ErrorMessages.InvalidEmail };
        public override IdentityError DuplicateUserName(string userName) => new() { Code = nameof(DuplicateUserName), Description = ErrorMessages.DuplicateUserName };
        public override IdentityError DuplicateEmail(string email) => new() { Code = nameof(DuplicateEmail), Description = ErrorMessages.DuplicateEmail };
        public override IdentityError InvalidRoleName(string roleName) => new() { Code = nameof(InvalidRoleName), Description = ErrorMessages.InvalidRoleName };
        public override IdentityError DuplicateRoleName(string roleName) => new() { Code = nameof(DuplicateRoleName), Description = ErrorMessages.DuplicateRoleName };
        public override IdentityError UserAlreadyHasPassword() => new() { Code = nameof(UserAlreadyHasPassword), Description = ErrorMessages.UserAlreadyHasPassword };
        public override IdentityError UserLockoutNotEnabled() => new() { Code = nameof(UserLockoutNotEnabled), Description = ErrorMessages.UserLockoutNotEnabled };
        public override IdentityError UserAlreadyInRole(string role) => new() { Code = nameof(UserAlreadyInRole), Description = ErrorMessages.UserAlreadyInRole };
        public override IdentityError UserNotInRole(string role) => new() { Code = nameof(UserNotInRole), Description = ErrorMessages.UserNotInRole };
        public override IdentityError PasswordTooShort(int length) => new() { Code = nameof(PasswordTooShort), Description = ErrorMessages.PasswordTooShort };
        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars) => new() { Code = nameof(PasswordRequiresUniqueChars), Description = ErrorMessages.PasswordRequiresUniqueChars };
        public override IdentityError PasswordRequiresNonAlphanumeric() => new() { Code = nameof(PasswordRequiresNonAlphanumeric), Description = ErrorMessages.PasswordRequiresNonAlphanumeric };
        public override IdentityError PasswordRequiresDigit() => new() { Code = nameof(PasswordRequiresDigit), Description = ErrorMessages.PasswordRequiresDigit };
        public override IdentityError PasswordRequiresLower() => new() { Code = nameof(PasswordRequiresLower), Description = ErrorMessages.PasswordRequiresLower };
        public override IdentityError PasswordRequiresUpper() => new() { Code = nameof(PasswordRequiresUpper), Description = ErrorMessages.PasswordRequiresUpper };


    }
}
//PasswordRequiresNonAlphanumeric
//PasswordRequiresDigit
//PasswordRequiresLower
//PasswordRequiresUpper

