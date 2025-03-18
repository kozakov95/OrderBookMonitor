using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using OrderBookMonitor.Application.Modules.OrderBook.Models;
using OrderBookMonitor.Application.Modules.OrderBook.Queries.CalculateDepthOfMarket;
using OrderBookMonitor.Common.Modules.OrderBook.Constants;
using OrderBookMonitor.Common.Modules.OrderBook.DTO;
using OrderBookMonitor.Infrastructure.Modules.OrderBook.Bitstamp.Models;
using OrderBookMonitor.Infrastructure.Modules.OrderBook.Bitstamp.Services;
using OrderBookMonitor.Infrastructure.Modules.OrderBook.Logging.Models;
using OrderBookMonitor.Infrastructure.Modules.OrderBook.Messaging.Hubs;
using Serilog;

namespace OrderBookMonitor.Infrastructure.Modules.OrderBook.HostedServices;

public class OrderBookStreamingBackgroundService: BackgroundService
{
    private readonly IHubContext<OrderBookHub> _hubContext;
    
    private readonly IOrderBookStreamingService _streamingService;
    
    private readonly IMapper _mapper;

    private readonly IMediator _mediator;
    
    public OrderBookStreamingBackgroundService(IOrderBookStreamingService streamingService, IHubContext<OrderBookHub> hubContext, IMapper mapper, IMediator mediator)
    {
        _streamingService = streamingService;
        _hubContext = hubContext;
        _mapper = mapper;
        _mediator = mediator;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            _streamingService.OnDataReceived += ProcessOrderBook;
            await _streamingService.StartStreamingAsync(stoppingToken);
        }
        finally
        {
            await _streamingService.StopStreamingAsync(stoppingToken);
            _streamingService.OnDataReceived -= ProcessOrderBook;
        }
    }

    private async Task ProcessOrderBook(OrderBookPollingModel orderBookModel)
    {
        var applicationModel = _mapper.Map<OrderBookModel>(orderBookModel.DataPollingModel);
        var domCalculatedResult = await _mediator.Send(new CalculateDepthOfMarketQuery(applicationModel));

        var orderBookLogEntry = _mapper.Map<OrderBookLogEntry>(domCalculatedResult.Value);
        Log.Information("Processing order book data: {@orderBookLogEntry}", orderBookLogEntry);
        var objectToSend = _mapper.Map<DepthOfMarketDto>(domCalculatedResult.Value);
        await _hubContext.Clients.All.SendAsync(SignalRMethodConstants.OrderBookStreaming, objectToSend);
    }
}