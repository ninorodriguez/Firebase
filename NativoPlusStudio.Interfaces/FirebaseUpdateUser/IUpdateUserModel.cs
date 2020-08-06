
namespace NativoPlusStudio.Interfaces.FirebaseUpdateUser
{
    public interface IUpdateUserModel
    {
        string DocumentId { get; set; }
        IUpdateUserRequest UserData { get; set; }
    }
}