using System.Threading.Tasks;

namespace NativoPlusStudio.Interfaces.FirebaseUpdateUser
{
    public interface IUpdateUserService
    {
        void FirebaseAuth();
        Task<IUpdateUserResponse> UpdateUser(IUpdateUserModel model);
    }
}