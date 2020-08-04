using FluentValidation.Results;
using NativoPlusStudio.DataTransferObjects.FirebaseUploadFile;
using NativoPlusStudio.FluentValidation;
using NativoPlusStudio.Interfaces.FirebaseUploadFile;
using NativoPlusStudio.RequestResponsePattern;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace NativoPlusStudio.WebRequestHandlers
{
    public class UploadFileHandler : HttpHandler<UploadRequest>
    {
        private readonly IUploadFileService _uploadFileService;

        public UploadFileHandler(IUploadFileService uploadFileService, ILogger logger)
          : base(logger)
        {
            _uploadFileService = uploadFileService;
        }

        protected override async Task<HttpResponse> HandleAsync(
           UploadRequest input,
           CancellationToken cancellationToken = default)
        {
            _logger.Information(nameof(HandleAsync));
            var transactionId = input.TransactionId.IsNullOrEmptyOrWhiteSpace() ? Guid.NewGuid().ToString() : input.TransactionId;
            if (input == null)
            {
                _logger.Error("#UploadFile The request is null");
                var error = NullBadRequest<UploadRequest>(transactionId: input.TransactionId.IsNullOrEmptyOrWhiteSpace() ? Guid.NewGuid().ToString() : input.TransactionId);
                return error;
            }

            var validation = Validate(input);

            if(validation.IsValid)
            {
                var response = await _uploadFileService.FileUpload(input);
                if (response == null)
                {
                    var errors = new List<Error>();
                    errors.Add(new Error
                    {
                        Message = "An error occurred while processing your request.",
                        Code = ((int)HttpStatusCode.InternalServerError).ToString()

                    });
                    return BadRequest(new HttpStandardResponse<UploadResponse>
                    {
                        Response = null,
                        Error = errors,
                        Status = false,
                        TransactionId = transactionId
                    }, transactionId);
                }
                return Ok(response: (UploadResponse)response, input.TransactionId.IsNullOrEmptyOrWhiteSpace() ? Guid.NewGuid().ToString() : input.TransactionId);
            }
            return BadRequest<UploadRequest>(validation, transactionId);
        }
        private ValidationResult Validate(UploadRequest command)
        {
            _logger.Information("#Validate");

            var validator = new UploadFileValidator();
            ValidationResult result = validator.Validate(command);
            return result;
        }

    }
}
