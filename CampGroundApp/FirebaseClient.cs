using RestAPI.FirebaseSettings.Models;
using RestAPI.FirebaseSettings.Models.RequestModels;
using RestAPI.FirebaseSettings.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestAPI
{
    public class FirebaseClient : IFirebaseClient
    {
        private readonly HttpClient _httpClient;
        public FirebaseClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FirebaseSignInResponse> SignInAsync(FirebaseSignUpRequest model)
        {
            const string url = ":signInWithPassword?key=AIzaSyCpSun5GRL9nSbERlYcnC0-LcnTWdWUFIk";

            var postJson = JsonSerializer.Serialize(model);
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(_httpClient.BaseAddress, url),
                Method = HttpMethod.Post,
                Content = new StringContent(postJson, Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<FirebaseSignInResponse>();
        }

        public async Task<FirebaseSignUpResponse> SignUpAsync(FirebaseSignUpRequest model)
        {
            const string url = ":signUp?key=AIzaSyCpSun5GRL9nSbERlYcnC0-LcnTWdWUFIk";

            var postJson = JsonSerializer.Serialize(model);
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(_httpClient.BaseAddress, url),
                Method = HttpMethod.Post,
                Content = new StringContent(postJson, Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<FirebaseSignUpResponse>();
            
        }
    }
}
