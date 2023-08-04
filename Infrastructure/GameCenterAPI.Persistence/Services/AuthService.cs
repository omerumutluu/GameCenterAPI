using GameCenterAPI.Application.Abstraction.Services;
using GameCenterAPI.Application.Abstraction.Token;
using GameCenterAPI.Application.DTOs;
using GameCenterAPI.Application.DTOs.User;
using GameCenterAPI.Application.Exceptions;
using GameCenterAPI.Application.Features.Auth;
using GameCenterAPI.Application.Features.Auth.Validations;
using GameCenterAPI.Application.Helpers;
using GameCenterAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using ZstdSharp.Unsafe;

namespace GameCenterAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly IUserService _userService;
        readonly ITokenHandler _tokenHandler;
        readonly IMailService _mailService;

        public AuthService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IUserService userService,
            ITokenHandler tokenHandler,
            IMailService mailService

            )
        {
            _userManager = userManager;
            _mailService = mailService;
            _userService = userService;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
        }

        public Task<Token> FacebookLoginAsync(string authToken)
        {
            throw new NotImplementedException();
        }

        public Task<Token> GoogleLoginAsync(string authToken)
        {
            throw new NotImplementedException();
        }

        public async Task<(LoginUserResponse user, Token token)> LoginAsync(string emailOrUsername, string password)
        {
            AppUser? user = await _userManager.FindByEmailAsync(emailOrUsername);

            if (user == null)
                user = await _userManager.FindByNameAsync(emailOrUsername);

            if (user == null)
                throw new Exception(ErrorMessages.EmailOrUsernameFalse);

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, true);

            if (!result.Succeeded)
                throw new Exception(ErrorMessages.PasswordIsFalse);

            Token token = _tokenHandler.CreateToken(user);

            await _userService.UpdateRefreshTokenAsync(user, token.RefreshToken, token.Expiration.AddMinutes(1));

            return new() { token = token, user = new() { FirstName = user.FirstName, LastName = user.LastName, UserName = user.UserName } };
        }

        public async Task PasswordResetAsync(string email)
        {
            AppUser? user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                //byte[] tokenBytes = Encoding.UTF8.GetBytes(resetToken);
                //resetToken = WebEncoders.Base64UrlEncode(tokenBytes);
                resetToken = resetToken.UrlEncode();

                await _mailService.SendPasswordResetMailAsync(email, user.Id, resetToken);
                return;
            }
            throw new Exception("Parola sıfırlanırken bilinmeyen bir hatayla karşılaşıldı.");
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = _userManager.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);

            if (user == null)
                throw new Exception(ErrorMessages.UserCanNotFoundByRefreshToken);

            if (user.RefreshTokenExpiration > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateToken(user);

                await _userService.UpdateRefreshTokenAsync(user, token.RefreshToken, token.Expiration.AddMinutes(1));

                return token;
            }
            throw new Exception(ErrorMessages.RefreshTokenExpired);
        }

        public async Task<bool> RegisterAsync(RegisterUser model)
        {
            AppUser? user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
                throw new Exception(ErrorMessages.DuplicateEmail);

            user = await _userManager.FindByNameAsync(model.Username);

            if (user != null)
                throw new Exception(ErrorMessages.DuplicateUserName);

            user = new()
            {
                Email = model.Email,
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                throw new Exception(ErrorMessages.UnknownErrorWhenUserRegister);

            string emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            await _mailService.SendEmailConfirmMailAsync(user.Email, user.Id, emailConfirmationToken);

            return result.Succeeded;
        }

        public async Task<bool> VerifyEmailConfirmTokenAsync(string userId, string emailConfirmToken)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new Exception(ErrorMessages.UserCanNotFoundById);

            bool isVerified = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.EmailConfirmationTokenProvider, "EmailConfirmation", emailConfirmToken);

            if (isVerified)
            {
                IdentityResult confirmMailResult = await _userManager.ConfirmEmailAsync(user, emailConfirmToken);
                if (confirmMailResult.Succeeded)
                    return true;
                throw new Exception(ErrorMessages.UnknownErrorWhenMailConfirm);
            }
            throw new Exception(ErrorMessages.InvalidEmailConfirmToken);
        }

        public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();

                bool result = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);
                return result;
            }
            return false;
        }
    }
}
