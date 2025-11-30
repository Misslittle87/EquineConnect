using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace EquineConnect.Mobile.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://10.0.2.2:5041/") };

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Login(string email, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", new { Email = email, Password = password });

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
            return result.Token;
        }
    }
}
