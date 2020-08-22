using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NativoPlusStudio.DataTransferObjects.FirebaseCreateUser;
using NativoPlusStudio.DataTransferObjects.FirebaseSearchCollection;
using NativoPlusStudio.DataTransferObjects.FirebaseUpdateUser;
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
        public async Task<IActionResult> UploadFile([FromForm] UploadFileRequest request) => await _mediator.Send(request);

        /// <summary>
        /// Create Users on Firebase Database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        [Route("CreateUserToCloudFireStore")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request) => await _mediator.Send(request);

        /// <summary>
        /// Get User Info by specific field FirtName, or LastName or Email
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Route("GetFirebaseUsersCollection")]
        public async Task<IActionResult> GetUserInfo([FromBody] GetUsersCollectionRequest request) => await _mediator.Send(request);

        /// <summary>
        /// Update User Information
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="userObject"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Route("UpdateUser/{documentId}", Name = "UpdateUser")]
        public async Task<IActionResult> UpdateUser(string documentId, [FromBody] UpdateUserRequest userObject) =>
            await _mediator.Send(request: new UpdateUserModel { DocumentId = documentId, UserData = userObject });  
        
    }
}

