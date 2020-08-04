using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Masking.Serilog;
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
using Microsoft.Extensions.Options;

namespace NativoPlusStudio.SharedConfiguration
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// This is usefull for .net core APIS
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigurationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(x => Log.Logger.BuildCustomLogger(configuration));
            services.AddMediatR(typeof(AssemblyForMediatR));
            services.AddControllers();
            services.AddSwaggerDocument();

            services.Configure<ProgramOptions>(configuration.GetSection("ProgramOptions"));
            services.Configure<FirebaseOptions>(configuration.GetSection("FirebaseOptions"));

            services.AddScoped<IUploadFileService, UploadFileService>();
            services.AddScoped<ICreateUsersService, CreateUsersService>();
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

        }

        public static IConfiguration BuildCustomConfiguration(this IConfiguration config)
        {            
            config = new ConfigurationBuilder()
                .AddEnvironmentVariables()  
                .AddUserSecrets<FirebaseOptions>()
                .AddAzureAppConfiguration(options => {
                    options.Connect(config["AzureAppConfiguration:ConnectionString"])
                           .Select(KeyFilter.Any, LabelFilter.Null)
                           .Select(KeyFilter.Any, config["ProgramOptions:Environment"]);
                })
                .SetBasePath(Directory.GetCurrentDirectory())                
                .AddJsonFile($"{AppContext.BaseDirectory}/appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"{AppContext.BaseDirectory }/appsettings.Production.json",                
                            optional: false, reloadOnChange: true)
                            .Build();

            return config;
        }        

        public static ILogger BuildCustomLogger(this ILogger log, IConfiguration configuration)
        {
            var apiServiceName = configuration["DatadogOptions:Service"];
            var dataDogApiKey = configuration["DatadogOptions:ApiKey"];
            var dataDogSource = configuration["DatadogOptions:Source"];
            var dataDogTag = configuration["DatadogOptions:Tag"];
            var loggerConfig = new LoggerConfiguration()
                .Destructure.ByMaskingProperties("PrimarySocialSecurityNumber", "JointSocialSecurityNumber", "CustomerBankAccountNumber", "RoutingNumber", "AccountNumber")
                  .ReadFrom.Configuration(configuration)
                      .Enrich.FromLogContext()
                      .Enrich.WithMachineName()
                      .WriteTo.DatadogLogs(apiKey: dataDogApiKey, service: apiServiceName, source: dataDogSource, host: Environment.MachineName ?? "unknown", tags: new string[] { dataDogTag })
                      .Enrich.WithEnvironmentUserName()
                      .CreateLogger();
            return loggerConfig;

        }
    }
}
