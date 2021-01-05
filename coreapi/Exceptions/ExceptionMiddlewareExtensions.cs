using coreapi.LogApiHandler;
using coreapi.MongoModel.Settings;
using coreapi.MongoModel.TableName;
using coreapi.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Net;

namespace coreapi.Exceptions
{
    /// <summary>
    /// 全局异常处理中间件
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IOptions<DBSettings> settings)
        {
            LogRepository _repository = new LogRepository(settings);
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        await _repository.AddOne(new LogEventData
                        {
                            Message = contextFeature.Error.ToString(),
                            Date = DateTime.Now,
                            Level = "Fatal",
                            LogSource = "LogWebApi"
                        });
                        await context.Response.WriteAsync(context.Response.StatusCode + "-Internal Server Error.");
                    }
                });
            });
        }
    }
}
