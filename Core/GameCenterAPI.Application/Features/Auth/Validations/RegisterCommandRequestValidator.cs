using FluentValidation;
using GameCenterAPI.Application.Features.Auth.Commands;

namespace GameCenterAPI.Application.Features.Auth.Validations
{
    public class RegisterCommandRequestValidator : AbstractValidator<RegisterCommandRequest>
    {
        public RegisterCommandRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotNull().WithMessage(ValidationMessages.UsernameCanNotBeNull)
                .NotEmpty().WithMessage(ValidationMessages.UsernameCanNotBeNull)
                .MinimumLength(4).WithMessage(ValidationMessages.UsernameCanBeMinimum4Letter)
                .MaximumLength(20).WithMessage(ValidationMessages.UsernameCanBeMaximum20Letter);

            RuleFor(x => x.FirstName)
                .NotNull().WithMessage(ValidationMessages.FirstNameCanNotBeNull)
                .NotEmpty().WithMessage(ValidationMessages.FirstNameCanNotBeNull)
                .MinimumLength(3).WithMessage(ValidationMessages.FirstNameCanBeMinimum3Letter)
                .MaximumLength(20).WithMessage(ValidationMessages.FirstNameCanBeMaximum20Letter);

            RuleFor(x => x.LastName)
                .NotNull().WithMessage(ValidationMessages.LastNameCanNotBeNull)
                .NotEmpty().WithMessage(ValidationMessages.LastNameCanNotBeNull)
                .MinimumLength(3).WithMessage(ValidationMessages.LastNameCanBeMinimum3Letter)
                .MaximumLength(20).WithMessage(ValidationMessages.LastNameCanBeMaximum20Letter);

            RuleFor(x => x.PhoneNumber)
                .NotNull().WithMessage(ValidationMessages.PhoneNumberCanNotBeNull)
                .NotEmpty().WithMessage(ValidationMessages.PhoneNumberCanNotBeNull)
                .Length(11).WithMessage(ValidationMessages.PhoneNumberMustBe11Digit);

            RuleFor(x => x.Email)
                .NotNull().WithMessage(ValidationMessages.EmailCanNotBeNull)
                .NotEmpty().WithMessage(ValidationMessages.EmailCanNotBeNull)
                .EmailAddress().WithMessage(ValidationMessages.EmailMustBeValidEmailFormat);

            RuleFor(x => x.Password)
                .NotNull().WithMessage(ValidationMessages.PasswordCanNotBeNull)
                .NotEmpty().WithMessage(ValidationMessages.PasswordCanNotBeNull)
                .MinimumLength(8).WithMessage(ValidationMessages.PasswordCanBeMinimum8Letter)
                .MaximumLength(20).WithMessage(ValidationMessages.PasswordCanBeMaximum20Letter)
                .Must(PasswordMustContainsCapitalLetter).WithMessage(ValidationMessages.PasswordMustContainsCapitalLetter)
                .Must(PasswordMustContainsLowerLetter).WithMessage(ValidationMessages.PasswordMustContainsLowerLetter)
                .Must(PasswordMustContainsDigit).WithMessage(ValidationMessages.PasswordMustContainsDigit);

        }

        private bool PasswordMustContainsCapitalLetter(string password)
            => password.Any(x => char.IsUpper(x));

        private bool PasswordMustContainsLowerLetter(string password)
            => password.Any(x => char.IsLower(x));

        private bool PasswordMustContainsDigit(string password)
            => password.Any(x => char.IsDigit(x));
    }
}
