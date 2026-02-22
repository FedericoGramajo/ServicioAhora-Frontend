using System;

namespace ClientLibrary.Models.Landing
{
    public record ServiceHistoryItem(string Service, string Category, string Professional, DateTime Date, string Status, decimal Total);
}
