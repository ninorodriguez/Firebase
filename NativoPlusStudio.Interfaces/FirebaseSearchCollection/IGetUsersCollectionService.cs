using System.Collections.Generic;
using System.Threading.Tasks;

namespace NativoPlusStudio.Interfaces.FirebaseSearchCollection
{
    public interface IGetUsersCollectionService
    {        
        Task<List<IGetUsersCollectionResponse>> GetUsersInfo(IGetUsersCollectionRequest model);
    }
}