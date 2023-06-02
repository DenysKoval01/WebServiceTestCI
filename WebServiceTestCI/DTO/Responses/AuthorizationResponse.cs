using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebServicesCI.DTO.Responses
{
    public class AuthorizationResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("reason")]
        public string Reason { get; set; }
    }
}
