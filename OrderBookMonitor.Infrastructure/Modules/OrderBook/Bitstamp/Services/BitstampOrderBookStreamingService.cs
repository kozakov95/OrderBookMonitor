using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using CSharpFunctionalExtensions;
using OrderBookMonitor.Infrastructure.Modules.OrderBook.Bitstamp.Constants;
using OrderBookMonitor.Infrastructure.Modules.OrderBook.Bitstamp.Models;
using OrderBookMonitor.Modules.OrderBook.CommonModels;


namespace OrderBookMonitor.Infrastructure.Modules.OrderBook.Bitstamp.Services;

public class BitstampOrderBookStreamingService : IOrderBookStreamingService
{
    private static readonly HashSet<WebSocketState> _closingWebSocketStates  = new()
    {
        WebSocketState.Closed,
        WebSocketState.CloseReceived,
        WebSocketState.CloseSent,
        WebSocketState.Aborted,
    };
    
    private static readonly Uri _baseUri = new("wss://ws.bitstamp.net");
    
    private ClientWebSocket? _webSocket;
    
    private static JsonSerializerOptions _jsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower };
    
    //Saw this buffer size of 4735 in the Bitstamp example page for order book
    //In the example page -> network -> WS area
    //When it was exchanging data with the server
    //Yet, the size is unstable, it tends to grow a bit from time to time
    private static byte[] _buffer = new byte[4735 * 2];

    public BitstampOrderBookStreamingService()
    {

    }

    public async Task StartStreamingAsync(CancellationToken cancellationToken)
    {
        await OpenConnectionAsync(cancellationToken);

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var receivedMessage = await ReadMessageAsync(cancellationToken);

                if (receivedMessage.IsFailure)
                    break;
                
                await SendMessagesToClients(receivedMessage.Value, cancellationToken);
            }            
            catch (WebSocketException)
            {
                await CloseConnection(cancellationToken);
                await OpenConnectionAsync(cancellationToken);
            }
            catch (OperationCanceledException) // when (cancellationToken.IsCancellationRequested)
            {
                break;
            }
        }
    }

    public async Task StopStreamingAsync(CancellationToken cancellationToken)
    {
        await CloseConnection(cancellationToken);
    }

    public event Func<OrderBookPollingModel, Task>? OnDataReceived;

    private async Task SendMessagesToClients(ReadOnlyMemory<byte> memory, CancellationToken cancellationToken)
    {
        var messageObject = JsonSerializer.Deserialize<OrderBookPollingModel>(memory.Span);
        await OnDataReceived?.Invoke(messageObject);
    }

    private async Task OpenConnectionAsync(CancellationToken cancellationToken)
    {
        while (true)
        {
            _webSocket?.Dispose();
            _webSocket = new ClientWebSocket();
            await _webSocket.ConnectAsync(_baseUri, cancellationToken);
            
            var subscribeJson = JsonSerializer.Serialize(WebSocketMessages.Subscribe, _jsonOptions);
            var subscribeBytes = Encoding.UTF8.GetBytes(subscribeJson);
            var subscribeSegment = new ArraySegment<byte>(subscribeBytes);
            
            await _webSocket.SendAsync(subscribeSegment, WebSocketMessageType.Binary, true, cancellationToken);
            
            var response = await ReadMessageAsync(cancellationToken);

            if (response.IsFailure)
                continue;

            var responseString = ReadStringFromByteResult(response.Value.Span.ToArray());
            
            if (!string.IsNullOrEmpty(responseString) &&
                responseString.Contains("bts:subscription_succeeded", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }
            
            await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
        }
    }

    private string ReadStringFromByteResult(byte[] byteResult)
    {
        return Encoding.UTF8.GetString(byteResult);
    }

    private async Task CloseConnection(CancellationToken cancellationToken)
    {
        if (_webSocket == null)
        {
            return;
        }
        
        if (!_closingWebSocketStates.Contains(_webSocket.State))
        {
            await SendUnsubscribeMessageAsync(cancellationToken);
            await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, cancellationToken);
        }
        
        _webSocket.Dispose();
        _webSocket = null;
    }

    private async Task SendUnsubscribeMessageAsync(CancellationToken cancellationToken)
    {
        if (_webSocket == null)
        {
            return;
        }

        var unsubscribeJson = JsonSerializer.Serialize(WebSocketMessages.Unsubscribe, _jsonOptions);
        var unsubscribeBytes = Encoding.UTF8.GetBytes(unsubscribeJson);
        var unsubscribeSegment = new ArraySegment<byte>(unsubscribeBytes);
        
        await _webSocket.SendAsync(unsubscribeSegment, WebSocketMessageType.Text, true, cancellationToken);
    }
    
    private async Task<Result<ReadOnlyMemory<byte>>> ReadMessageAsync(CancellationToken cancellationToken)
    {
        if (_webSocket == null)
        {
            return Result.Failure<ReadOnlyMemory<byte>>(string.Empty);
        }
        
        using var ms = new MemoryStream(_buffer);
        var resultBytesCount = 0;
        // This cycle is needed in case we receive only a part of the data for some reason
        while (true)
        {
            var result = await _webSocket!.ReceiveAsync(new ArraySegment<byte>(_buffer), cancellationToken);
            
            if (result.MessageType == WebSocketMessageType.Close)
            {
                await CloseConnection(cancellationToken);
                return Result.Failure<ReadOnlyMemory<byte>>(string.Empty);
            }
            
            ms.Write(_buffer, 0, result.Count);

            if (result.EndOfMessage)
            {
                resultBytesCount += result.Count;
                break;
            }
        }
        
        ms.Seek(0, SeekOrigin.Begin);
        var memory = new ReadOnlyMemory<byte>(ms.ToArray(), 0, resultBytesCount);
        
        return memory;
    }
}
