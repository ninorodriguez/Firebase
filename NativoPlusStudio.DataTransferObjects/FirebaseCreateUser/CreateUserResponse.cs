using NativoPlusStudio.Interfaces.FirebaseCreateUser;

namespace NativoPlusStudio.DataTransferObjects.FirebaseCreateUser
{
    public class CreateUserResponse : ICreateUserResponse
    {
        public string DbId { get; set; }
    }
}
