using System.Collections.Generic;
using System.Threading.Tasks;

namespace NativoPlusStudio.Interfaces.FirebaseSearchCollection
{
    public interface ISearchCollectionsService
    {        
        Task<List<IFirebaseUsersCollectionResponse>> GetUsersInfo(ISearchUsersCollectionRequest model);
    }
}