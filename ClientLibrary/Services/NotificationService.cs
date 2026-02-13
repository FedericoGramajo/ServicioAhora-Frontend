using ClientLibrary.Models.Notifications;

namespace ClientLibrary.Services;

public class NotificationService
{
    public event Action? OnChange;
    private List<Notification> _notifications = new();

    public IReadOnlyList<Notification> Notifications => _notifications.AsReadOnly();
    public int UnreadCount => _notifications.Count(n => !n.IsRead);

    public NotificationService()
    {
        // Mock initial notifications
        _notifications.Add(new Notification 
        { 
            Id = Guid.NewGuid(), 
            Title = "Bienvenido", 
            Message = "Completa tu perfil para mejorar tu experiencia.", 
            CreatedAt = DateTime.Now.AddDays(-1) 
        });
        
        _notifications.Add(new Notification 
        { 
            Id = Guid.NewGuid(), 
            Title = "Descuento", 
            Message = "Tienes un 10% off en tu primer servicio de Plomería.", 
            CreatedAt = DateTime.Now.AddHours(-2) 
        });
    }

    public void AddNotification(Notification notification)
    {
        _notifications.Insert(0, notification);
        NotifyStateChanged();
    }

    public void MarkAsRead(Guid id)
    {
        var notification = _notifications.FirstOrDefault(n => n.Id == id);
        if (notification != null && !notification.IsRead)
        {
            notification.IsRead = true;
            NotifyStateChanged();
        }
    }

    public void MarkAllAsRead()
    {
        foreach (var notification in _notifications)
        {
            notification.IsRead = true;
        }
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
