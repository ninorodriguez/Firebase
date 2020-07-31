using NativoPlusStudio.Interfaces.FirebaseSearchCollection;
using NativoPlusStudio.RequestResponsePattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace NativoPlusStudio.DataTransferObjects.FirebaseSearchCollection
{
    public class SearchUsersCollectionRequest : HttpRequest, ISearchUsersCollectionRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
