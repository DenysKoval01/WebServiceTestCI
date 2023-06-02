using NUnit.Framework;
using RestSharp;
using Shouldly;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using WebServicesCI.DTO.Responses;
using WebServicesCI.Extensions;
using WebServicesCI.TestData;

namespace WebServicesCI.Regression
{
    [TestFixture]
    public class BookingTests : BookingTestBase
    {
        [Test]
        public async Task PositiveBookingTest()
        {
            var bookingRequest = TestBookingModels.ValidBookingModel;

            var request = PostBookingRequest(bookingRequest);
            var postResponse = await _client.ExecuteAsync<BookingResponse>(request);
            var result = JsonSerializer.Deserialize<BookingResponse>(postResponse.Content);

            var getResult = await GetBookingById(result.BookingId);

            postResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
            getResult.ShouldBeValid(bookingRequest);
            
            await DeleteBookingById(result.BookingId);
        }


        [Test]
        public async Task NegativeBookingTest()
        {
            var bookingRequest = TestBookingModels.InValidBookingModel;

            var request = PostBookingRequest(bookingRequest);
            var postResponse = await _client.ExecuteAsync<BookingResponse>(request);

            postResponse.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
            postResponse.ErrorMessage.ShouldBe("'I' is an invalid start of a value. Path: $ | LineNumber: 0 | BytePositionInLine: 0.");

        }

        //use  allure serve allure-results to see Allure report
    }
}
