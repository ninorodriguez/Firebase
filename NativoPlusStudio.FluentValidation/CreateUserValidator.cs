using FluentValidation;
using NativoPlusStudio.DataTransferObjects.FirebaseCreateUser;

namespace NativoPlusStudio.FluentValidation
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.fullName).NotEmpty();            
            RuleFor(x => x.email).NotEmpty().EmailAddress();
            RuleFor(x => x.password).NotEmpty();
            RuleFor(x => x.provider).NotEmpty();
            RuleFor(x => x.isSubcribed).NotEmpty();
            RuleFor(x => x.weeksOfPregnancy).NotEmpty();
            RuleFor(x => x.hasEnabledNotifications).NotEmpty();
            RuleFor(x => x.journeyName).NotEmpty();
            RuleFor(x => x.appLanguage).NotEmpty();
            RuleFor(x => x.startingWeek).NotEmpty();
            RuleFor(x => x.createdDate).NotEmpty();
            RuleFor(x => x.pregnancyDateModified).NotEmpty();
            RuleFor(x => x.isExternalSubscriber).NotEmpty();
            RuleFor(x => x.isTrialSubscriber).NotEmpty();
            RuleFor(x => x.isTrialSubExpirationDate).NotEmpty();
            RuleFor(x => x.isExternalUser).NotEmpty();
        }
    }
}
