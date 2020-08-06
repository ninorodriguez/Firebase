using System.Threading.Tasks;

namespace NativoPlusStudio.Interfaces.FirebaseCreateUser
{
    public interface ICreateUsersService
    {  
        Task<ICreateUserResponse> AddUsers(ICreateUserRequest model);
    }
}