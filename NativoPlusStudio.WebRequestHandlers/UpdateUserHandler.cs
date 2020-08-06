using NativoPlusStudio.DataTransferObjects.FirebaseUpdateUser;
using NativoPlusStudio.Interfaces.FirebaseUpdateUser;
using NativoPlusStudio.RequestResponsePattern;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace NativoPlusStudio.WebRequestHandlers
{
    public class UpdateUserHandler : HttpHandler<UpdateUserModel>
    {
        private readonly IUpdateUserService _updateUser;

        public UpdateUserHandler(IUpdateUserService updateUser, ILogger logger)
            : base(logger)
        {
            _updateUser = updateUser;
        }

        protected override async Task<HttpResponse> HandleAsync(
           UpdateUserModel input,
           CancellationToken cancellationToken = default)
        {
            _logger.Information(nameof(HandleAsync));

            var transactionId = input.TransactionId.IsNullOrEmptyOrWhiteSpace() ? Guid.NewGuid().ToString() : input.TransactionId;
            if (input == null)
            {
                _logger.Error($"#Update Firebase User request is null");
                var error = NullBadRequest<UpdateUserModel>(transactionId: input.TransactionId.IsNullOrEmptyOrWhiteSpace() ? Guid.NewGuid().ToString() : input.TransactionId);
                return error;
            }

            var response = await _updateUser.UpdateUser(input);

            if (response.Succesfuly == false)
            {
                var errors = new List<Error>();
                errors.Add(new Error
                {
                    Message = "An error occurred while processing your request",
                    Code = ((int)HttpStatusCode.InternalServerError).ToString()
                });
                return BadRequest(new HttpStandardResponse<UpdateUserResponse>
                {
                    Response = null,
                    Error = errors,
                    Status = false,
                    TransactionId = transactionId
                }, transactionId);
            }
            return Ok(response: (UpdateUserResponse)response, input.TransactionId.IsNullOrEmptyOrWhiteSpace() ? Guid.NewGuid().ToString() : input.TransactionId);

        }

    }
}
