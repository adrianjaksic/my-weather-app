using Microsoft.Extensions.DependencyInjection;

namespace MyWeatherApp.Web.Helpers
{
    public static class RegisterAppCorsHelper
    {
        public const string AppCorsPolicyName = "MyWearherAppCors";

        public static IServiceCollection RegisterAppCors(this IServiceCollection services, string clientUrl)
        {
            var urls = clientUrl.Split(',');
            services.AddCors(options =>
            {
                options.AddPolicy(AppCorsPolicyName,
                builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.WithOrigins(urls);
                });
            });
            return services;
        }
    }
}
