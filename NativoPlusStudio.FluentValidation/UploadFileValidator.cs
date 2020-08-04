using FluentValidation;
using NativoPlusStudio.DataTransferObjects.FirebaseUploadFile;

namespace NativoPlusStudio.FluentValidation
{
    public class UploadFileValidator : AbstractValidator<UploadRequest>
    {
        public UploadFileValidator()
        {
            RuleFor(x => x.File).NotEmpty();
            RuleFor(x => x.Folder).NotEmpty();
        }
    }
}
