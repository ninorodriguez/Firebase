using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Serilog;
using System;
using System.IO;
using NativoPlusStudio.WebRequestHandlers;
using NativoPlusStudio.Interfaces.FirebaseUploadFile;
using NativoPlusStudio.Interfaces.FirebaseCreateUser;
using NativoPlusStudio.DataTransferObjects.Configurations;
using NativoPlusStudio.FirebaseConnector;
using NativoPlusStudio.Interfaces.FirebaseSearchCollection;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using NativoPlusStudio.Interfaces.FirebaseUpdateUser;
using Azure.Identity;

namespace NativoPlusStudio.SharedConfiguration
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// This is usefull for .net core APIS
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static IServiceCollection ConfigurationServices(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddSingleton(x => Log.Logger.BuildCustomLogger(configuration));
            services.AddMediatR(typeof(AssemblyForMediatR));
            services.AddControllers();
            services.AddSwaggerDocument();

            services.Configure<ProgramOptions>(configuration.GetSection("ProgramOptions"));
            services.Configure<FirebaseOptions>(configuration.GetSection("FirebaseOptions"));

            services.AddScoped<IUploadFileService, UploadFileService>();
            services.AddScoped<ICreateUsersService, CreateUsersService>();
            services.AddScoped<IUpdateUserService, UpdateUserService>();
            services.AddScoped<IFirebaseClient, FirebaseClient>();
            services.AddScoped<IFirebaseConfig, FirebaseConfig>();            
            services.AddScoped<IGetUsersCollectionService, GetUsersCollectionService>();            

            services.AddMvc()
               .AddNewtonsoftJson(options =>
               {
                   options.SerializerSettings.Converters.Add(new StringEnumConverter());
                   options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
               }
               )
               .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0)
                     .ConfigureApiBehaviorOptions(options =>
                     {
                         options.SuppressModelStateInvalidFilter = true;
                     });
            return services;
        }

        public static IConfiguration BuildCustomConfiguration(this IConfiguration config)
        {
            var connectionString = Environment.GetEnvironmentVariable("AZCE");//Azure configuration enviroment url
            var aspnetcoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            checkConnectionStringRequirement(connectionString);
            checkAspNetCoreEnviromentVariableRequirement(aspnetcoreEnvironment);
            config = new ConfigurationBuilder()
                .AddEnvironmentVariables()                 
                .AddAzureAppConfiguration(options =>
                {
                     options.Connect(connectionString)

                             .ConfigureKeyVault(kv =>
                             {
                                 kv.SetCredential(new DefaultAzureCredential());

                             }).Select(KeyFilter.Any, LabelFilter.Null)
                             .Select(KeyFilter.Any, aspnetcoreEnvironment);
                })
                .SetBasePath(Directory.GetCurrentDirectory())                
                .AddJsonFile($"{AppContext.BaseDirectory}/appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("firebaseAuth.json")
                .AddJsonFile($"{AppContext.BaseDirectory }/appsettings.Production.json",                
                            optional: false, reloadOnChange: true)
                            .Build();

            return config;
        }  

        private static void checkConnectionStringRequirement(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new Exception($@"The connection string for the app configuration enviroment is null or empty. Please set an 
                enviroment variable named AZCE 
                cmd: setx AZCE " + "\"connection - string - of - your - app - configuration - store:\" if this is a devops pipeline please declare an enviroment variable called AZCE  and set it's value to appropiate enviroment being deployed to");
            }
        }
        private static void checkAspNetCoreEnviromentVariableRequirement(string aspnetcoreEnvironment)
        {
            if (string.IsNullOrWhiteSpace(aspnetcoreEnvironment))
            {
                throw new Exception($@"The ASPNETCORE_ENVIRONMENT string for the app configuration enviroment is null or empty. Please set an 
                enviroment variable named ASPNETCORE_ENVIRONMENT 
                cmd: setx ASPNETCORE_ENVIRONMENT " + "\"Development\" if this is a devops pipeline please declare an enviroment variable called ASPNETCORE_ENVIRONMENT and set it's value to appropiate enviroment being deployed to ");
            }
        }

        public static ILogger BuildCustomLogger(this ILogger log, IConfiguration configuration)
        {
            var loggerConfig = new LoggerConfiguration().CreateLogger();
            return loggerConfig;

        }

        public static void ConfigureFirebaseServices(this IServiceCollection services, Action<FirebaseOptions> action)
        {
            services.Configure(action);
        }
        
    }
}
