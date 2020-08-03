using System;

namespace NativoPlusStudio.Interfaces.FirebaseCreateUser
{
    public interface ICreateUserRequest
    {
        string appLanguage { get; set; }
        DateTime? createdDate { get; set; }
        string email { get; set; }
        string password { get; set; }
        string displayName { get; set; }
        string firstName { get; set; }
        string fullName { get; set; }
        bool hasEnabledNotifications { get; set; }
        bool isExternalSubscriber { get; set; }
        bool isExternalUser { get; set; }
        bool isOnboarded { get; set; }
        bool isSubcribed { get; set; }
        DateTime? isTrialSubExpirationDate { get; set; }
        bool isTrialSubscriber { get; set; }
        string journeyName { get; set; }
        string lastName { get; set; }
        DateTime? pregnancyDateModified { get; set; }
        string provider { get; set; }
        int startingWeek { get; set; }
        string userId { get; set; }
        int weeksOfPregnancy { get; set; }
    }
}