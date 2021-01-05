using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.WebSockets;

namespace webapi_websocket.Controllers
{
    public class ChatController : ApiController
    {
        public static List<WebSocket> webSockets = new List<WebSocket> { };

        public const int BufferSize = 4096;
        [HttpGet]
        public HttpResponseMessage Lo()
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                HttpContext.Current.AcceptWebSocketRequest(RequestData);
            }
            return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
        }
        public async Task RequestData(AspNetWebSocketContext context)
        {
            var webSocket = context.WebSocket;
            webSockets.Add(webSocket);

            var isSwitch = true;
            while (isSwitch)
            {
                ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[BufferSize]);
                var result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
                if (webSocket.State != WebSocketState.Open)
                {
                    if (webSockets.Contains(webSocket)) webSockets.Remove(webSocket);
                    break;
                }
                var message = Encoding.UTF8.GetString(buffer.ToArray(), 0, result.Count);
                var recvBytes = Encoding.UTF8.GetBytes(message);
                ArraySegment<byte> sendBuffer = new ArraySegment<byte>(recvBytes);

                foreach (var innerSocket in webSockets)
                {
                    if (innerSocket != webSocket)
                    {
                        await innerSocket.SendAsync(sendBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                }
            }
        }




    }
}
