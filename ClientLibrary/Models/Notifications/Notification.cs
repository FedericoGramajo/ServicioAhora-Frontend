using System;

namespace ClientLibrary.Models.Notifications;

public class Notification
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public Guid? ReferenceId { get; set; } 
    public string ReferenceType { get; set; } = string.Empty;
}
