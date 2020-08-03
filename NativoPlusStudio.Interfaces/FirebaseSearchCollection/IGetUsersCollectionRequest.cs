namespace NativoPlusStudio.Interfaces.FirebaseSearchCollection
{
    public interface IGetUsersCollectionRequest
    {
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}