using NativoPlusStudio.Interfaces.FirebaseUploadFile;

namespace NativoPlusStudio.DataTransferObjects.FirebaseUploadFile
{
    public class UploadResponse : IUploadResponse
    {
        public bool Successful { get; set; }
        public string Url { get; set; }
    }
}
