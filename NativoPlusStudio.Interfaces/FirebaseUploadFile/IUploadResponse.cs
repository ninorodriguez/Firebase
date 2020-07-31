namespace NativoPlusStudio.Interfaces.FirebaseUploadFile
{
    public interface IUploadResponse
    {
        bool Successful { get; set; }
        string Url { get; set; }
    }
}