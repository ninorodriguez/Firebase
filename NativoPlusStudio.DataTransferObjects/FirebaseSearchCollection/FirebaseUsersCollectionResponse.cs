using Google.Cloud.Firestore;
using NativoPlusStudio.Interfaces.FirebaseSearchCollection;

namespace NativoPlusStudio.DataTransferObjects.FirebaseSearchCollection
{
    public class FirebaseUsersCollectionResponse : IFirebaseUsersCollectionResponse
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Provider { get; set; }
        public bool IsSubcribed { get; set; }
        public int WeeksOfPregnancy { get; set; }
        public string HasEnabledNotifications { get; set; }
        public string JourneyName { get; set; }
        public string AppLanguage { get; set; }
        public string StartingWeek { get; set; }
        public Timestamp CreatedDate { get; set; } 
        public Timestamp PregnancyDateModified { get; set; }
        public bool IsExternalSubscriber { get; set; }
        public bool IsTrialSubscriber { get; set; }
        public Timestamp IsTrialSubExpirationDate { get; set; }
        public bool IsExternalUser { get; set; }
        public bool IsOnboarded { get; set; }


    }
}
