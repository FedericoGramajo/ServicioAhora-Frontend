namespace ClientLibrary.Models.Booking.States
{
    public interface IBookingState
    {
        string DisplayName { get; }
        string BadgeClass { get; }
        string IconClass { get; }
        
        bool CanAccept { get; }
        bool CanCancel { get; }
        bool CanStart { get; }
        bool CanFinish { get; }
        bool CanRate { get; }
    }
}
