using System.Text.Json;
using System.Text.Json.Serialization;
using OrderBookMonitor.Infrastructure.Modules.OrderBook.Bitstamp.Models;

namespace OrderBookMonitor.Infrastructure.Modules.OrderBook.Bitstamp.JsonConverters;

public class OrderBookEntryModelConverter : JsonConverter<OrderBookEntryPollingModel>
{
    public override OrderBookEntryPollingModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        reader.Read();
        var price = decimal.Parse(reader.GetString());

        reader.Read();

        var amount = decimal.Parse(reader.GetString());

        reader.Read();

        return new OrderBookEntryPollingModel { Price = price, Amount = amount };
    }

    public override void Write(Utf8JsonWriter writer, OrderBookEntryPollingModel value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteNumberValue(value.Price);
        writer.WriteNumberValue(value.Amount);
        writer.WriteEndArray();
    }
}