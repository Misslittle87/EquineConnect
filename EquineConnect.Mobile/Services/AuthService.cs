using System.Net.Http.Json;

namespace EquineConnect.Mobile.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> Login(string email, string password)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/auth/login",
                new
                {
                    email,
                    password,
                    rememberMe = true
                });

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
            return result?.Token;
        }
    }
}
