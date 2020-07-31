using Google.Cloud.Firestore;

namespace NativoPlusStudio.Interfaces.FirebaseSearchCollection
{
    public interface IFirebaseUsersCollectionResponse
    {
        string AppLanguage { get; set; }
        Timestamp CreatedDate { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string FullName { get; set; }
        string HasEnabledNotifications { get; set; }
        bool IsExternalSubscriber { get; set; }
        bool IsExternalUser { get; set; }
        bool IsOnboarded { get; set; }
        bool IsSubcribed { get; set; }
        Timestamp IsTrialSubExpirationDate { get; set; }
        bool IsTrialSubscriber { get; set; }
        string JourneyName { get; set; }
        string LastName { get; set; }
        string Password { get; set; }
        Timestamp PregnancyDateModified { get; set; }
        string Provider { get; set; }
        string StartingWeek { get; set; }
        string UserId { get; set; }
        int WeeksOfPregnancy { get; set; }
    }
}