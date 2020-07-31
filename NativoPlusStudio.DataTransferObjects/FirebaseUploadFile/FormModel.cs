using Microsoft.AspNetCore.Http;
using NativoPlusStudio.Interfaces.FirebaseUploadFile;

namespace NativoPlusStudio.DataTransferObjects.FirebaseUploadFile
{
    public class FormModel : IFormModel
    {
        public IFormFile File { get; set; }
    }
}
