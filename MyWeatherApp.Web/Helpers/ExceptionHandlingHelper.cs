using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyWeatherApp.Entities;
using MyWeatherApp.Entities.Exceptions;
using System.Text.Json;

namespace MyWeatherApp.Web.Helpers
{
    public static class ExceptionHandlingHelper
    {
        public static IApplicationBuilder RegisterExceptionHandling(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                { 
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        var statusCode = 500;
                        var ex = error.Error;
                        if (ex is BaseException be)
                        {
                            statusCode = be.StatusCode;
                        }
                        logger.LogError(ex, context.Request.Path + context.Request.QueryString);
                        await context.Response.WriteAsync(JsonSerializer.Serialize(new BaseEntity()
                        {
                            StatusCode = statusCode,
                            ErrorMessage = ex.Message
                        }));                        
                    }
                });
            });
            return app;
        }
    }
}
