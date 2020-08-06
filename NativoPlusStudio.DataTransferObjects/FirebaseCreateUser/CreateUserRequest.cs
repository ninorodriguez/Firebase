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
        public string userId { get; set; }       
        public string fullName { get; set; }
        [FirestoreProperty]
        public string displayName { get; set; }
        [FirestoreProperty]
        public string firstName { get; set; }
        [FirestoreProperty]
        public string lastName { get; set; }
        [FirestoreProperty]
        public string email { get; set; }
        public string password { get; set; }
        [FirestoreProperty]
        public string provider { get; set; }
        [FirestoreProperty]
        public bool isSubcribed { get; set; }
        [FirestoreProperty]
        public int weeksOfPregnancy { get; set; }
        [FirestoreProperty]
        public bool hasEnabledNotifications { get; set; }
        [FirestoreProperty]
        public string journeyName { get; set; }
        [FirestoreProperty]
        public string appLanguage { get; set; }
        [FirestoreProperty]
        public int startingWeek { get; set; }
        [FirestoreProperty]
        public DateTime? createdDate { get; set; } = DateTime.Now.Date;
        [FirestoreProperty]
        public DateTime? pregnancyDateModified { get; set; } = DateTime.Now.Date;
        [FirestoreProperty]
        public bool isExternalSubscriber { get; set; }
        [FirestoreProperty]
        public bool isTrialSubscriber { get; set; }
        [FirestoreProperty]
        public DateTime? isTrialSubExpirationDate { get; set; } = DateTime.Now.AddDays(7);
        [FirestoreProperty]
        public bool isExternalUser { get; set; }
        [FirestoreProperty]
        public bool isOnboarded { get; set; }

    }

}
