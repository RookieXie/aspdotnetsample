using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Core
{
    public class RequestLogMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<RequestLogMiddleware> logger;
        public RequestLogMiddleware(RequestDelegate next, ILogger<RequestLogMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            logger.LogDebug("登录测试：IP=" + context.Connection.RemoteIpAddress.ToString());
            await next.Invoke(context);
            //context.Response.StatusCode = 404;
        }
    }
}
