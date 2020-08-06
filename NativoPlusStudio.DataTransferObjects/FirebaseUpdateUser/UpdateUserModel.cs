using NativoPlusStudio.Interfaces.FirebaseUpdateUser;
using NativoPlusStudio.RequestResponsePattern;


namespace NativoPlusStudio.DataTransferObjects.FirebaseUpdateUser
{
    public class UpdateUserModel : HttpRequest, IUpdateUserModel
    {
        public string DocumentId { get; set; }
        public IUpdateUserRequest UserData { get; set; }
    }
}
