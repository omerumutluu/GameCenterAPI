using FluentValidation.Results;
using GameCenterAPI.Application.Exceptions;
using GameCenterAPI.Application.Features.Auth.Validations;
using GameCenterAPI.Application.Repositories.Users;
using GameCenterAPI.Domain.Entities;
using GameCenterAPI.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GameCenterAPI.Application.Features.Auth.Commands
{
    public class RegisterCommandRequest : IRequest<RegisterCommandResponse>
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }

    public class RegisterCommandResponse
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class RegisterCommandRequestHandler : IRequestHandler<RegisterCommandRequest, RegisterCommandResponse>
    {
        readonly UserManager<AppUser> _userManager;

        public RegisterCommandRequestHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<RegisterCommandResponse> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            AppUser? user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null)
                throw new Exception(ErrorMessages.DuplicateEmail);

            user = await _userManager.FindByNameAsync(request.Username);
            if (user != null)
                throw new Exception(ErrorMessages.DuplicateUserName);


            RegisterCommandRequestValidator validator = new();
            ValidationResult validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errors = new List<ValidationExceptionInnerException>();

                foreach (var error in validationResult.Errors)
                    errors.Add(new() { FailedProperty = error.PropertyName, Message = error.ErrorMessage });

                throw new ValidationException(ErrorMessages.ValidationFailed, errors);
            }

            user = new()
            {
                Email = request.Email,
                UserName = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                IsEmailConfirmed = false,
                IsPhoneNumberConfirmed = false
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                throw new Exception(ErrorMessages.UnknownErrorWhenUserRegister);


            RegisterCommandResponse response = new()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Username = user.PhoneNumber,
                CreatedDate = user.CreatedAt
            };

            return response;
        }
    }
}
