using System;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebSockets;
using static Test.Cryptocurrencies.CryptoPrices;

namespace Test.WebSocketFolder
{
    public class WebSocketHandler
    {
        public static async Task Handle(HttpContext httpContext, WebSocket webSocket)
        {
            const int maxMessageSize = 1024;

            //Buffer for received bits.
            var receivedDataBuffer = new ArraySegment<Byte>(new Byte[maxMessageSize]);

            var cancellationToken = new CancellationToken();

            //Checks WebSocket state.
            while (webSocket.State == WebSocketState.Open)
            {
                //Reads data.
                WebSocketReceiveResult webSocketReceiveResult =
                    await webSocket.ReceiveAsync(receivedDataBuffer, cancellationToken);

                //If input frame is cancelation frame, send close command.
                if (webSocketReceiveResult.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure,
                        string.Empty, cancellationToken);
                }
                else
                {
                    byte[] payloadData = receivedDataBuffer.Array.Where(b => b != 0).ToArray();

                    //Because we know that is a string, we convert it.
                    string received =
                        System.Text.Encoding.UTF8.GetString(receivedDataBuffer.Array, 0, webSocketReceiveResult.Count);

                    //Converts string to byte array.
                    string str = crDictionary.ContainsKey(received) 
                        ? $"{received}: {crDictionary[received]}" : 
                        $"{received}: Currency Pair Not Registered.";

                    var bytes = System.Text.Encoding.UTF8.GetBytes(str);

                    //Sends data back.
                    await webSocket.SendAsync(new ArraySegment<byte>(bytes),
                        WebSocketMessageType.Text, true, cancellationToken);
                }
            }
        }
    }
}