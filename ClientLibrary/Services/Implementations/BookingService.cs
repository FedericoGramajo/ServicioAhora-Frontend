using ClientLibrary.Helper;
using ClientLibrary.Models;
using ClientLibrary.Models.Booking;
using ClientLibrary.Services.Contracts;

namespace ClientLibrary.Services.Implementations
{
    public class BookingService(IApiCallHelper apiHelper, IHttpClientHelper httpClient) : IBookingService
    {
        public async Task<ServiceResponse> CreateBookingAsync(CreateBooking model)
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constant.Booking.Create,
                Type = Constant.ApiCallType.Post,
                Client = client,
                Model = model
            };
            var result = await apiHelper.ApiCallTypeCall<CreateBooking>(apiCall);
            return await apiHelper.GetServiceResponse<ServiceResponse>(result) ?? apiHelper.ConnectionError();
        }

        public async Task<List<GetBooking>> GetCustomerBookingsAsync(string customerId)
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constant.Booking.GetByCustomer,
                Type = Constant.ApiCallType.Get,
                Client = client
            };
            apiCall.ToString(customerId);
            var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
            if (result != null && result.IsSuccessStatusCode)
            {
                var response = await apiHelper.GetServiceResponse<List<GetBooking>>(result);
                return response ?? new List<GetBooking>();
            }
            return new List<GetBooking>();
        }

        public async Task<ServiceResponse> SubmitRatingAsync(CreateRatingDto rating)
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constant.Rating.Post,
                Type = Constant.ApiCallType.Post,
                Client = client,
                Model = rating
            };
            var result = await apiHelper.ApiCallTypeCall<CreateRatingDto>(apiCall);
            return await apiHelper.GetServiceResponse<ServiceResponse>(result) ?? apiHelper.ConnectionError();
        }

        public async Task<GetRatingDto?> GetRatingByServiceAsync(Guid serviceId)
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constant.Rating.GetByService,
                Type = Constant.ApiCallType.Get,
                Client = client,
                Id = serviceId.ToString()
            };
            var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
            if (result.IsSuccessStatusCode)
            {
                return await apiHelper.GetServiceResponse<GetRatingDto>(result);
            }
            return null;
        }

        public async Task<List<GetBooking>> GetAdminGlobalBookingsAsync(DateTime startDate, DateTime endDate)
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = $"{Constant.Booking.GetAdminGlobal}?startDate={startDate:yyyy-MM-ddTHH:mm:ss}&endDate={endDate:yyyy-MM-ddTHH:mm:ss}",
                Type = Constant.ApiCallType.Get,
                Client = client
            };
            
            var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);

            if (result != null && result.IsSuccessStatusCode)
            {
                var response = await apiHelper.GetServiceResponse<List<GetBooking>>(result);
                return response ?? new List<GetBooking>();
            }
            return new List<GetBooking>();
        }
    }
}
