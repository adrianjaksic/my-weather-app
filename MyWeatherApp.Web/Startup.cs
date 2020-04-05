using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyWeatherApp.Entities;
using MyWeatherApp.Web.Helpers;

namespace MyWeatherApp.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .RegisterResponseConverters();
            
            services.AddOptions()   
                .AddHttpClient()
                .Configure<AppSettings>(Configuration.GetSection("AppSettings"))
                .RegisterAppServices();

            var clientUrl = Configuration.GetSection("AppSettings").GetValue<string>("ClientUrl");
            services.RegisterAppCors(clientUrl);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var loggerFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Startup>();

            app.RegisterExceptionHandling(logger)
                .UseCors(RegisterAppCorsHelper.AppCorsPolicyName)
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
