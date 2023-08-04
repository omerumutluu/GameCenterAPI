using FluentValidation.Results;
using GameCenterAPI.Application.Abstraction.Services;
using GameCenterAPI.Application.Exceptions;
using GameCenterAPI.Application.Features.Auth.Validations;
using GameCenterAPI.Domain.Entities.Identity;
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
        public bool IsSucceeded { get; set; }
    }

    public class RegisterCommandRequestHandler : IRequestHandler<RegisterCommandRequest, RegisterCommandResponse>
    {
        readonly IAuthService _authService;

        public RegisterCommandRequestHandler(IAuthService authService)
        {
            _authService = authService;
        }


        public async Task<RegisterCommandResponse> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            RegisterCommandRequestValidator validator = new();
            ValidationResult validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errors = new List<ValidationExceptionInnerException>();

                foreach (var error in validationResult.Errors)
                    errors.Add(new() { FailedProperty = error.PropertyName, Message = error.ErrorMessage });

                throw new ValidationException(ErrorMessages.ValidationFailed, errors);
            }

            bool result = await _authService.RegisterAsync(new()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber,
                Username = request.Username
            });

            if (!result)
                throw new Exception(ErrorMessages.UnknownErrorWhenUserRegister);

            return new() { IsSucceeded = result };
        }
    }
}
