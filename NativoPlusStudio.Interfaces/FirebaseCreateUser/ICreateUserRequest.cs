using System;

namespace NativoPlusStudio.Interfaces.FirebaseCreateUser
{
    public interface ICreateUserRequest
    {
        string AppLanguage { get; set; }
        DateTime? CreatedDate { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string FirstName { get; set; }
        string FullName { get; set; }
        string HasEnabledNotifications { get; set; }
        bool IsExternalSubscriber { get; set; }
        bool IsExternalUser { get; set; }
        bool IsOnboarded { get; set; }
        bool IsSubcribed { get; set; }
        DateTime? IsTrialSubExpirationDate { get; set; }
        bool IsTrialSubscriber { get; set; }
        string JourneyName { get; set; }
        string LastName { get; set; }
        DateTime? PregnancyDateModified { get; set; }
        string Provider { get; set; }
        string StartingWeek { get; set; }
        string UserId { get; set; }
        int WeeksOfPregnancy { get; set; }
    }
}