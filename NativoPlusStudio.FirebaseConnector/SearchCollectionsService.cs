using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Options;
using Microsoft.Win32.SafeHandles;
using NativoPlusStudio.DataTransferObjects.Configurations;
using NativoPlusStudio.DataTransferObjects.FirebaseCreateUser;
using NativoPlusStudio.DataTransferObjects.FirebaseSearchCollection;
using NativoPlusStudio.Interfaces.FirebaseCreateUser;
using NativoPlusStudio.Interfaces.FirebaseSearchCollection;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NativoPlusStudio.FirebaseConnector
{
    public class SearchCollectionsService : ISearchCollectionsService
    {
        private ILogger _logger;
        private readonly FirebaseOptions _config;
        string projectId;
        FirestoreDb fireStoreDb;

        public SearchCollectionsService(ILogger logger,
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
                Credential = GoogleCredential.GetApplicationDefault()
            });
        }      

        public async Task<List<IFirebaseUsersCollectionResponse>> GetUsersInfo(ISearchUsersCollectionRequest model)
        {            
            var fieldPathSpecific = string.Empty;           
            var valueSpecific = string.Empty;


            // Google Firebase is very limited in their Queries. You can not make an OR Queries

            if (model.FirstName != null && model.LastName == null && model.Email == null)
            {                
                fieldPathSpecific = "FirstName";
                valueSpecific = model.FirstName;
            }
            else if(model.FirstName == null && model.LastName != null && model.Email == null)
            {                
                fieldPathSpecific = "LastName";
                valueSpecific = model.LastName;
            }
           else if(model.FirstName == null && model.LastName == null && model.Email != null)
            {                
                fieldPathSpecific = "Email";
                valueSpecific = model.Email;
            }

            try
            {               
                CollectionReference userRef = fireStoreDb.Collection("users");         
                Query query = userRef.WhereEqualTo(fieldPathSpecific, valueSpecific);                                           
                QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
                List<IFirebaseUsersCollectionResponse> lstUsers = new List<IFirebaseUsersCollectionResponse>();
                
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
                        var response = JsonConvert.DeserializeObject<FirebaseUsersCollectionResponse>(stringResponse, settings);
                        lstUsers.Add(response);
                    }
                }
                
                return lstUsers;

            }
            catch(Exception ex)
            {
                _logger.Error($"#GetUserInfo: {ex.Message}");
                return new List<IFirebaseUsersCollectionResponse>();
            }        
        }
    }
}
