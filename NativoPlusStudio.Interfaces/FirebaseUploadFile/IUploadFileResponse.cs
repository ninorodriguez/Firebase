namespace NativoPlusStudio.Interfaces.FirebaseUploadFile
{
    public interface IUploadFileResponse
    {
        bool Successful { get; set; }
        string Url { get; set; }
    }
}