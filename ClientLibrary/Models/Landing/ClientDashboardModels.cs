using System;

namespace ClientLibrary.Models.Landing
{
    public record ServiceHistoryItem(Guid ServiceId, string Service, string Category, string Professional, DateTime Date, string Status, decimal Total, bool IsRated)
    {
        public Booking.States.IBookingState State => Booking.States.BookingStateFactory.Create(Status);
    }
}
