namespace OrderBookMonitor.Infrastructure.Modules.OrderBook.Bitstamp.Models;

public record SubscriptionMessage(string Event, SubscriptionData Data);
