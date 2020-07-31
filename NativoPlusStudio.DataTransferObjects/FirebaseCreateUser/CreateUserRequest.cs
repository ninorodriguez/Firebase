using Google.Cloud.Firestore;
using NativoPlusStudio.Interfaces.FirebaseCreateUser;
using NativoPlusStudio.RequestResponsePattern;
using System;

namespace NativoPlusStudio.DataTransferObjects.FirebaseCreateUser
{
    [FirestoreData]
    public class CreateUserRequest : HttpRequest, ICreateUserRequest
    {
        [FirestoreProperty]
        public string UserId { get; set; }
        [FirestoreProperty]
        public string FullName { get; set; }
        [FirestoreProperty]
        public string FirstName { get; set; }
        [FirestoreProperty]
        public string LastName { get; set; }
        [FirestoreProperty]
        public string Email { get; set; }
        public string Password { get; set; }
        [FirestoreProperty]
        public string Provider { get; set; }
        [FirestoreProperty]
        public bool IsSubcribed { get; set; }
        [FirestoreProperty]
        public int WeeksOfPregnancy { get; set; }
        [FirestoreProperty]
        public string HasEnabledNotifications { get; set; }
        [FirestoreProperty]
        public string JourneyName { get; set; }
        [FirestoreProperty]
        public string AppLanguage { get; set; }
        [FirestoreProperty]
        public string StartingWeek { get; set; }
        [FirestoreProperty]
        public DateTime? CreatedDate { get; set; } = DateTime.Now.Date;
        [FirestoreProperty]
        public DateTime? PregnancyDateModified { get; set; } = DateTime.Now.Date;
        [FirestoreProperty]
        public bool IsExternalSubscriber { get; set; }
        [FirestoreProperty]
        public bool IsTrialSubscriber { get; set; }
        [FirestoreProperty]
        public DateTime? IsTrialSubExpirationDate { get; set; } = DateTime.Now.AddDays(7);
        [FirestoreProperty]
        public bool IsExternalUser { get; set; }
        [FirestoreProperty]
        public bool IsOnboarded { get; set; }

    }

}
