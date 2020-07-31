using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NativoPlusStudio.DataTransferObjects.FirebaseCreateUser;
using NativoPlusStudio.DataTransferObjects.FirebaseSearchCollection;
using NativoPlusStudio.DataTransferObjects.FirebaseUploadFile;

namespace NativoPlusStudio.FirebaseApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FirebaseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FirebaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Upload any file to Firebase Storage
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Route("UploadFileToStorage")]
        public async Task<IActionResult> UploadFile([FromForm] UploadRequest request) => await _mediator.Send(request);

        /// <summary>
        /// Create Users on Firebase Database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Route("CreateUserToCloudFireStore")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request) => await _mediator.Send(request);

        /// <summary>
        /// Create Users on Firebase Database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Route("SearchInFirebaseUsersCollection")]
        public async Task<IActionResult> SearchUsers([FromBody] SearchUsersCollectionRequest request) => await _mediator.Send(request);
    }
}

