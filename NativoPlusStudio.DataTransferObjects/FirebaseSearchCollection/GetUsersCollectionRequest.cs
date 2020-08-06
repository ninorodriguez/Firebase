using NativoPlusStudio.Interfaces.FirebaseSearchCollection;
using NativoPlusStudio.RequestResponsePattern;

namespace NativoPlusStudio.DataTransferObjects.FirebaseSearchCollection
{
    public class GetUsersCollectionRequest : HttpRequest, IGetUsersCollectionRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
