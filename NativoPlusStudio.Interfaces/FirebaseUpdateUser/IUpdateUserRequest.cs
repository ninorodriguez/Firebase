using System;

namespace NativoPlusStudio.Interfaces.FirebaseUpdateUser
{
    public interface IUpdateUserRequest
    {
        string appLanguage { get; set; }
        string displayName { get; set; }
        string email { get; set; }
        string firstName { get; set; }
        bool hasEnabledNotifications { get; set; }
        bool isExternalSubscriber { get; set; }
        bool isExternalUser { get; set; }
        bool isOnboarded { get; set; }
        bool isSubcribed { get; set; }
        bool isTrialSubscriber { get; set; }
        string journeyName { get; set; }
        string lastName { get; set; }
        string provider { get; set; }
        int startingWeek { get; set; }
        string userId { get; set; }
        int weeksOfPregnancy { get; set; }
       
        











    }
}