using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Test.WebSocketFolder
{
    public class WebSocketMiddlewareClass
    {
        private readonly RequestDelegate next;

        public WebSocketMiddlewareClass(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                await WebSocketHandler.Handle(context, webSocket);
            }

            // Call the next delegate/middleware in the pipeline
            await this.next(context);
        }
    }

    public static class WebSocketMiddlewareExtensions
    {
        public static IApplicationBuilder UseWebSocketHandler(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WebSocketMiddlewareClass>();
        }
    }
}