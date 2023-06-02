using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebServicesCI.DTO
{
    public class BookingDates
    {
        [JsonPropertyName("checkin")]
        public string CheckIn { get; set; }

        [JsonPropertyName("checkout")]
        public string CheckOut { get; set; }
    }
}
