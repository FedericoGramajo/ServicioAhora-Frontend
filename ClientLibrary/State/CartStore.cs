using ClientLibrary.Models.Cart;
using System.Collections.Concurrent;

namespace ClientLibrary.State;

public class CartStore
{
    // Using a dictionary for easier management by ID
    private readonly ConcurrentDictionary<string, CartItem> _items = new();

    public event Action? OnChange;

    public IEnumerable<CartItem> Items => _items.Values;

    public int Count => _items.Values.Sum(i => i.Quantity);

    public decimal Total => _items.Values.Sum(i => i.TotalPrice);

    public void AddToCart(CartItem item)
    {
        if (_items.TryGetValue(item.ServiceId, out var existingItem))
        {
            existingItem.Quantity += item.Quantity;
        }
        else
        {
            _items.TryAdd(item.ServiceId, item);
        }

        NotifyStateChanged();
    }

    public void RemoveFromCart(string serviceId)
    {
        if (_items.TryRemove(serviceId, out _))
        {
            NotifyStateChanged();
        }
    }
    
    public void UpdateQuantity(string serviceId, int quantity)
    {
        if (_items.TryGetValue(serviceId, out var existingItem))
        {
            if (quantity <= 0)
            {
                RemoveFromCart(serviceId);
            }
            else
            {
                existingItem.Quantity = quantity;
                NotifyStateChanged();
            }
        }
    }

    public void ClearCart()
    {
        _items.Clear();
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
