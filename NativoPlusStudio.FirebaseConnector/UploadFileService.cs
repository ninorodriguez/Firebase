using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.Extensions.Options;
using NativoPlusStudio.DataTransferObjects.Configurations;
using NativoPlusStudio.DataTransferObjects.FirebaseUploadFile;
using NativoPlusStudio.Interfaces.FirebaseUploadFile;
using Serilog;

namespace NativoPlusStudio.FirebaseConnector
{
    public class UploadFileService : IUploadFileService
    {
        private ILogger _logger;
        private readonly FirebaseOptions _config;

        public UploadFileService(
            ILogger logger,
            IOptions<FirebaseOptions> firebaseOptions)
        {
            _logger = logger;
            _config = firebaseOptions.Value;

        }

        public async Task<string> EmailandPasswordToken()
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(_config.ApiKey));
            var token = await auth.SignInWithEmailAndPasswordAsync(_config.Email, _config.Password);

            return token.FirebaseToken;
        }        

        public async Task<IUploadResponse> FileUpload(IUploadRequest model)
        {
            _logger.Information($"#UploadFile started");          
            
            try
            {
                var fileUpload = model.File;               
                MemoryStream stream = null;                              
                if (fileUpload.Length > 0)
                {
                    //Create the directory for images or tracks 
                    if (fileUpload.ContentType == "audio/mpeg")
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            model.File.OpenReadStream().CopyTo(memoryStream);
                            var value = memoryStream.ToArray();
                            stream = new MemoryStream(value);
                        }                        
                                             
                    }
                    if (fileUpload.ContentType == "image/png" || fileUpload.ContentType == "image/jpeg")
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            model.File.OpenReadStream().CopyTo(memoryStream);
                            var value = memoryStream.ToArray();
                            stream = new MemoryStream(value);
                        }
                    }                   

                    //Firebase Email & Password Auth                   
                    var token = EmailandPasswordToken();

                    //cancelation token and push to firebase
                    var cancellation = new CancellationTokenSource();
                    var upload = new FirebaseStorage(
                        _config.Bucket,
                        new FirebaseStorageOptions
                        {
                            AuthTokenAsyncFactory = () => token,
                            ThrowOnCancel = true
                        })
                        .Child(model.Folder.ToString())
                        .Child($"{fileUpload.FileName}.{Path.GetExtension(fileUpload.FileName.Substring(1))}")
                        .PutAsync(stream, cancellation.Token);
                    stream = null;
                    
                    var url = await upload;
                    if (url != null)
                    {
                        return new UploadResponse()
                        {
                            Successful = true,
                            Url = url
                        };
                    }
                }

                return new UploadResponse();

            }
            catch (Exception ex)
            {
                _logger.Error($"#FileUpload error: {ex.Message}");
                return new UploadResponse();
            }
        }        
    }
}
