namespace NativoPlusStudio.Interfaces.FirebaseSearchCollection
{
    public interface ISearchUsersCollectionRequest
    {
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}