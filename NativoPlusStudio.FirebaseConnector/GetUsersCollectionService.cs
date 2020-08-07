using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Options;
using NativoPlusStudio.DataTransferObjects.Configurations;
using NativoPlusStudio.DataTransferObjects.FirebaseSearchCollection;
using NativoPlusStudio.Interfaces.FirebaseSearchCollection;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NativoPlusStudio.FirebaseConnector
{
    public class GetUsersCollectionService : IGetUsersCollectionService
    {
        private ILogger _logger;
        private readonly FirebaseOptions _config;
        string projectId;
        FirestoreDb fireStoreDb;

        public GetUsersCollectionService(ILogger logger,
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

        public async Task<List<IGetUsersCollectionResponse>> GetUsersInfo(IGetUsersCollectionRequest model)
        {  

            try
            {
                
                var query = GetQuery(model);
                QuerySnapshot querySnapshot = await query.Result.GetSnapshotAsync();
                List<IGetUsersCollectionResponse> lstUsers = new List<IGetUsersCollectionResponse>();
                
                foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Dictionary<string, object> users = documentSnapshot.ToDictionary();
                        var stringResponse = JsonConvert.SerializeObject(users);
                        var settings = new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore,
                            DateParseHandling = DateParseHandling.None,
                        };
                        var response = JsonConvert.DeserializeObject<GetUsersCollectionResponse>(stringResponse, settings);
                        lstUsers.Add(response);
                    }
                }
                
                return lstUsers;

            }
            catch(Exception ex)
            {
                _logger.Error($"#GetUserInfo: {ex.Message}");
                return new List<IGetUsersCollectionResponse>();
            }        
        }


        public async Task<Query> GetQuery(IGetUsersCollectionRequest model)
        {           
            var fieldPathSpecific = string.Empty;
            var valueSpecific = string.Empty;

            if (model.FirstName != null && model.LastName == null && model.Email == null)
            {                
                fieldPathSpecific = "firstName";
                valueSpecific = model.FirstName;
            }
            else if (model.FirstName == null && model.LastName != null && model.Email == null)
            {                
                fieldPathSpecific = "lastName";
                valueSpecific = model.LastName;
            }
            else if (model.FirstName == null && model.LastName == null && model.Email != null)
            {                
                fieldPathSpecific = "email";
                valueSpecific = model.Email;
            }          
                         
            CollectionReference userRef = fireStoreDb.Collection("users");
            Query query = userRef.WhereEqualTo(fieldPathSpecific, valueSpecific);
            
            return query;
        }
    }
}
