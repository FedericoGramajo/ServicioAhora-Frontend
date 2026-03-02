namespace ClientLibrary.Models.Booking.States
{
    public class PendingState : IBookingState
    {
        public string DisplayName => "Pendiente";
        public string BadgeClass => "bg-warning text-dark";
        public string IconClass => "fa-regular fa-clock";
        public bool CanAccept => true;
        public bool CanCancel => true;
        public bool CanStart => false;
        public bool CanFinish => false;
        public bool CanRate => false;
    }

    public class ConfirmedState : IBookingState
    {
        public string DisplayName => "Confirmado";
        public string BadgeClass => "bg-success";
        public string IconClass => "fa-solid fa-check";
        public bool CanAccept => false;
        public bool CanCancel => true;
        public bool CanStart => true;
        public bool CanFinish => false;
        public bool CanRate => false;
    }

    public class InProgressState : IBookingState
    {
        public string DisplayName => "En curso";
        public string BadgeClass => "bg-info text-dark";
        public string IconClass => "fa-solid fa-play";
        public bool CanAccept => false;
        public bool CanCancel => false;
        public bool CanStart => false;
        public bool CanFinish => true;
        public bool CanRate => false;
    }

    public class CompletedState : IBookingState
    {
        public string DisplayName => "Completado";
        public string BadgeClass => "bg-primary";
        public string IconClass => "fa-solid fa-flag-checkered";
        public bool CanAccept => false;
        public bool CanCancel => false;
        public bool CanStart => false;
        public bool CanFinish => false;
        public bool CanRate => true;
    }

    public class CanceledState : IBookingState
    {
        public string DisplayName => "Cancelado";
        public string BadgeClass => "bg-danger";
        public string IconClass => "fa-solid fa-xmark";
        public bool CanAccept => false;
        public bool CanCancel => false;
        public bool CanStart => false;
        public bool CanFinish => false;
        public bool CanRate => false;
    }

    public class UnknownState : IBookingState
    {
        public string DisplayName => "Desconocido";
        public string BadgeClass => "bg-secondary";
        public string IconClass => "fa-solid fa-question";
        public bool CanAccept => false;
        public bool CanCancel => false;
        public bool CanStart => false;
        public bool CanFinish => false;
        public bool CanRate => false;
    }
}
