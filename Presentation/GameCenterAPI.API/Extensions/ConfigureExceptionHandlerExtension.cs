using GameCenterAPI.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace GameCenterAPI.API.Extensions
{
    public static class ConfigureExceptionHandlerExtension
    {
        public static void ConfigureExceptionHandler<T>(this WebApplication application, ILogger<T> logger)
        {
            application.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {


                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Application.Json;

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        //loglama
                        logger.LogError(contextFeature.Error.Message);
                        if (contextFeature.Error.GetType() == typeof(ValidationException))
                        {
                            ValidationException exception = (ValidationException)contextFeature.Error;

                            context.Response.StatusCode = 403;

                            await context.Response.WriteAsync(JsonSerializer.Serialize(new
                            {
                                Title = "Validation Error",
                                ErrorType = contextFeature.Error.GetType().ToString(),
                                StatusCode = context.Response.StatusCode,
                                Message = contextFeature.Error.Message,
                                ValidationErrors = exception.Errors
                            }));
                        }
                        else
                        {
                            await context.Response.WriteAsync(JsonSerializer.Serialize(new
                            {
                                Title = "Hata Alındı",
                                ErrorType = contextFeature.Error.GetType().ToString(),
                                StatusCode = context.Response.StatusCode,
                                Message = contextFeature.Error.Message
                            }));
                        }

                    }
                });
            });
        }
    }
}
