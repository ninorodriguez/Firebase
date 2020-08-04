using FluentValidation;
using NativoPlusStudio.DataTransferObjects.FirebaseUploadFile;

namespace NativoPlusStudio.FluentValidation
{
    public class UploadFileValidator : AbstractValidator<UploadFileRequest>
    {
        public UploadFileValidator()
        {
            RuleFor(x => x.File).NotEmpty();
            RuleFor(x => x.Folder).NotEmpty();
        }
    }
}
