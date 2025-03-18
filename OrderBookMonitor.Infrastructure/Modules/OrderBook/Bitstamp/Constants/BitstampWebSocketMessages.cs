using OrderBookMonitor.Infrastructure.Modules.OrderBook.Bitstamp.Models;

namespace OrderBookMonitor.Infrastructure.Modules.OrderBook.Bitstamp.Constants;

public static class WebSocketMessages
{
    public static readonly SubscriptionMessage Subscribe =
        new("bts:subscribe", new SubscriptionData("order_book_btcusd"));

    public static readonly SubscriptionMessage Unsubscribe =
        new("bts:unsubscribe", new SubscriptionData("order_book_btcusd"));
}