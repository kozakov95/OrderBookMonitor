namespace OrderBookMonitor.Modules.OrderBook.Bitstamp.Models;

public record SubscriptionMessage(string Event, SubscriptionData Data);