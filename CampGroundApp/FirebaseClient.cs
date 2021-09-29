using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RestAPI.FirebaseSettings;
using RestAPI.FirebaseSettings.Models.RequestModels;
using RestAPI.FirebaseSettings.Models.ResponseModels;
using RestAPI.Options;
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
        private readonly string _baseAddress;
        private readonly FirebaseOptions _firebaseOptions;
        public FirebaseClient(HttpClient httpClient, IConfiguration configuration, IOptions<FirebaseOptions> firebaseOptions)
        {
            _baseAddress = configuration.GetSection("FirebaseOptions:BaseAddress").Value;
            _httpClient = httpClient;
            _firebaseOptions = firebaseOptions.Value;
        }

        public async Task<FirebaseSignInResponse> SignInAsync(FirebaseSignUpRequest model)
        {
            var url = $"{_baseAddress}/accounts:signInWithPassword?key=AIzaSyCpSun5GRL9nSbERlYcnC0-LcnTWdWUFIk";

            /* var postJson = JsonSerializer.Serialize(model);
             var request = new HttpRequestMessage
             {
                 RequestUri = new Uri(_httpClient.BaseAddress, url),
                 Method = HttpMethod.Post,
                 Content = new StringContent(postJson, Encoding.UTF8, "application/json")
             };

             var response = await _httpClient.SendAsync(request);*/
            var response = await _httpClient.PostAsJsonAsync(url, model);

            if (!response.IsSuccessStatusCode)
            {
                var newError = await response.Content.ReadFromJsonAsync<ErrorResponseModel>();
                throw new BadHttpRequestException($"{newError.Error.Message}", newError.Error.Code);
            }

            return await response.Content.ReadFromJsonAsync<FirebaseSignInResponse>();
        }

        public async Task<FirebaseSignUpResponse> SignUpAsync(FirebaseSignUpRequest model)
        {
            var url = $"{_baseAddress}/accounts:signUp?key=AIzaSyCpSun5GRL9nSbERlYcnC0-LcnTWdWUFIk";

            var response = await _httpClient.PostAsJsonAsync(url, model);

            if (!response.IsSuccessStatusCode)
            {
                var newError = await response.Content.ReadFromJsonAsync<ErrorResponseModel>();
                throw new BadHttpRequestException($"{newError.Error.Message}", newError.Error.Code);
            }
            return await response.Content.ReadFromJsonAsync<FirebaseSignUpResponse>();

        }
        public async Task<PasswordResetResponse> PasswordResetAsync(PasswordResetRequest model)
        {
            var url = $"{_baseAddress}/accounts:sendOobCode?key=AIzaSyCpSun5GRL9nSbERlYcnC0-LcnTWdWUFIk";

            var response = await _httpClient.PostAsJsonAsync(url, model);

            if (!response.IsSuccessStatusCode)
            {
                var newError = await response.Content.ReadFromJsonAsync<ErrorResponseModel>();
                throw new BadHttpRequestException($"{newError.Error.Message}", newError.Error.Code);
            }

            return await response.Content.ReadFromJsonAsync<PasswordResetResponse>();

        }
        public async Task<VerifyEmailResponse> VerifyEmailAsync(VerifyEmailRequest model)
        {
            var url = $"{_baseAddress}/accounts:sendOobCode?key=AIzaSyCpSun5GRL9nSbERlYcnC0-LcnTWdWUFIk";

            var response = await _httpClient.PostAsJsonAsync(url, model);

            if (!response.IsSuccessStatusCode)
            {
                var newError = await response.Content.ReadFromJsonAsync<ErrorResponseModel>();
                throw new BadHttpRequestException($"{newError.Error.Message}", newError.Error.Code);
            }

            return await response.Content.ReadFromJsonAsync<VerifyEmailResponse>();
        }

        public async Task<ChangeEmailResponse> ChangeEmailAsync(ChangeEmailRequest model)
        {
            var url = $"{_baseAddress}/accounts:update?key=AIzaSyCpSun5GRL9nSbERlYcnC0-LcnTWdWUFIk";

            var response = await _httpClient.PostAsJsonAsync(url, model);

            if (!response.IsSuccessStatusCode)
            {
                var newError = await response.Content.ReadFromJsonAsync<ErrorResponseModel>();
                throw new BadHttpRequestException($"{newError.Error.Message}", newError.Error.Code);
            }

            return await response.Content.ReadFromJsonAsync<ChangeEmailResponse>();

        }
        public async Task<ChangePasswordResponse> ChangePasswordAsync(ChangePasswordRequest model)
        {
            var url = $"{_baseAddress}/accounts:update?key=AIzaSyCpSun5GRL9nSbERlYcnC0-LcnTWdWUFIk";

            var response = await _httpClient.PostAsJsonAsync(url, model);

            if (!response.IsSuccessStatusCode)
            {
                var newError = await response.Content.ReadFromJsonAsync<ErrorResponseModel>();
                throw new BadHttpRequestException($"{newError.Error.Message}", newError.Error.Code);
            }

            return await response.Content.ReadFromJsonAsync<ChangePasswordResponse>();

        }

        public Task<int> DeleteAsync(DeleteAccountRequest model)
        {
            var url = $"{_baseAddress}/accounts:delete?key=AIzaSyCpSun5GRL9nSbERlYcnC0-LcnTWdWUFIk";

            return _httpClient.PostAsJsonAsync(url, model);
        }
    }
}
