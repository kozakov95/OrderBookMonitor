using OrderBookMonitor.Modules.OrderBook.CommonModels;

namespace OrderBookMonitor.Modules.OrderBook.Messaging.State;

public class OrderBookStateContainer
{
    private OrderBookModel _data ;
    public OrderBookModel Data
    {
        get => _data;
        set
        {
            if (_data != value)
            {
                _data = value;
                NotifyStateChanged();
            }
        }
    }

    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();
}