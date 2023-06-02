using Shouldly;
using WebServicesCI.DTO;

namespace WebServicesCI.Extensions
{
    public static class ShouldlyExtension
    {
        public static void ShouldBeValid(this BookingModel actualBookingModel, BookingModel expectedBookingResponse)
        {
            actualBookingModel.ShouldSatisfyAllConditions(
                () => actualBookingModel.FirstName.ShouldBe(expectedBookingResponse.FirstName),
                () => actualBookingModel.LastName.ShouldBe(expectedBookingResponse.LastName),
                () => actualBookingModel.DepositPaid.ShouldBe(expectedBookingResponse.DepositPaid),
                () => actualBookingModel.BookinDates.CheckIn.ShouldBe(expectedBookingResponse.BookinDates.CheckIn),
                () => actualBookingModel.BookinDates.CheckOut.ShouldBe(expectedBookingResponse.BookinDates.CheckOut),
                () => actualBookingModel.AdditionalNeeds.ShouldBe(expectedBookingResponse.AdditionalNeeds));
        }
    }
}
