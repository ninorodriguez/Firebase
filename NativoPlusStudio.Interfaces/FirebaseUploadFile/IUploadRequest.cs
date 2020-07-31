using System.IO;
using static NativoPlusStudio.Enums.Values;

namespace NativoPlusStudio.Interfaces.FirebaseUploadFile
{
    public interface IUploadRequest
    {
        Microsoft.AspNetCore.Http.IFormFile File { get; set; }
        FolderNames Folder { get; set; }
    }
}