using System;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Options;
using NativoPlusStudio.DataTransferObjects.Configurations;
using NativoPlusStudio.DataTransferObjects.FirebaseCreateUser;
using NativoPlusStudio.Interfaces.FirebaseCreateUser;
using Serilog;

namespace NativoPlusStudio.FirebaseConnector
{
    public class CreateUsersService : ICreateUsersService
    {
        private ILogger _logger;        
        private readonly FirebaseOptions _config;
        string projectId;
        FirestoreDb fireStoreDb;

        public CreateUsersService(ILogger logger,            
            IOptions<FirebaseOptions> firebaseOptions)
        {
            _logger = logger;            
            _config = firebaseOptions.Value;
            
            projectId = _config.ProjectId;
            fireStoreDb = FirestoreDb.Create(projectId);           
        }

        public async Task<string> SingUp(ICreateUserRequest model)
        {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.GetApplicationDefault()
                });
          

            UserRecord newUser;
            try 
            {
                UserRecordArgs args = new UserRecordArgs()
                {
                    Email = model.Email,
                    Password = model.Password,
                    DisplayName = model.FullName,
                    EmailVerified = true
                };
                newUser = await FirebaseAuth.DefaultInstance.CreateUserAsync(args);
                return newUser.Uid;

            }
            catch (Exception ex)
            {
                _logger.Error($"#SingUp: {ex.Message}");
               return null;
            }                       

        }
        public async Task<ICreateUserResponse> AddUsers(ICreateUserRequest model)
        {
            var userAuth = SingUp(model);
            if (userAuth.Result == null)
            {
                return new CreateUserResponse();
            }            

            try
            {

                model.UserId = userAuth.Result;
                CollectionReference colRef = fireStoreDb.Collection("users");                
                var value = await colRef.AddAsync(model);                
                FirebaseApp.DefaultInstance.Delete();
                if (value != null)
                {
                    return new CreateUserResponse()
                    {
                        DbId = value.Id
                    };
                }
                return new CreateUserResponse();
            }
            catch (Exception ex)
            {
                _logger.Error($"#AddUser: {ex.Message}");
                return new CreateUserResponse();
            }            
        }        
    }
}
