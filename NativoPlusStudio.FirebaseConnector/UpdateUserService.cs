using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Options;
using NativoPlusStudio.DataTransferObjects.Configurations;
using NativoPlusStudio.DataTransferObjects.FirebaseUpdateUser;
using NativoPlusStudio.Interfaces.FirebaseUpdateUser;
using Serilog;
using System;
using System.Threading.Tasks;

namespace NativoPlusStudio.FirebaseConnector
{
    public class UpdateUserService : IUpdateUserService
    {
        private ILogger _logger;
        private readonly FirebaseOptions _config;
        string projectId;
        FirestoreDb fireStoreDb;

        public UpdateUserService(ILogger logger,
            IOptions<FirebaseOptions> firebaseOptions)
        {
            _logger = logger;
            _config = firebaseOptions.Value;

            projectId = _config.ProjectId;
            fireStoreDb = FirestoreDb.Create(projectId);
        }

        public async void FirebaseAuth()
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = await GoogleCredential.GetApplicationDefaultAsync()
            });

        }

        public async Task<IUpdateUserResponse> UpdateUser(IUpdateUserModel model)
        {
            try
            {
                DocumentReference reference = fireStoreDb.Collection("users").Document(model.DocumentId);
                var response = await reference.SetAsync(model.UserData, SetOptions.MergeAll);
                if(response != null)
                {
                    return new UpdateUserResponse()
                    { 
                        Succesfuly = true
                    };
                }
                return new UpdateUserResponse();
            }
            catch(Exception ex)
            {
                _logger.Error($"#UpdateUser: {ex.Message}");
                return new UpdateUserResponse();
            }
        }

    }
}
