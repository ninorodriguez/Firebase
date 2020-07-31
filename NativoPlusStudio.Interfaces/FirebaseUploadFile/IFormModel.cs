using Microsoft.AspNetCore.Http;

namespace NativoPlusStudio.Interfaces.FirebaseUploadFile
{
    public interface IFormModel
    {
        IFormFile File { get; set; }
    }
}