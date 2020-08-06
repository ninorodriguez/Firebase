using FluentValidation.Results;
using NativoPlusStudio.DataTransferObjects.FirebaseCreateUser;
using NativoPlusStudio.FluentValidation;
using NativoPlusStudio.Interfaces.FirebaseCreateUser;
using NativoPlusStudio.RequestResponsePattern;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace NativoPlusStudio.WebRequestHandlers
{
    public class CreateUserHandler : HttpHandler<CreateUserRequest>
    {
        private readonly ICreateUsersService _createUser;

        public CreateUserHandler(ICreateUsersService createUser, ILogger logger)
          : base(logger)
        {
            _createUser = createUser;
        }

        protected override async Task<HttpResponse> HandleAsync(
           CreateUserRequest input,
           CancellationToken cancellationToken = default)
        {
            _logger.Information(nameof(HandleAsync));

            var transactionId = input.TransactionId.IsNullOrEmptyOrWhiteSpace() ? Guid.NewGuid().ToString() : input.TransactionId;
            if (input == null)
            {
                _logger.Error($"#Create Firebase User request is null");
                var error = NullBadRequest<CreateUserRequest>(transactionId: input.TransactionId.IsNullOrEmptyOrWhiteSpace() ? Guid.NewGuid().ToString() : input.TransactionId);
                return error;
            }

            var validation = Validate(input);

            if(validation.IsValid)
            {
                var response = await _createUser.AddUsers(input);

                if (response.DbId == null)
                {
                    var errors = new List<Error>();
                    errors.Add(new Error
                    {
                        Message = "An error occurred while processing your request",
                        Code = ((int)HttpStatusCode.InternalServerError).ToString()
                    });
                    return BadRequest(new HttpStandardResponse<CreateUserResponse>
                    {
                        Response = null,
                        Error = errors,
                        Status = false,
                        TransactionId = transactionId
                    }, transactionId);
                }
                return Ok(response: (CreateUserResponse)response, input.TransactionId.IsNullOrEmptyOrWhiteSpace() ? Guid.NewGuid().ToString() : input.TransactionId);
            }
            return BadRequest<CreateUserRequest>(validation, transactionId);
        }

        private ValidationResult Validate(CreateUserRequest command)
        {
            _logger.Information("#Validate");

            var validator = new CreateUserValidator();
            ValidationResult result = validator.Validate(command);
            return result;
        }

    }
}
