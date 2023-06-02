using RestSharp;
using WebServicesCI.Constants;
using WebServicesCI.DTO.Responses;
using WebServicesCI.DTO;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebServicesCI
{
    public abstract class BookingTestBase
    {
        protected readonly RestClient _client = new RestClient(ApiTestBase.RestfulBokerUrl);

        public async Task<BookingModel> GetBookingById(int bookingId)
        {
            var request = BookingByIdRequest(bookingId, Method.Get);

            var response = await _client.ExecuteAsync<BookingResponse>(request);
            var result = JsonSerializer.Deserialize<BookingModel>(response.Content);

            return result;
        }

        public async Task DeleteBookingById(int bookingId)
        {
            var request = BookingByIdRequest(bookingId, Method.Delete);

            await _client.ExecuteAsync<BookingResponse>(request);
        }

        public RestRequest BookingByIdRequest(int bookingId, Method method)
        {
            var request = new RestRequest(Endpoints.GetBookingByIdEndpoint, method);
            request.AddUrlSegment(Endpoints.GetBookingByIdSegment, bookingId);
            request.AddHeaders();
            request.AddAuthorizationHeader();

            return request;
        }

        public RestRequest PostBookingRequest(BookingModel bookingRequest)
        {
            var jsonRequest = JsonSerializer.Serialize(bookingRequest);

            var request = new RestRequest(Endpoints.BookingEndpoint, Method.Post);
            request.AddHeaders();
            request.AddParameter(HttpHeaders.Value.ApplicationJson, jsonRequest, ParameterType.RequestBody);

            return request;
        }
    }
}
