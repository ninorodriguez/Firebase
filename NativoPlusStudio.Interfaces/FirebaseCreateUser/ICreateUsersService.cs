using System.Threading.Tasks;

namespace NativoPlusStudio.Interfaces.FirebaseCreateUser
{
    public interface ICreateUsersService
    {
        //Task<ICreateUserResponse> AddUser(IUserCreateRequest model);
        //Task<string> FirebaseAuthUid(IUserCreateRequest model);
        Task<ICreateUserResponse> AddUsers(ICreateUserRequest model);
    }
}