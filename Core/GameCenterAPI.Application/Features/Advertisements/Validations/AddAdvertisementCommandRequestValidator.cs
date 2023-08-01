using FluentValidation;
using GameCenterAPI.Application.Features.Advertisements.Commands;

namespace GameCenterAPI.Application.Features.Advertisements.Validations
{
    public class AddAdvertisementCommandRequestValidator : AbstractValidator<AddAdvertisementCommandRequest>
    {
        public AddAdvertisementCommandRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotNull().WithMessage(ValidationMessages.TitleNotNullMessage)
                .NotEmpty().WithMessage(ValidationMessages.TitleNotNullMessage)
                .MinimumLength(3).WithMessage(ValidationMessages.TitleCanMinimum3Letter)
                .MaximumLength(50).WithMessage(ValidationMessages.TitleCanMaximum100Letter);
            
            RuleFor(x => x.Description)
                .NotNull().WithMessage(ValidationMessages.DescriptionNotNullMessage)
                .NotEmpty().WithMessage(ValidationMessages.DescriptionNotNullMessage)
                .MinimumLength(3).WithMessage(ValidationMessages.DescriptionCanMinimum3Letter)
                .MaximumLength(50).WithMessage(ValidationMessages.DescriptionCanMaximum400Letter);

            RuleFor(x => x.GameId)
                .NotNull().WithMessage(ValidationMessages.GameIdCanNotBeNull)
                .NotEmpty().WithMessage(ValidationMessages.GameIdCanNotBeNull);

            RuleFor(x => x.UserId)
                .NotNull().WithMessage(ValidationMessages.UserIdCanNotBeNull)
                .NotEmpty().WithMessage(ValidationMessages.UserIdCanNotBeNull);

            RuleFor(x => x.Thumbnail)
                .NotNull().WithMessage(ValidationMessages.ThumbnailCanNotBeNull)
                .NotEmpty().WithMessage(ValidationMessages.ThumbnailCanNotBeNull);

            RuleFor(x => x.Price)
                .NotNull().WithMessage(ValidationMessages.PriceCanNotBeNull)
                .NotEmpty().WithMessage(ValidationMessages.PriceCanNotBeNull)
                .GreaterThan(0).WithMessage(ValidationMessages.PriceMustBeGreaterThan0);

            RuleFor(x => x.DeliveryHour)
                .NotNull().WithMessage(ValidationMessages.DeliveryHourCanNotBeNull)
                .NotEmpty().WithMessage(ValidationMessages.DeliveryHourCanNotBeNull)
                .GreaterThan(0).WithMessage(ValidationMessages.DeliveryHourMustBeGreaterThan0);
        }
    }
}
