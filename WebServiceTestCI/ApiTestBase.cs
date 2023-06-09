﻿using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.IO;
using System.Text.Json;
using WebServicesCI.Constants;
using WebServicesCI.DTO.Requests;
using WebServicesCI.DTO.Responses;

namespace WebServicesCI
{
    public static class ApiTestBase
    {
        public static string RestfulBokerUrl => "https://restful-booker.herokuapp.com";

        public static IConfiguration Configuration { get; set; }

        static ApiTestBase()
        {
/*            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();*/
        }

        public static void AddHeaders(this RestRequest request)
        {
            request.AddHeader(HttpHeaders.Name.ContentType, HttpHeaders.Value.ApplicationJson);
            request.AddHeader(HttpHeaders.Name.Accept, HttpHeaders.Value.ApplicationJson);
        }

        public static void AddAuthorizationHeader(this RestRequest request)
        {
            var token = GetAuthToken();
            var headerValue = $"token={token}";
            request.AddHeader(HttpHeaders.Name.Cookie, headerValue);
        }

        public static string GetAuthToken()
        {
            var client = new RestClient("https://restful-booker.herokuapp.com");

            var body = new AuthorizationRequest
            {
                Username = Authorization.Username,
                Password = Authorization.Password
            };

            var request = new RestRequest(Endpoints.AuthorizationEndpoint, Method.Post);
            var json = JsonSerializer.Serialize(body);
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var response = client.Execute<AuthorizationResponse>(request);
            var result = JsonSerializer.Deserialize<AuthorizationResponse>(response.Content);
            if (result.Token != null)
            {
                return result.Token;
            }
            else
            {
                throw new Exception("Bad credentials");
            }
        }
    }
}
