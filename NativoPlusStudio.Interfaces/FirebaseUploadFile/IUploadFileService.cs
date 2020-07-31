using System.Threading.Tasks;

namespace NativoPlusStudio.Interfaces.FirebaseUploadFile
{
    public interface IUploadFileService
    {
        Task<IUploadResponse> FileUpload(IUploadRequest model);
    }
}