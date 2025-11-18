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
        public static class Category
        {
            public const string GetAll                  = "category/all";
            public const string Get                     = "category/single";
            public const string Add                     = "category/add";
            public const string Update                  = "category/update";
            public const string Delete                  = "category/delete";
            public const string GetProductByCategory    = "category/products-by-category";
        }
        public static class Authentication
        {
            public const string Type        = "Bearer";
            public const string Register    = "authentication/register";
            public const string Login       = "authentication/login";
            public const string ReviveToken = "authentication/refreshToken";
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
            public const int Seconds = 86400; //La libreria que uso cambio de dias a segundos
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
        }
    }
}
