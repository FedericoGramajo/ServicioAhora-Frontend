namespace ClientLibrary.Helper
{
    public static class RouteConstants
    {
        // Public Pages
        public const string Home = "/";
        public const string Login = "/login";
        public const string Register = "/register";
        public const string ForgotPassword = "/forgot-password";
        public const string ResetPassword = "/reset-password";
        public const string ResetPasswordPage = "/reset-password/{token}";
        public const string Contact = "/contacto";
        
        // Search & Discovery
        public const string Search = "/search";
        public const string Professionals = "/profesionales";
        public const string ServiceDetail = "/servicio"; // Base for /servicio/{Slug}
        public const string ServiceDetailPage = "/servicio/{Slug}"; 
        public const string ProfessionalDetail = "/profesional"; // Base for /profesional/{Slug}
        public const string ProfessionalDetailPage = "/profesional/{Slug}";

        // User Account
        public const string Profile = "/perfil";
        public const string UserServices = "/mis-servicios";

        // Dashboards
        public const string ProfessionalDashboard = "/professional/dashboard";
        public const string ClientDashboard = "/client/dashboard";
        public const string AdminDashboard = "/admin/dashboard";
        public const string OldAdminDashboard = "/oldpanel-administrador";

        // Authentication
        public const string Logout = "/authentication/logout";
        public const string AuthenticationLogin = "/authentication/login";
        public const string AuthenticationLoginPageWithRoute = "/authentication/login/{route}";
        public const string AuthenticationRegister = "/authentication/register";

        // E-Commerce & Admin
        public const string MyCart = "/my-cart";
        public const string SearchResult = "/search-result";
        public const string SearchResultPage = "/search-result/{filter}";
        public const string AdminCategory = "/category";
        public const string AdminProduct = "/product";
        public const string AdminSales = "/admin";
        public const string PaymentSuccess = "/payment-success";
        public const string PaymentCancel = "/payment-cancel";

        public const string MainProductCategory = "/main/products/category/{categoryId}";
        
        // Anchor Links (hashes)
        public const string HashInicio = "/#inicio";
        public const string HashServices = "/#services";
        public const string HashGarantias = "/#garantias";
        public const string HashContacto = "/#contacto";

        /// <summary>
        /// Helper to create route with parameter
        /// </summary>
        public static string Create(string baseRoute, string param) => $"{baseRoute}/{param}";
    }
}
