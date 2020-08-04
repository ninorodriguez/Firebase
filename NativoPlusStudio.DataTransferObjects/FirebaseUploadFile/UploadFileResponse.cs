using NativoPlusStudio.Interfaces.FirebaseUploadFile;

namespace NativoPlusStudio.DataTransferObjects.FirebaseUploadFile
{
    public class UploadFileResponse : IUploadFileResponse
    {
        public bool Successful { get; set; }
        public string Url { get; set; }
    }
}
