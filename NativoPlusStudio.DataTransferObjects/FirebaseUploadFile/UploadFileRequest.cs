using NativoPlusStudio.RequestResponsePattern;
using NativoPlusStudio.Interfaces.FirebaseUploadFile;
using static NativoPlusStudio.Enums.Values;


namespace NativoPlusStudio.DataTransferObjects.FirebaseUploadFile
{
    public class UploadFileRequest : HttpRequest, IUploadFileRequest
    {
        public Microsoft.AspNetCore.Http.IFormFile File { get; set; }  
        public FolderNames Folder { get; set; }

    }
}
