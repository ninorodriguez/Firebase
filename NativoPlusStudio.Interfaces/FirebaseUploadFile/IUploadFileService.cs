using System.Threading.Tasks;

namespace NativoPlusStudio.Interfaces.FirebaseUploadFile
{
    public interface IUploadFileService
    {
        Task<IUploadFileResponse> FileUpload(IUploadFileRequest model);
    }
}