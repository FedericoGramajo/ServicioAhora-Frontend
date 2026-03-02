using ClientLibrary.Models.Cart;

namespace ClientLibrary.Helper
{
    public static class Constant
    {
        public static class Product
        {
            public const string GetAll  = "product/all";
            public const string Get     = "product/single";
            public const string Add     = "product/add";
            public const string Update  = "product/update";
            public const string Delete  = "product/delete";
        }
        public static class ServiceOffering
        {
            public const string GetAll      = "serviceoffering/all";
            public const string Get         = "serviceoffering/single";
            public const string Add         = "serviceoffering/add";
            public const string Update      = "serviceoffering/update";
            public const string Deactivate  = "serviceoffering/deactivate"; // PATCH /api/ServiceOffering/deactivate/{id}
            public const string Delete      = "serviceoffering/delete";
            public const string GetByProfessional = "serviceoffering/by-professional"; // GET /api/ServiceOffering/by-professional/{professionalId}
            public const string GetAdminByDate = "serviceoffering/admin/all-by-date";
        }
        public static class Category
        {
            public const string GetAll                  = "category/all";
            public const string Get                     = "category/single";
            public const string Add                     = "category/add";
            public const string Update                  = "category/update";
            public const string Delete                  = "category/delete";
            public const string GetServiceByCategory    = "category/servoffering-by-category"; // /api/Category/servoffering-by-category/{categoryId}
        }
        public static class Authentication
        {
            public const string Type                    = "Bearer";
            public const string Register                = "authentication/create";
            public const string Login                   = "authentication/login";
            public const string ReviveToken             = "authentication/refreshToken";
            public const string RequestPasswordReset    = "authentication/request-password-reset";
            public const string VerifyToken             = "authentication/verifyToken";
            public const string ChangePassword          = "authentication/change-password";
            public const string Me                      = "authentication/me";
            public const string Logout                  = "authentication/logout";
            public const string Update                  = "authentication/update";
        }
        public static class ApiCallType
        {
            public const string Get     = "get";
            public const string Post    = "post";
            public const string Update  = "put";
            public const string Delete  = "delete";
        }
        public static class Cookie
        {
            public const string Name = "token";
            public const string Path = "/";
            public const int Days = 1; 
        }
        public static class ApiClient
        {
            public const string Name = "Blazor-Client";
        }
        public static class Payment
        {
            public const string GetAll = "payment/methods";
        }

        public static class Cart
        {
            public const string Checkout = "cart/checkout";
            public const string SaveCart = "cart/save-checkout";
            public const string Name = "my-cart";
            public const string GetAchieve = "cart/get-achieves";
        }
        public static class Administration
        {
            public const string AdminRole = "Admin";
            public const string ProfessionalRole = "Professional";
            public const string CustomerRole = "Customer";
        }

        public static class Address
        {
            public const string Get         = "Address/user";
            public const string Add         = "Address/add";
            public const string Update      = "Address/update";
            public const string Delete      = "Address/delete";
        }
        public static class ServiceHistory
        {
            public const string GetHistory = "servicehistory/get";
        }
        public static class Professional
        {
            public const string GetMetrics              = "dashboard/metrics";           
            public const string GetServiceGroups        = "professional/service-groups"; 
            public const string GetTransactions         = "dashboard/transactions";      

            public const string AddService              = "serviceoffering/add";
            public const string UpdateService           = "serviceoffering/update";
            public const string DeleteService           = "serviceoffering/delete"; // DELETE /api/ServiceOffering/delete/{id}

            public const string GetAvailableCategories  = "professionalcategory/all";        // GET /api/ProfessionalCategory/all
            public const string GetMyCategories         = "professionalcategory/by-professional"; // GET /api/ProfessionalCategory/by-professional/{professionalId}
            public const string AddMyCategory           = "professionalcategory/add";        // POST /api/ProfessionalCategory/add
            public const string RemoveMyCategory        = "professionalcategory/delete";     // DELETE /api/ProfessionalCategory/delete/{id}

            public const string GetAvailability         = "availability/professional"; // GET /api/Availability/professional/{professionalId}
            public const string AddAvailability         = "availability/add";          // POST /api/Availability/add
            public const string UpdateAvailability      = "availability/update";       // PUT /api/Availability/update
            public const string RemoveAvailability      = "availability/delete";       // DELETE /api/Availability/delete/{id}
            public const string BatchAVailability       = "availability/batch";

            // Certifications (no están en el OpenAPI — endpoints de backend)
            public const string GetCertifications       = "professional/certifications/get";
            public const string AddCertification        = "professional/certifications/add";
            public const string RemoveCertification     = "professional/certifications/remove";

            // Public listing
            public const string Get                     = "Professional/single";
            public const string GetAllPublic            = "Professional/all";
        }
        public static class Admin
        {
            public const string GetMetrics              = "dashboard/metrics";
            public const string GetTransactions         = "dashboard/transactions";
            public const string GetProfessionals        = "Professional/all";
            public const string UpdateProfessional      = "Professional/update";
            public const string GetAdminReports         = "dashboard/admin-reports";
            public const string GetProfessionalReports  = "dashboard/professional-reports";
            public const string GetCustomerReports      = "dashboard/customer-reports";
        }

        public static class AuditLog
        {
            public const string GetAll = "AuditLog/all";
        }

        public static class Notification
        {
            public const string Me = "Notification";
            public const string GetAllByUser = "Notification/my-notifications";
            public const string GetUnreadByUser = "Notification/my-unread-notifications";
            public const string MarkAsRead = "Notification/mark-as-read";
            public const string MarkAllAsRead = "Notification/mark-all-as-read";
        }

        public static class ProfessionalLicense
        {
            public const string Add = "ProfessionalLicense/add";
            public const string Update = "ProfessionalLicense/update";
            public const string Delete = "ProfessionalLicense/delete";
            public const string GetAll = "ProfessionalLicense/professional";
        }

        public static class Booking
        {
            public const string Create = "Booking/create";
            public const string GetByProfessional = "Booking/professional";
            public const string GetByCustomer = "Booking/customer";
            public const string UpdateStatus = "Booking/update-status";
            public const string Accept = "Booking/accept";
            public const string Cancel = "Booking/cancel";
            public const string GetAdminGlobal = "Booking/admin/global";
        }

        public static class Availability
        {
            public const string GetSlots = "Availability/slots";
        }
        public static class BookingStatus
        {
            public const string Pending = "Pendiente";
            public const string Confirmed = "Confirmado";
            public const string InProgress = "En curso";
            public const string Completed = "Finalizado";
            public const string Canceled = "Cancelado";
        }

        public static class Rating
        {
            public const string Post = "Rating";
            public const string GetByProfessional = "Rating/professional";
            public const string GetByService = "Rating/service";
        }
    }
}
