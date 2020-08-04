using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NativoPlusStudio.DataTransferObjects.FirebaseCreateUser;
using NativoPlusStudio.DataTransferObjects.FirebaseSearchCollection;
using NativoPlusStudio.DataTransferObjects.FirebaseUploadFile;
using NativoPlusStudio.Interfaces.FirebaseCreateUser;
using NativoPlusStudio.Interfaces.FirebaseSearchCollection;
using NativoPlusStudio.Interfaces.FirebaseUploadFile;
using NativoPlusStudio.SharedConfiguration;
using System;
using System.IO;
using static NativoPlusStudio.Enums.Values;

namespace NativoPlusStudio.FirebaseUnitTest.Services
{
    [TestClass]
    public class ServicesTests
    {
        IServiceProvider serviceProvider;
        private readonly IUploadFileService _uploadFileService;
        private readonly ICreateUsersService _createUser;
        private readonly IGetUsersCollectionService _search;

        public ServicesTests()
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Production.json", optional: false, reloadOnChange: true)
            .Build();

            IServiceCollection collection = new ServiceCollection();
            collection.ConfigurationServices(configuration);
            serviceProvider = collection.BuildServiceProvider();
            _uploadFileService = serviceProvider.GetRequiredService<IUploadFileService>();
            _createUser = serviceProvider.GetRequiredService<ICreateUsersService>();
            _search = serviceProvider.GetRequiredService<IGetUsersCollectionService>();
        }

        [TestMethod]
        public void UploadImageFile()
        {
            using (var stream = File.OpenRead("C:\\Users\\nrodr\\Desktop\\GBLogo.png"))
            {
                var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/png"
                };

                var model = new UploadFileRequest()
                {
                    File =file,
                    Folder = FolderNames.GbProfileImages
                };

                var result = _uploadFileService.FileUpload(model);

                Assert.IsTrue(result.Result.Successful);
            }       
        }

        [TestMethod]
        public void CreateUserInfo()
        {
            var model = new CreateUserRequest()
            {
                fullName = "Ana Rodriguez",
                firstName = "Ana",
                lastName = "Rodriguez",
                email = "nsrd793@gmail.com",
                password = "password",
                provider = "email",
                isSubcribed = true,
                weeksOfPregnancy = 1,
                hasEnabledNotifications = true,
                journeyName = "Vaginal Birth",
                appLanguage = "English",
                startingWeek = 13,
                createdDate = DateTime.UtcNow,
                pregnancyDateModified = DateTime.UtcNow,
                isExternalSubscriber = true,
                isTrialSubscriber = true,
                isTrialSubExpirationDate = DateTime.UtcNow.AddDays(7),
                isExternalUser = true,
                isOnboarded = true
            };

            var result = _createUser.AddUsers(model);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Search()
        {
            var model = new GetUsersCollectionRequest()
            {
                FirstName = "Ninoshka"
            };

            var result = _search.GetUsersInfo(model);
            Assert.IsNotNull(result);
        }
    }
}
