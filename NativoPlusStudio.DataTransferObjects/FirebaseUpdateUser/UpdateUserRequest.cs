using Google.Cloud.Firestore;
using NativoPlusStudio.Interfaces.FirebaseUpdateUser;

namespace NativoPlusStudio.DataTransferObjects.FirebaseUpdateUser
{
    [FirestoreData]
    public class UpdateUserRequest : IUpdateUserRequest
    {
        [FirestoreProperty]
        public string appLanguage { get; set; }
        [FirestoreProperty]
        public string displayName { get; set; }
        [FirestoreProperty]
        public string email { get; set; }
        [FirestoreProperty]
        public string firstName { get; set; }
        [FirestoreProperty]
        public bool hasEnabledNotifications { get; set; }
        [FirestoreProperty]
        public bool isExternalSubscriber { get; set; }
        [FirestoreProperty]
        public bool isExternalUser { get; set; }
        [FirestoreProperty]
        public bool isOnboarded { get; set; }
        [FirestoreProperty]
        public bool isSubcribed { get; set; }
        [FirestoreProperty]
        public bool isTrialSubscriber { get; set; }
        [FirestoreProperty]
        public string journeyName { get; set; }
        [FirestoreProperty]
        public string lastName { get; set; }
        [FirestoreProperty]
        public string provider { get; set; }
        [FirestoreProperty]
        public int startingWeek { get; set; }
        [FirestoreProperty]
        public string userId { get; set; }      
        [FirestoreProperty]
        public int weeksOfPregnancy { get; set; }   
    }
}
