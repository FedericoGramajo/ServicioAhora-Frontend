using ClientLibrary.Helper;
using ClientLibrary.Models;
using ClientLibrary.Models.Notifications;
using ClientLibrary.Services.Contracts;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations
{
    public class NotificationService(IHttpClientHelper httpClient, IApiCallHelper apiHelper) : INotificationService
    {
        public event Action? OnChange;
        private List<Notification> _notifications = new();

        public IReadOnlyList<Notification> Notifications => _notifications.AsReadOnly();
        public int UnreadCount => _notifications.Count(n => !n.IsRead);

        public async Task LoadInitialNotifications(string userId)
        {
            Console.WriteLine($"[Notifications] Loading for user: {userId}");
            var notifications = await GetMyNotificationsAsync(userId);
            if (notifications != null)
            {
                _notifications = notifications.ToList();
                Console.WriteLine($"[Notifications] Loaded {_notifications.Count} notifications");
                NotifyStateChanged();
            }
            else
            {
                Console.WriteLine("[Notifications] Failed to load notifications (null result)");
            }
        }

        public async Task<IEnumerable<Notification>> GetMyNotificationsAsync(string userId)
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constant.Notification.GetAllByUser,
                Type = Constant.ApiCallType.Get,
                Client = client
            };
            apiCall.ToString(userId);
            Console.WriteLine($"[Notifications] Calling API: {Constant.Notification.GetAllByUser}/{userId}");
            var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
            if (result != null && result.IsSuccessStatusCode)
            {
                var data = await apiHelper.GetServiceResponse<IEnumerable<Notification>>(result);
                return data ?? [];
            }
            Console.WriteLine($"[Notifications] API Error: {result?.StatusCode}");
            return [];
        }

        public async Task<IEnumerable<Notification>> GetMyUnreadNotificationsAsync(string userId)
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constant.Notification.GetUnreadByUser,
                Type = Constant.ApiCallType.Get,
                Client = client
            };
            apiCall.ToString(userId);
            var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
            if (result != null && result.IsSuccessStatusCode)
                return await apiHelper.GetServiceResponse<IEnumerable<Notification>>(result) ?? [];
            return [];
        }

        public async Task<ServiceResponse> MarkAsReadAsync(Guid id)
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constant.Notification.MarkAsRead,
                Type = Constant.ApiCallType.Post,
                Client = client
            };
            apiCall.ToString(id);
            var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
            var response = result == null ? apiHelper.ConnectionError() : await apiHelper.GetServiceResponse<ServiceResponse>(result);
            
            if (response != null && response.success)
            {
                var notification = _notifications.FirstOrDefault(n => n.Id == id);
                if (notification != null)
                {
                    notification.IsRead = true;
                    NotifyStateChanged();
                }
            }
            
            return response ?? new ServiceResponse(false, "El servidor devolvió un error inesperado al marcar la notificación como leída");
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
