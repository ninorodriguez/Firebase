using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NativoPlusStudio.SharedConfiguration;

namespace NativoPlusStudio.FirebaseApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration.BuildCustomConfiguration();          
        }        

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigurationServices(Configuration);            
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            SetUpSwagger(app);

            app
             .UseHttpsRedirection()
             .UseRouting()
             .UseEndpoints(endpoints =>
             {
                 endpoints.MapControllers();
             });
        }

        
        private void SetUpSwagger(IApplicationBuilder app)
        {

            var enviromentName =
                Configuration["ProgramOptions:SwaggerTitle"] == null ?
                    "MisconfiguredLosFacade" : Configuration["ProgramOptions:SwaggerTitle"];

            app.UseOpenApi(x => {
                x.DocumentName = enviromentName;
                x.PostProcess = (document, request) =>
                {
                    document.Info.Title = enviromentName;
                    document.Info.Description = "Interaction with Firebase";
                };

            });
            app.UseSwaggerUi3(x => {
                x.DocumentTitle = enviromentName;
            });
        }
    }
}
