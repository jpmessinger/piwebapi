using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PIWebAPI_Demo
{
    public class PIWebAPIChannel
    {
        private ClientWebSocket ws;

        public PIWebAPIChannel(string uri)
        {
            ws = new ClientWebSocket();
            ws.Options.SetRequestHeader("Authorization", "Basic xxxxxxxxxx");
        }

        public async Task GetAsync(string url, CancellationToken cancellationToken)
        {
            Uri uri = new Uri("wss://myserver/piwebapi/streams/{webId}/channel");

            WebSocketReceiveResult receiveResult;
            byte[] receiveBuffer = new byte[65536];
            ArraySegment<byte> receiveSegment = new ArraySegment<byte>(receiveBuffer);

            using (ClientWebSocket webSocket = new ClientWebSocket())
            {
                try
                {
                    await webSocket.ConnectAsync(uri, CancellationToken.None);
                }
                catch (WebSocketException)
                {
                    Console.WriteLine("Could not connect to server.");
                    return;
                }

                while (true)
                {
                    try
                    {
                        receiveResult = await webSocket.ReceiveAsync(receiveSegment, cancellationToken);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }

                    if (receiveResult.MessageType != WebSocketMessageType.Text)
                    {
                        await webSocket.CloseAsync(
                            WebSocketCloseStatus.InvalidMessageType,
                            "Message type is not text.",
                            CancellationToken.None);
                        return;
                    }
                    else if (receiveResult.Count > receiveBuffer.Length)
                    {
                        await webSocket.CloseAsync(
                            WebSocketCloseStatus.InvalidPayloadData,
                            "Message is too long.",
                            CancellationToken.None);
                        return;
                    }

                    string message = Encoding.UTF8.GetString(receiveBuffer, 0, receiveResult.Count);

                    Console.WriteLine(message);
                }

                await webSocket.CloseAsync(
                WebSocketCloseStatus.NormalClosure,
                "Closing connection.",
                CancellationToken.None);
            }
        }
    }
}
