using BlazorWasm;
using BlazorWasm.Authentication;
using ClientLibrary.Helper;
using ClientLibrary.Services;
using ClientLibrary.Services.Contracts;
using ClientLibrary.Services.Implementations;
using ClientLibrary.State;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NetcodeHub.Packages.Components.DataGrid;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add HttpClientFactory explicitly
builder.Services.AddHttpClient();

builder.Services.AddScoped<ICookieStorageService, CookieStorageService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHttpClientHelper, HttpClientHelper>();
builder.Services.AddScoped<IApiCallHelper, ApiCallHelper>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<RefreshTokenHandler>();
builder.Services.AddHttpClient(Constant.ApiClient.Name, option =>
{

    var apiUrl = builder.Configuration["ApiUrl"] ?? "https://localhost:7132/api/";
    option.BaseAddress = new Uri(apiUrl);
}).AddHttpMessageHandler<RefreshTokenHandler>();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
builder.Services.AddScoped<IServOfferingService, ServOfferingService>();
builder.Services.AddVirtualizationService();

builder.Services.AddScoped<CategoryStore>();
builder.Services.AddScoped<CartStore>();
builder.Services.AddScoped<AvailabilityMockService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<IProfessionalDashboardService, ProfessionalDashboardService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IServiceHistoryService, ServiceHistoryService>();
builder.Services.AddScoped<IAdminDashboardService, AdminDashboardService>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();

await builder.Build().RunAsync();
