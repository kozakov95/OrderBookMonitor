using System.Text.Json;
using System.Text.Json.Serialization;
using OrderBookMonitor.Modules.OrderBook.Bitstamp.Models;
using OrderBookMonitor.Modules.OrderBook.CommonModels;

namespace OrderBookMonitor.Modules.OrderBook.Bitstamp.JsonConverters;

public class OrderBookEntryModelConverter : JsonConverter<OrderBookEntryModel>
{
    public override OrderBookEntryModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        reader.Read();
        string price = reader.GetString();

        reader.Read();

        string amount = reader.GetString();

        reader.Read();

        return new OrderBookEntryModel { Price = price, Amount = amount };
    }

    public override void Write(Utf8JsonWriter writer, OrderBookEntryModel value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteStringValue(value.Price);
        writer.WriteStringValue(value.Amount);
        writer.WriteEndArray();
    }
}