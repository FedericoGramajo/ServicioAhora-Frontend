using ClientLibrary.Models.Landing;
using ClientLibrary.Helper;

namespace ClientLibrary.Helper
{
    public static class MenuConstants
    {
        public static readonly IReadOnlyList<MenuActionModel> AccountActions = new List<MenuActionModel>
        {
            new("Mi perfil", RouteConstants.ClientDashboard),
            new("Panel profesional", RouteConstants.ProfessionalDashboard),
            new("Panel administrador", RouteConstants.AdminDashboard),
            new("Cerrar sesión", RouteConstants.Logout),
            new("Iniciar sesión", RouteConstants.Login),
            new("Registrarse", RouteConstants.Register)
        };

        public static readonly IReadOnlyList<NavLinkModel> PrimaryNavLinks = new List<NavLinkModel>
        {
            new("Inicio", RouteConstants.Home),
            new("Servicios", RouteConstants.Search),
            new("Profesionales", RouteConstants.Professionals),
            new("Contacto", RouteConstants.Contact)
        };
    }
}
