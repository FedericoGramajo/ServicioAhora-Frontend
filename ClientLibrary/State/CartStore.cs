using System.Collections.Concurrent;
using ClientLibrary.Models.Cart;
using ClientLibrary.Helper;

namespace ClientLibrary.State
{
    public class CartStore(ICookieStorageService cookieService)
    {
        private readonly ConcurrentDictionary<string, CartItem> _items = new();
        private const string CartCookieName = "Cart-ServicioAhora";

        public event Action? OnChange;

        public IEnumerable<CartItem> Items => _items.Values;

        public int Count => _items.Values.Sum(i => i.Quantity);

        public decimal Total => _items.Values.Sum(i => i.TotalPrice);

        public async Task InitializeAsync()
        {
            var cartJson = await cookieService.GetCookieAsync(CartCookieName);
            if (!string.IsNullOrEmpty(cartJson))
            {
                try
                {
                    var items = System.Text.Json.JsonSerializer.Deserialize<List<CartItem>>(cartJson);
                    if (items != null)
                    {
                        _items.Clear();
                        foreach (var item in items)
                        {
                            _items.TryAdd(item.ServiceId, item);
                        }
                        NotifyStateChanged();
                    }
                }
                catch { /* Ignore deserialization errors */ }
            }
        }

        public async Task AddToCartAsync(CartItem item)
        {
            if (_items.TryGetValue(item.ServiceId, out var existingItem))
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                _items.TryAdd(item.ServiceId, item);
            }

            await SaveToCookiesAsync();
            NotifyStateChanged();
        }

        public async Task RemoveFromCartAsync(string serviceId)
        {
            if (_items.TryRemove(serviceId, out _))
            {
                await SaveToCookiesAsync();
                NotifyStateChanged();
            }
        }
        
        public async Task UpdateQuantityAsync(string serviceId, int quantity)
        {
            if (_items.TryGetValue(serviceId, out var existingItem))
            {
                if (quantity <= 0)
                {
                    await RemoveFromCartAsync(serviceId);
                }
                else
                {
                    existingItem.Quantity = quantity;
                    await SaveToCookiesAsync();
                    NotifyStateChanged();
                }
            }
        }

        public async Task ClearCartAsync()
        {
            _items.Clear();
            await SaveToCookiesAsync();
            NotifyStateChanged();
        }

        private async Task SaveToCookiesAsync()
        {
            var cartJson = System.Text.Json.JsonSerializer.Serialize(_items.Values.ToList());
            await cookieService.SetCookieAsync(CartCookieName, cartJson, 7, "/");
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
