# Future Implementations

## Frontend (Consejos)

### Calendar
- Use a calendar library like **FullCalendar** or **react-calendar** (if using React) or native Blazor/MVC components.
- Do not render the entire month at once if there is significant logic; load availability on demand (when the user clicks on a day).

### Real-time Notifications (Bonus)
- To make notifications appear "instantly" without refreshing the page, use **SignalR**.
- Create a `NotificationHub`.
- When the Backend confirms payment, in addition to saving to the DB, invoke:
  ```csharp
  hubContext.Clients.User(userId).SendAsync("ReceiveNotification", message);
  ```
