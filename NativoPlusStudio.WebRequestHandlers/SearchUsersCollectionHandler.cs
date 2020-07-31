using NativoPlusStudio.DataTransferObjects.FirebaseSearchCollection;
using NativoPlusStudio.Interfaces.FirebaseSearchCollection;
using NativoPlusStudio.RequestResponsePattern;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace NativoPlusStudio.WebRequestHandlers
{
    public class SearchUsersCollectionHandler : HttpHandler<SearchUsersCollectionRequest>
    {
        private readonly ISearchCollectionsService _searchUser;

        public SearchUsersCollectionHandler(ISearchCollectionsService searchUser, ILogger logger)
         : base(logger)
        {
            _searchUser = searchUser;
        }

        protected override async Task<HttpResponse> HandleAsync(
           SearchUsersCollectionRequest input,
           CancellationToken cancellationToken = default)
        {
            _logger.Information(nameof(HandleAsync));

            if (input == null)
            {
                _logger.Error($"#Search User request is null");
                var error = NullBadRequest<SearchUsersCollectionRequest>(transactionId: input.TransactionId.IsNullOrEmptyOrWhiteSpace() ? Guid.NewGuid().ToString() : input.TransactionId);
                return error;
            }

            var response = await _searchUser.GetUsersInfo(input);
            var transactionId = input.TransactionId.IsNullOrEmptyOrWhiteSpace() ? Guid.NewGuid().ToString() : input.TransactionId;
            if (response == null)
            {
                var errors = new List<Error>();
                errors.Add(new Error
                {
                    Message = "An error occurred while processing your request",
                    Code = ((int)HttpStatusCode.InternalServerError).ToString()
                });
                return BadRequest(new HttpStandardResponse<FirebaseUsersCollectionResponse>
                {
                    Response = null,
                    Error = errors,
                    Status = false,
                    TransactionId = transactionId
                }, transactionId);
            }
            return Ok(response: response, input.TransactionId.IsNullOrEmptyOrWhiteSpace() ? Guid.NewGuid().ToString() : input.TransactionId);

        }
    }
}
