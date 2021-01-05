using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Http;
using coreapi.MongoModel;
using Microsoft.Extensions.Options;
using System.Net;
using coreapi.MongoModel.Settings;

namespace coreapi.Exceptions
{
    public class RequestAuthorizeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<AppSettings> _appsettings;

        public RequestAuthorizeMiddleware(RequestDelegate next, IOptions<AppSettings> appsettings)
        {
            _next = next;
            _appsettings = appsettings;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"].ToString();
            if (authHeader != null && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader.Substring("Basic ".Length).Trim();
                var credentialstring = Encoding.GetEncoding("ISO-8859-1").GetString(Convert.FromBase64String(token));
                var credentials = credentialstring.Split(':');
                if (credentials[0] == _appsettings.Value.RequestAuth.UserName && credentials[1] == _appsettings.Value.RequestAuth.Password)
                {
                    await _next(context);
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return;
                }
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }
        }
    }
}
